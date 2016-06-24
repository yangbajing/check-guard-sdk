# 数据加解密接入


以下以Java为例说明：

### 函数说明

**构造函数**

```java
/**
 * @param token 消息校验token
 * @param encodingAesKey 消息加解密的key EncodingAESKey
 * @param appid 第三方平台的appid
 *
 * @throws AesException 执行失败，请查看该异常的错误码和具体的错误信息
 */
public SdkBizMsgCrypt(/*String token, */String encodingAesKey, String appId) throws AesException
```

**解密函数**

```java
/**
 * 对密文进行解密.
 *
 * @param text 需要解密的密文
 * @return 解密得到的明文
 * @throws AesException aes解密失败
 */
public String decryptMsg(String text) throws AesException
```

**加密函数**

```java
/**
 * 对明文进行加密.
 *
 * @param text 需要加密的明文
 * @return 加密后base64编码的字符串
 * @throws AesException aes加密失败
 */
public String encryptMsg(String randomStr, String text) throws AesException
```

### 使用方法

**实例化对象**

使用构造函数实例化一个对象，传入第三方平台的token，encodingAesKey，appid。

**注意事项**

1. encodingAeSKey长度固定为43个字符，从a-z、A-Z、0-9共62个字符中选取。
