package sc.check.sdk.aes;

import java.security.SecureRandom;
import java.util.Random;

/**
 * Created by Yang Jing (yangbajing@gmail.com) on 2016-05-23.
 */
public class SdkUtils {
    public static final int APPID_LENGTH = 20;
    public static final int TOKEN_LENGTH = 32;
    public static final int ENCODING_AES_KEY_LENGTH = 43;
    public static final int RANDOM_STR_LENGTH = 16;
    public static final String RANDOM_STR = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

    // 随机生成16位字符串
    public static String getRandomStr() {
        Random random = new SecureRandom();
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < RANDOM_STR_LENGTH; i++) {
            int number = random.nextInt(RANDOM_STR.length());
            sb.append(RANDOM_STR.charAt(number));
        }
        return sb.toString();
    }
}
