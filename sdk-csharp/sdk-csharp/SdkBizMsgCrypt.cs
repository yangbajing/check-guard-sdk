using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
//using System.Web;
using System.Security.Cryptography;
//-40001 ： 签名验证错误
//-40002 :  xml解析失败
//-40003 :  sha加密生成签名失败
//-40004 :  AESKey 非法
//-40005 :  appid 校验错误
//-40006 :  AES 加密失败
//-40007 ： AES 解密失败
//-40008 ： 解密后得到的buffer非法
//-40009 :  base64加密异常
//-40010 :  base64解密异常
namespace Bee110
{
    class SdkBizMsgCrypt
    {
        string m_sToken;
        string m_sEncodingAESKey;
        string m_sAppID;
        enum SdkBizMsgCryptErrorCode
        {
            SdkBizMsgCrypt_OK = 0,
            SdkBizMsgCrypt_ValidateSignature_Error = -40001,
            SdkBizMsgCrypt_ParseXml_Error = -40002,
            SdkBizMsgCrypt_ComputeSignature_Error = -40003,
            SdkBizMsgCrypt_IllegalAesKey = -40004,
            SdkBizMsgCrypt_ValidateAppid_Error = -40005,
            SdkBizMsgCrypt_EncryptAES_Error = -40006,
            SdkBizMsgCrypt_DecryptAES_Error = -40007,
            SdkBizMsgCrypt_IllegalBuffer = -40008,
            SdkBizMsgCrypt_EncodeBase64_Error = -40009,
            SdkBizMsgCrypt_DecodeBase64_Error = -40010
        };

        //构造函数
	    // @param sToken: 公众平台上，开发者设置的Token
	    // @param sEncodingAESKey: 公众平台上，开发者设置的EncodingAESKey
	    // @param sAppID: 公众帐号的appid
        public SdkBizMsgCrypt(string sToken, string sEncodingAESKey, string sAppID)
        {
            m_sToken = sToken;
            m_sAppID = sAppID;
            m_sEncodingAESKey = sEncodingAESKey;
        }


        // 检验消息的真实性，并且获取解密后的明文
        // @param sEncryptText: 密文，对应POST请求的数据
        // @param sMsg: 解密后的原文，当return返回0时有效
        // @return: 成功0，失败返回对应的错误码
        public int DecryptMsg(string sEncryptText, ref string sMsg)
        {
			if (m_sEncodingAESKey.Length!=43)
			{
				return (int)SdkBizMsgCryptErrorCode.SdkBizMsgCrypt_IllegalAesKey;
			}

            //decrypt
            string cpid = "";
            try
            {
                sMsg = Cryptography.AES_decrypt(sEncryptText, m_sEncodingAESKey, ref cpid);
            }
            catch (FormatException)
            {
                return (int)SdkBizMsgCryptErrorCode.SdkBizMsgCrypt_DecodeBase64_Error;
            }
            catch (Exception)
            {
                return (int)SdkBizMsgCryptErrorCode.SdkBizMsgCrypt_DecryptAES_Error;
            }
            if (cpid != m_sAppID)
                return (int)SdkBizMsgCryptErrorCode.SdkBizMsgCrypt_ValidateAppid_Error;
            return 0;
        }

        //将投先查回复用户的消息加密打包
        // @param sText: 投先查待回复用户的消息，xml格式的字符串
        // @param sEncryptMsg: 加密后的可以直接回复用户的密文，包括msg_signature, timestamp, nonce, encrypt的xml格式的字符串,
        //						当return返回0时有效
        // return：成功0，失败返回对应的错误码
        public int EncryptMsg(string sText, ref string sEncryptMsg)
        {
			if (m_sEncodingAESKey.Length!=43)
			{
				return (int)SdkBizMsgCryptErrorCode.SdkBizMsgCrypt_IllegalAesKey;
			}
            try
            {
                sEncryptMsg = Cryptography.AES_encrypt(sText, m_sEncodingAESKey, m_sAppID);
            }
            catch (Exception)
            {
                return (int)SdkBizMsgCryptErrorCode.SdkBizMsgCrypt_EncryptAES_Error;
            }

            return 0;
        }

        public class DictionarySort : System.Collections.IComparer
        {
            public int Compare(object oLeft, object oRight)
            {
                string sLeft = oLeft as string;
                string sRight = oRight as string;
                int iLeftLength = sLeft.Length;
                int iRightLength = sRight.Length;
                int index = 0;
                while (index < iLeftLength && index < iRightLength)
                {
                    if (sLeft[index] < sRight[index])
                        return -1;
                    else if (sLeft[index] > sRight[index])
                        return 1;
                    else
                        index++;
                }
                return iLeftLength - iRightLength;

            }
        }
        //Verify Signature
        private static int VerifySignature(string sToken, string sTimeStamp, string sNonce, string sMsgEncrypt, string sSigture)
        {
            string hash = "";
            int ret = 0;
            ret = GenarateSinature(sToken, sTimeStamp, sNonce, sMsgEncrypt, ref hash);
            if (ret != 0)
                return ret;
            //System.Console.WriteLine(hash);
            if (hash == sSigture)
                return 0;
            else
            {
                return (int)SdkBizMsgCryptErrorCode.SdkBizMsgCrypt_ValidateSignature_Error;
            }
        }

        public static int GenarateSinature(string sToken, string sTimeStamp, string sNonce, string sMsgEncrypt ,ref string sMsgSignature)
        {
            ArrayList AL = new ArrayList();
            AL.Add(sToken);
            AL.Add(sTimeStamp);
            AL.Add(sNonce);
            AL.Add(sMsgEncrypt);
            AL.Sort(new DictionarySort());
            string raw = "";
            for (int i = 0; i < AL.Count; ++i)
            {
                raw += AL[i];
            }

            SHA1 sha;
            ASCIIEncoding enc;
            string hash = "";
            try
            {
                sha = new SHA1CryptoServiceProvider();
                enc = new ASCIIEncoding();
                byte[] dataToHash = enc.GetBytes(raw);
                byte[] dataHashed = sha.ComputeHash(dataToHash);
                hash = BitConverter.ToString(dataHashed).Replace("-", "");
                hash = hash.ToLower();
            }
            catch (Exception)
            {
                return (int)SdkBizMsgCryptErrorCode.SdkBizMsgCrypt_ComputeSignature_Error;
            }
            sMsgSignature = hash;
            return 0;
        }
    }
}
