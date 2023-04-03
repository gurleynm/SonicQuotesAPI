using System.Security.Cryptography;
using System.Text;

namespace SonicQuotesAPI
{
    public class Constants
    {
        public static string PassKey => "SpinningIsAGoodTrick!";
        public static string SHA256(string value)
        {
            if (value == null) return null;
            StringBuilder Sb = new StringBuilder();

            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                Byte[] result = hash.ComputeHash(enc.GetBytes(value));

                foreach (Byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }
    }
}
