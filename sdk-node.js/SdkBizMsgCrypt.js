/**
 * 投先查数据加解密 Node.js SDK
 * Created by Yang Jing (yangbajing@gmail.com) on 2016-06-03.
 */
'use strict';
var crypto = require('crypto');

class SdkBizMsgCrypt {

  /**
   *
   * @param token 接入商Token
   * @param encodingAesKey 接入商消息加解密密钥
   * @param appid 接入商 appid
   */
  constructor(token, encodingAesKey, appid) {
    this.token = token;
    this.encodingAesKey = encodingAesKey;
    this.appid = appid;
  }

  /**
   * 加密函数
   * @param text 需要加密的字符串
   * @returns [] 返回长度为2的数组，第一个为状态标识。当[0]为true时，[1]为加密后字符串，否则[1]为错误消息
   */
  encryptMsg(text) {
    var prp = new PrpCrypt(this.encodingAesKey);
    return prp.encrypt(text, this.appid);
  }

  /**
   * 解密函数
   * @param encryptText 需要解密的字符串
   * @returns [] 返回长度为2的数组，第一个为状态标识。当[0]为true时，[1]为解密后字符串，否则[1]为错误消息
   */
  decryptMsg(encryptText) {
    var prp = new PrpCrypt(this.encodingAesKey);
    return prp.decrypt(encryptText, this.appid);
  }

}

class PrpCrypt {
  constructor(k) {
    this.key = new Buffer(k + '=', 'base64').toString('binary');
    this.mode = 'aes-256-cbc';
  }

  encrypt(originText, appid) {
    var text = new Buffer(originText),
      pad = this.enclen(text.length);
    var pack = new PKCS7().encode(20 + text.length + appid.length),
      random = this.getRandomStr(),
      content = random + pad + text.toString('binary') + appid + pack;
    try {
      var cipher = crypto.createCipheriv(this.mode, this.key, this.key.slice(0, 16));
      cipher.setAutoPadding(false);
      var crypted = cipher.update(content, 'binary', 'base64') + cipher.final('base64');
      return [true, crypted];
    }
    catch (e) {
      return [false, e];
    }
  }

  decrypt(encrypted, appid) {
    var decipher, plain_text;
    try {
      decipher = crypto.Decipheriv(this.mode, this.key, this.key.slice(0, 16));
      decipher.setAutoPadding(false);
      plain_text = decipher.update(encrypted, 'base64', 'utf8') + decipher.final('utf8');
    }
    catch (e) {
      return [false, e];
    }

    var pad = plain_text.charCodeAt(plain_text.length - 1);
    var content = plain_text.slice(20, -(pad + appid.length)); //.replace(/<\/xml>.*/, '</xml>');
    var from_appid = plain_text.slice(20 + content.length, -pad);
    if (from_appid != appid) {
      return [false, 'appid invalid'];
    }

    return [true, content];
  }

  enclen(len) {
    var buf = new Buffer(4);
    buf.writeUInt32BE(len);
    return buf.toString('binary');
  }

  getRandomStr() {
    var pool = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz";
    var re = '';
    for (var i = 0; i < 16; i++) {
      re += pool.charAt(Math.random() * pool.length);
    }
    return re;
  }
}

class PKCS7 {
  constructor() {
    this.block_size = 32;
  }

  encode(text_length) {
    // 计算需要填充的位数
    var amount_to_pad = this.block_size - (text_length % this.block_size);
    if (amount_to_pad === 0) {
      amount_to_pad = this.block_size;
    }

    // 获得补位所用的字符
    var pad = String.fromCharCode(amount_to_pad), s = [];
    //console.log('pad:', amount_to_pad, pad);
    for (var i = 0; i < amount_to_pad; i++) s.push(pad);
    return s.join('');
  }
}

module.exports = SdkBizMsgCrypt;
