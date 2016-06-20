# 应用接入（方式二）

## API

向接口：`https://www.bee110.com/sdk/v1/encrypt?appid=<APP ID>` 发送**POST**请求，并提交需要加密的请求参数（使用JSON格式）。

***（注意：必需使用https请求）***

**请求参数**

请求参数使用JSON格式上传，当前许可的参数有：

**企业套餐**

- companyName: 企业命名
- regNo: 企业注册号。可选

**个人套餐**

- personName: 姓名
- idCard: 身份证号。可选
- phone: 手机号。可选
- card: 银行卡号。可选 

**返回值**

当调用成功，会返回加密后字符串。接入商需要用加密后字符串和`appid`生成URL链接。格式如下：

```
String url = "https://www.bee110.com/page/query" + "?appid=" + appid + "&value=" + encryptText;
```

并加生成的`url`渲染到页面上需要使用投先查功能的按钮或链接上。如：

```html
<a href="<url>" target="_blank">投先查</a>
```

## 示例

**企业套餐示例：**

```curl
curl -v -H 'Content-Type: application/json;charset=utf8' \
  -d '{"companyName":"","regNo":""}' \
  https://www.bee110.com/sdk/v1/encrypt?appid=sdfsdfjeDFSD3429dsf98s234
```

**个人套餐请求示例：**

```curl
curl -v -H 'Content-Type: application/json;charset=utf8' \
  -d '"persons":[{"personName":"","card":""},{"personName":"","idCard":"","phone":""}]}' \
  https://www.bee110.com/sdk/v1/encrypt?appid=sdfsdfjeDFSD3429dsf98s234
```

