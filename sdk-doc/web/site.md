# 网站接入

## 网页设置

P2P网络平台接入投先查服务，需要引入JS SDK。JS SDK地址：

```
https://www.bee110.com/sdk/js/sdk.js
```

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

网站接入投先查服务的设置这些就可以了，其中`data-sc-value`是加密后的文本，需要使用`encodingAesKey`对原始消息进行加密。对消息的加密推荐使用官方提供的[SDK](../dev/sdk.md)，具体加密方式请看 [数据加解密接入](../dev/encrypt.md)。

## 参数组装与数据加密

投先查现在提供如下参数：

- companyName: 企业名
- regNo: 企业注册号
- personName: 姓名
- idCard: 身份证号
- card: 银行卡号
- phone: 手机号

开发者需要按一定顺序拼接参数，示例如下：

```java
String msg = "companyName=xxxx&regNo=xxxx&personName=xxxx&idCard=xxxx&card=xxx&phone=xxxx&personName=xxxxx&phone=xxxx&card=xxxx";
SdkBizMsgCrypt msgCrypt = new SdkBizMsgCrypt(token, encodingAesKey, appid);
String value = msgCrypt.encryptMsg(msg);
```

**注意：**

`companyName`和`regNo`只能传一个，可以选择不传或只传其中一个参数。

`personName`、`idCard`、`card`、`phone`，四个参数均可以不传。但若传，则`personName`必须存在！
当有多组人需要提交时，请先按序填写完一组后再填写下一组。

将调用`msgCrypt.encryptMsg`生成的加密字符串填入按钮页面的`data-sc-value`属性即可。

