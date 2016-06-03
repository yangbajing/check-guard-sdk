<?php

/**
 * 对平台发送给投先查的消息加解密示例代码.
 */

include_once "sha1.php";
include_once "pkcs7Encoder.php";
include_once "errorCode.php";

/**
 * 1.第三方回复加密消息给投先查；
 * 2.第三方收到投先查发送的消息，验证消息的安全性，并对消息进行解密。
 */
class SdkBizMsgCrypt
{
    private $token;
    private $encodingAesKey;
    private $appId;

    /**
     * 构造函数
     * @param $token string 投先查上，开发者设置的token
     * @param $encodingAesKey string 投先查上，开发者设置的EncodingAESKey
     * @param $appId string 投先查的appId
     */
    public function SdkBizMsgCrypt($token, $encodingAesKey, $appId)
    {
        $this->token = $token;
        $this->encodingAesKey = $encodingAesKey;
        $this->appId = $appId;
    }

    /**
     * 将投先查回复用户的消息加密打包.
     * <ol>
     *    <li>对要发送的消息进行AES-CBC加密</li>
     *    <li>生成安全签名</li>
     *    <li>将消息密文和安全签名打包成xml格式</li>
     * </ol>
     *
     * @param $text string 投先查待回复用户的消息，xml格式的字符串
     * @param &$encryptMsg string 加密后的可以直接回复用户的密文，包括msg_signature, timestamp, nonce, encrypt的xml格式的字符串,
     *                      当return返回0时有效
     *
     * @return int 成功0，失败返回对应的错误码
     */
    public function encryptMsg($text, &$encryptMsg)
    {
        $pc = new Prpcrypt($this->encodingAesKey);

        //加密
        $array = $pc->encrypt($text, $this->appId);
        $ret = $array[0];
        if ($ret != 0) {
            return $ret;
        }

        $encryptMsg = $array[1];
        return ErrorCode::$OK;
    }


    /**
     * 检验消息的真实性，并且获取解密后的明文.
     * <ol>
     *    <li>利用收到的密文生成安全签名，进行签名验证</li>
     *    <li>若验证通过，则提取xml中的加密消息</li>
     *    <li>对消息进行解密</li>
     * </ol>
     *
     * @param $encrypted string 密文，对应POST请求的数据
     * @param &$text string 解密后的原文，当return返回0时有效
     *
     * @return int 成功0，失败返回对应的错误码
     */
    public function decryptMsg($encrypted, &$text)
    {
        if (strlen($this->encodingAesKey) != 43) {
            return ErrorCode::$IllegalAesKey;
        }

        $pc = new Prpcrypt($this->encodingAesKey);

        $result = $pc->decrypt($encrypted, $this->appId);
        if ($result[0] != 0) {
            return $result[0];
        }
        $text = $result[1];

        return ErrorCode::$OK;
    }

}

