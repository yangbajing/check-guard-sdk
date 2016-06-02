# 接入指南

## 接入概述

接入投先查平台，开发者需要按照如下步骤完成：

1. 向邮箱（暂未开放申请）发送申请邮件，内容如下：
    
    - 公司名：XXX网络金融有限公司
    - 联系邮箱：xxxx@xxx.com
    - 网站域名：xxx.com

网站域名部分只需要填写一级域名即可。开发账号申请成功后，相应的appid、token、encodingAesKey将发送到您提供的
联系邮箱中。

2. 依据接口文档实现业务逻辑

## 测试消息加密功能

```java
String appid = "....";
String encodingAesKey = "....";
String token = "....";
String msg = "投先查消息加密";
SdkBizMsgCrypt msgCrypt = new SdkBizMsgCrypt(token, encodingAesKey, appid);
String encryptText = msgCrypt.encryptMsg(msg);
Response resp = httpClient.url("https://www.bee110.com/sdk/test/decrypt")
  .withQueryString("appid", appid)
  .withBody(encryptText)
  .post();
String decryptMsg = resp.body;
assertEquals(msg, decryptMsg);
```
