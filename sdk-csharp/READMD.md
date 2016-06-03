# C# SDK

**注意事项**

1. Cryptography.cs文件封装了AES加解密过程，用户无须关心具体实现。SdkBizMsgCrypt.cs文件提供了用户接入投先查的两个接口，Sample.cs文件提供了如何使用这两个接口的示例。
2. SdkBizMsgCrypt.cs封装了DecryptMsg, EncryptMsg两个接口，分别用于收到用户回复消息的解密以及开发者回复消息的加密过程。使用方法可以参考Sample.cs文件。
3. 加解密协议请参考投先查开放平台官方文档。
