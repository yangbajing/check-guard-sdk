# 网站接入（方式一）

P2P网络平台接入投先查服务，需要引入JS SDK。JS SDK地址：`https://www.bee110.com/sdk/js/sdk.js`。

引入JS SDK需要在使用投先查功能插件的网页上引入（投先查JS SDK支持https）。示例代码如下：

```html
<script src="//www.bee110.com/sdk/js/sdk.js">
    appid:xxxxxxxxdUpgxxxxxxxx
</script>
```

然后再在需要放置“投先查”按钮的地方设置`button`按钮：

```html
<button type="button" class="sc-button" data-sc-value="SageXmehKaJNL6N9ALcuVoMfMknJpfDvzWmaZp/4l/1GYRcup8YuiY57UU1lqsWr2IILN779HmjiClQdKxf5hJ9WR/CNl/qYsTEcph2Y4TTpj52oNEaEWf8R+bkKXyevE4dkL/WILoezLMF220IRMpZfDstBF+91jJmmQh63XoM=">投先查（重庆XXXXXX有限公司）</button>
```

`.sc-button` 属性用于告诉投先查SDK需要判断哪些**button**（或**a**）是进入投先查的按钮。换句话说，只有设置了`.sc-button`属性的按钮或链接会被投先查JS SDK判断。

其中`data-sc-value`是加密后的文本，需要使用`encodingAesKey`对原始消息进行加密。对消息的加密推荐使用官方提供的[SDK](../dev/sdk.md)，具体加密方式请看 [数据加解密接入](../dev/encrypt.md)。

## 参数组装与数据加密

投先查现在提供两种类型的数据查询服务：

1. 企业查询。包含以下参数：
    - companyName: 企业名
    - regNo: 企业注册号
2. 个人查询。
    - personName: 姓名
    - idCard: 身份证号
    - card: 银行卡号
    - phone: 手机号

开发者需要按一定顺序拼接参数，示例如下：

```java
// String msg = "companyName=xxxx&regNo=xxxx" // 企业查询参数拼接
String msg = "personName=xxxx&idCard=xxxx&card=xxx&phone=xxxx&personName=xxxxx&phone=xxxx&card=xxxx";
SdkBizMsgCrypt msgCrypt = new SdkBizMsgCrypt(token, encodingAesKey, appid);
String encryptText = msgCrypt.encryptMsg(msg);
```

**注意：**

> 企业套餐：`companyName`和`regNo`只能传一组，可以选择不传或只传其中一个参数。
>
> 个人套餐：`personName`、`idCard`、`card`、`phone`，`personName`为必传参数。当有多组人需要提交时，请先按序填写完一组后再填写下一组。
>
> 将调用`msgCrypt.encryptMsg`生成的加密字符串填入按钮的的`data-sc-value`属性即可。

## 其它

### 加密功能在线测试接口

生成的加密字符串`encryptText`可以通过投先查提供的一个测试接口 `https://www.bee110.com/sdk/test/decrypt?appid=<appid>` 来验证正确性：

```bash
curl -v -XPOST -d 'SfEyVQQ3e+Y2gDJvaga9hG9eHdTLI6HWjM30u3nw5hCEaoHvy7XjbR6m0tcoeMYXjRLplGWkOGTr0gwHDo6SkoOEZDBaoyP/ZrcsvhsJLOjHu7RBKi10IKMcqe22/wAmDmliiivwtWuFXcDAYblTfsCx6rlpkjkHCFaSnMd48fw=' "https://www.bee110.com/sdk/test/decrypt?appid=t5TK63TodUpgink1yg0o"
```

当解密成功后将返回200状态码，并返回原始未密码内容：

```
< HTTP/1.1 200 OK
< Content-Type: text/plain; charset=utf-8
< 
companyName=重庆XXXXXX有限公司&personName=XXX&idCard=440224XXXXXXXX28XX
```

