# 加密解密技术方案

出于安全考虑，接入立与投先查之间的数据交互必须经过加解密。

## 加密解密技术方案

投先查插件的数据加密解密技术方案采用基于AES加解密算法（<a target="_blank" href="https://zh.wikipedia.org/wiki/%E9%AB%98%E7%BA%A7%E5%8A%A0%E5%AF%86%E6%A0%87%E5%87%86">高级加密标准</a>）来实现，具体如下：

1. EncodingAESKey即消息加解密Key，长度固定为43个字符，从a-z,A-Z,0-9共62个字符中选取。
2. AES密钥： AESKey=Base64_Decode(EncodingAESKey + “=”)，EncodingAESKey尾部填充一个字符的“=”, 用Base64_Decode生成32个字节的AESKey；
3. AES采用CBC模式，秘钥长度为32个字节（256位），数据采用PKCS#7填充 ； PKCS#7：K为秘钥字节数（采用32），buf为待加密的内容，N为其字节数。Buf 需要被填充为K的整数倍。在buf的尾部填充(K-N%K)个字节，每个字节的内容 是(K- N%K)。

<table>
<tr>
<th>尾部填充</th>
<th></th>
</tr>
<tr>
<td>01</td>
<td>if ( N%K==(K-1))</td>
</tr>
<tr>
<td>0202</td>
<td>if ( N%K==(K-3))</td>
</tr>
<tr>
<td>030303</td>
<td>if ( N%K==(K-2))</td>
</tr>
<tr>
<td>....</td>
<td>....</td>
</tr>
<tr>
<td>KK....KK (K个字节)</td>
<td>if ( N%K==0)</td>
</tr>
</table>

4. BASE64采用MIME格式，字符包括大小写字母各26个，加上10个数字，和加号“+”，斜杠“/”，一共64个字符，等号“=”用作后缀填充；

## 例子：接入商发送消息

现在消息明文如下：

```java
String msg = "companyName=重庆XXXXXX有限公司&personName=XXX&idCard=440224XXXXXXXX28XX";
```

加密后的消息密文如下：

```java
String encryptMsg = "WjnWgKJ5uZ6cK1km+Acm8pWB2LL20s1R8vxpKwUkWCYmUPCMBnZeKgVd/fO/f/HmjPNUs0tkp3/aIOMB4ROWDVPwmYWsPddJhwZgIUIojpnwsUHWAm8CAXI2sT8spE2zpyBJmopi3x79b7rX08vteeRIJsk/LsgSjDTifdNWt/A=";
```

其中，`msg_encrypt = Base64_Encode( AES_Encrypt[ random(16B) + msg_len(4B) + msg + ] )`。
加密的buf由16个字节的随机字符串、4个字节的msg_len(网络字节序)、msg和`AESKey =Base64_Decode(EncodingAESKey + “=”)`，32个字节。

## 常见错误举例

1. java要求jdk 1.6以上

2. 异常java.security.InvalidKeyException:illegal Key Size的解决方案：在官方网站下载JCE无限制权限策略文件（<a target="_blank" href="http://www.oracle.com/technetwork/java/javase/downloads/jce-7-download-432124.html">JDK7的下载地址</a>）
    下载后解压，可以看到local_policy.jar和US_export_policy.jar以及readme.txt，如果安装了JRE，将两个jar文件放到%JRE_HOME%\lib\security目录下覆盖原来的文件；如果安装了JDK，将两个jar文件放到%JDK_HOME%\jre\lib\security目录下覆盖原来文件
