using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
namespace MsgCryptTest
{
    class Sample
    {

        static void Main(string[] args)
        {
            //公众平台上开发者设置的token, appID, EncodingAESKey
            string sToken = "iS-aJPrZ5NxVoEH6LmmAs9GtIYuI_PW3JBk0t5tr";
            string sAppID = "t5TK63TodUpgink1yg0o";
            string sEncodingAESKey = "V9cgiUeuGS99RSIX7vm5nat796Adl31ro2eWCpST46H";

            Bee110.SdkBizMsgCrypt wxcpt = new Bee110.SdkBizMsgCrypt(sToken, sEncodingAESKey, sAppID);
            
            string sText = "companyName=xxxx&regNo=xxxx&personName=xxxx&idCard=xxxx&card=xxx&phone=xxxx&personName=xxxxx&phone=xxxx&card=xxxx";
            string sEncryptMsg = ""; //加密后的密文
            ret = wxcpt.EncryptMsg(sText, ref sEncryptMsg);
            System.Console.WriteLine("sEncryptMsg");
            System.Console.WriteLine(sEncryptMsg);

            string sMsg = "";  //解析之后的明文
			int ret = wxcpt.DecryptMsg(sEncryptMsg, ref sMsg);
            if (ret != 0)
            {
                System.Console.WriteLine("ERR: Decrypt fail, ret: " + ret);
                return;
            }
            System.Console.WriteLine(sMsg);
        }
    }
}
