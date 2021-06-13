using System.Web;

namespace AnnotationApi.Client.Utils
{
    public class HttpHelper
    {
        public static string UrlEncode(string encode)
        {
            if (encode == null) return null;
            string encoded = "";

            foreach (char c in encode)
            {
                int val = (int)c;
                if (val == 32 || val == 45 || (val >= 48 && val <= 57) || (val >= 65 && val <= 90) || (val >= 97 && val <= 122))
                    encoded += c;
                else
                    encoded += "%" + val.ToString("X");
            }

            // Fix MS BS
            encoded = encoded.Replace("%25", "-25").Replace("%2A", "-2A").Replace("%26", "-26").Replace("%3A", "-3A");

            return encoded;
        }

        public static string UrlDecode(string decode)
        {
            if (decode == null) return null;
            // Fix MS BS
            decode = decode.Replace("-25", "%25").Replace("-2A", "%2A").Replace("-26", "%26").Replace("-3A", "%3A");

            return HttpUtility.UrlDecode(decode);
        }
    }
}
