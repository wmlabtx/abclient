using System;
using System.Security.Cryptography;
using System.Text;

namespace ABClient.Helpers
{
    internal static class SerialKey
    {
        public static string Newyork(string nick, DateTime expiredDate)
        {
            var str = $"((++{nick.ToUpperInvariant()}***{expiredDate.ToString("yyyyMMdd")}++))";
            var buffer = Encoding.UTF8.GetBytes(str);
            var md5 = MD5.Create();
            var hashbuffer = md5.ComputeHash(buffer);
            const string m = "ОЕАИНТСРВЛКМПУЯГ";

            var sb = new StringBuilder();
            for (var i = 0; i < 16; i++)
            {
                sb.Append(m[hashbuffer[i] >> 4]);
                sb.Append(m[hashbuffer[i] & 0xF]);
                if (((i + 1) % 4) == 0 && (i != 15))
                    sb.Append('-');
            }

            return sb.ToString();
        }

        /*
        internal static bool IsAllowed(string actualNick, DateTime actualDateTime, string key)
        {
            string nick = string.Empty;
            DateTime creationDate = DateTime.MinValue;
            int days = 0;

            if (!Decrypt(key, ref nick, ref creationDate, ref days, "ABC"))
                return false;

            if (!string.Equals(actualNick, nick, StringComparison.CurrentCultureIgnoreCase))
                return false;

            if (actualDateTime.CompareTo(creationDate.AddDays(days)) == 1)
                return false;

            return true;
        }

        internal static DateTime GetLicenceExpired(string key)
        {
            string nick = string.Empty;
            DateTime creationDate = DateTime.MinValue;
            int days = 0;

            if (!Decrypt(key, ref nick, ref creationDate, ref days, "ABC"))
                return DateTime.MinValue;

            return creationDate.AddDays(days);
        }

        internal static string Encrypt(string nick, DateTime creationDate, int days, string secretPhase)
        {
            var sb = new StringBuilder();
            sb.Append(nick);
            sb.Append('|');
            sb.Append(creationDate.ToString("yyyyMMdd"));
            sb.Append('|');
            sb.Append(days);
            var result = EncryptString(sb.ToString(), secretPhase);
            return result;
        }

        private static bool Decrypt(string cipherText, ref string nick, ref DateTime creationDate, ref int days, string secretPhase)
        {
            string result = DecryptString(cipherText, secretPhase);
            if (string.IsNullOrEmpty(result))
                return false;

            var pars = result.Split('|');
            if (pars.Length < 3)
                return false;

            nick = pars[0];
            if (pars[1].Length != 8)
                return false;

            int year;
            if (!int.TryParse(pars[1].Substring(0, 4), out year))
                return false;

            int month;
            if (!int.TryParse(pars[1].Substring(4, 2), out month))
                return false;

            int day;
            if (!int.TryParse(pars[1].Substring(6, 2), out day))
                return false;

            creationDate = new DateTime(year, month, day);

            return int.TryParse(pars[2], out days);
        }

        private static string EncryptString(string plainText, string secretPhase)
        {
            var initVectorBytes = Encoding.UTF8.GetBytes("HR$2pIjHR$2pIj11");
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            var password = new Rfc2898DeriveBytes(secretPhase, initVectorBytes);
            var keyBytes = password.GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged { Mode = CipherMode.CBC };
            var encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                    cryptoStream.FlushFinalBlock();
                    return Convert.ToBase64String(memoryStream.ToArray(), Base64FormattingOptions.None);
                }
            }
        }

        private static string DecryptString(string cipherText, string secretPhase)
        {
            if (string.IsNullOrEmpty(cipherText) || string.IsNullOrEmpty(secretPhase))
                return null; 

            var initVectorBytes = Encoding.ASCII.GetBytes("HR$2pIjHR$2pIj11");
            try
            {
                byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
                var password = new Rfc2898DeriveBytes(secretPhase, initVectorBytes);
                var keyBytes = password.GetBytes(256 / 8);
                var symmetricKey = new RijndaelManaged { Mode = CipherMode.CBC };
                var decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
                using (var memoryStream = new MemoryStream(cipherTextBytes))
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        var plainTextBytes = new byte[cipherTextBytes.Length];
                        var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                        return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
                    }
                }
            }
            catch (FormatException)
            {
                return null;
            }
        }
         */
    }
}
