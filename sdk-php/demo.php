<?php

include_once "sdkBizMsgCrypt.php";

// 第三方发送消息给投先查
$appid = "t5TK63TodUpgink1yg0o";
$token = "iS-aJPrZ5NxVoEH6LmmAs9GtIYuI_PW3JBk0t5tr";
$encodingAesKey = "V9cgiUeuGS99RSIX7vm5nat796Adl31ro2eWCpST46H";
$text = "companyName=xxxx&regNo=xxxx&personName=xxxx&idCard=xxxx&card=xxx&phone=xxxx&personName=xxxxx&phone=xxxx&card=xxxx";


$pc = new SdkBizMsgCrypt($token, $encodingAesKey, $appid);
$encryptMsg = '';
$errCode = $pc->encryptMsg($text, $encryptMsg);
if ($errCode == 0) {
    print("加密后: " . $encryptMsg . "\n");
} else {
    print($errCode . "\n");
}

$decryptMsg = "";
$errCode = $pc->decryptMsg($encryptMsg, $decryptMsg);
if ($errCode != 0) {
    print($errCode . "\n");
} else {
    print("解密后: " . $decryptMsg . "\n");
}
