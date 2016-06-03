/**
 * Created by Yang Jing (yangbajing@gmail.com) on 2016-06-03.
 */
var SdkBizMsgCrypt = require('./SdkBizMsgCrypt');

var appid = "t5TK63TodUpgink1yg0o";
var token = "iS-aJPrZ5NxVoEH6LmmAs9GtIYuI_PW3JBk0t5tr";
var encodingAesKey = "V9cgiUeuGS99RSIX7vm5nat796Adl31ro2eWCpST46H";

var bizMsgCrypt = new SdkBizMsgCrypt(token, encodingAesKey, appid);

var text = "companyName=xxxx&regNo=xxxx&personName=xxxx&idCard=xxxx&card=xxx&phone=xxxx&personName=xxxxx&phone=xxxx&card=xxxx";
console.log(text);

var ret = bizMsgCrypt.encryptMsg(text);
var encryptText = ret[1];
console.log(ret[0], encryptText);

ret = bizMsgCrypt.decryptMsg(encryptText);
var decryptText = ret[1];
console.log(ret[0], decryptText);

