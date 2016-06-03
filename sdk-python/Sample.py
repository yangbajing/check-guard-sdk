#!/usr/bin/env python
# -*- coding: utf-8 -*-
from SdkBizMsgCrypt import SdkBizMsgCrypt

if __name__ == "__main__":
    """
    1.第三方回复加密消息给投先查；
    2.第三方收到投先查发送的消息，验证消息的安全性，并对消息进行解密。
    """
    encodingAESKey = "V9cgiUeuGS99RSIX7vm5nat796Adl31ro2eWCpST46H"
    text = "companyName=xxxx&regNo=xxxx&personName=xxxx&idCard=xxxx&card=xxx&phone=xxxx&personName=xxxxx&phone=xxxx&card=xxxx"
    token = "iS-aJPrZ5NxVoEH6LmmAs9GtIYuI_PW3JBk0t5tr"
    appid = "t5TK63TodUpgink1yg0o"

    # 测试加密接口
    sdkBizCrypt = SdkBizMsgCrypt(token, encodingAESKey, appid)
    ret, encrypt_text = sdkBizCrypt.EncryptMsg(text)
    print ret, encrypt_text

    # 测试解密接口
    ret, decryp_text = sdkBizCrypt.DecryptMsg(encrypt_text)
    print ret, decryp_text
