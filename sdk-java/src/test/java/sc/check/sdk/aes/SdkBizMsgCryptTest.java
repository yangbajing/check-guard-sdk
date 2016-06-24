package sc.check.sdk.aes;

import com.google.common.escape.Escaper;
import com.google.common.net.UrlEscapers;
import org.junit.BeforeClass;
import org.junit.Test;

import static java.lang.System.out;
import static org.junit.Assert.assertEquals;

/**
 * SdkBizMsgCryptTest测试
 * Created by Yang Jing (yangbajing@gmail.com) on 2016-06-02.
 */
public class SdkBizMsgCryptTest {
    private final static Escaper escaper = UrlEscapers.urlFormParameterEscaper();
    private final static String appid = "abcdefghijklmn";
    private final static String token = "1234567890asdfghjkqwer";
    private final static String encodingAesKey = "zxcvbnmasdfghjklqwertyuiop1234567890ij76gdw";
    private static SdkBizMsgCrypt msgCrypt;

    @BeforeClass
    public static void beforeClass() throws AesException {
        msgCrypt = new SdkBizMsgCrypt(token, encodingAesKey, appid);
        out.println("encodingAesKey: " + encodingAesKey);
        out.println("msgCrypt: " + msgCrypt);
    }

    @Test
    public void test02() throws Exception {
        String str = "companyName=重庆XXXXXX有限公司&personName=XXX&idCard=440224XXXXXXXX28XX";
        String encryptStr = msgCrypt.encryptMsg(str);
        out.println("encryptStr: " + encryptStr);
        out.println("encryptStr: " + escaper.escape(encryptStr));
        assertEquals(msgCrypt.decryptMsg(encryptStr), str);
    }

}