using System.Text;

namespace Common;

public class AuthUtils
{

    public static byte[] symmetricSecurityKeyBase {
        get {
            //とりあえずテキトーな鍵
            return Encoding.UTF8.GetBytes("1234567890123456");
        }
    }
    public static string validIssuer {
        get
        {
            // ダミー
            return "Dummy";
        }
    }
    public static string validAudience
    {
        get
        {
            // ダミー
            return "Dummy";
        }
    }
}


