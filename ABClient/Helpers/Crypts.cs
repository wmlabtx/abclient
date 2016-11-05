namespace ABClient.Helpers
{
    using System;
    using System.IO;
    using System.Security.Cryptography;

    internal static class Crypts
    {
        internal static string EncryptString(string str, string password)
        {
            if (str == null)
            {
                throw new ArgumentNullException("str");
            }

            if (password == null)
            {
                throw new ArgumentNullException("password");
            }

            var buffer = Russian.Codepage.GetBytes(str);
            var encbuffer = EncryptData(buffer, password);
            return Convert.ToBase64String(encbuffer);
        }

        internal static string DecryptString(string str, string password)
        {
            if (str == null)
            {
                throw new ArgumentNullException("str");
            }

            if (password == null)
            {
                throw new ArgumentNullException("password");
            }

            var encbuffer = Convert.FromBase64String(str);
            var buffer = DecryptData(encbuffer, password);
            return Russian.Codepage.GetString(buffer);
        }

        internal static byte[] EncryptData(byte[] array, string password)
        {
            if (array == null)
            {
                throw new ArgumentNullException("array");
            }

            if (password == null)
            {
                throw new ArgumentNullException("password");
            }

            var pdb = new Rfc2898DeriveBytes(password, AppConsts.SaltBinary);
            return Encryptbuffer(array, pdb.GetBytes(16), pdb.GetBytes(8));
        }

        internal static byte[] DecryptData(byte[] encrypted, string password)
        {
            try
            {
                var pdb = new Rfc2898DeriveBytes(password, AppConsts.SaltBinary);
                return Decryptbuffer(encrypted, pdb.GetBytes(16), pdb.GetBytes(8));
            }
            catch (CryptographicException)
            {
                return null;
            }
        }

        internal static string Password2Hash(string password)
        {
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }

            var bpassword = Russian.Codepage.GetBytes(AppConsts.SaltText + password);
            using (var md5 = new MD5CryptoServiceProvider())
            {
                return Convert.ToBase64String(md5.ComputeHash(bpassword));
            }
        }

        private static byte[] Encryptbuffer(byte[] clearData, byte[] key, byte[] iv)
        {
            if (clearData == null)
            {
                throw new ArgumentNullException("clearData");
            }

            if (key == null)
            {
                throw new ArgumentNullException("key");
            }

            if (iv == null)
            {
                throw new ArgumentNullException("iv");
            }

            using (var ms = new MemoryStream())
            {
                using (var alg = TripleDES.Create())
                {
                    alg.Key = key;
                    alg.IV = iv;
                    using (var cs = new CryptoStream(ms, alg.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearData, 0, clearData.Length);
                    }
                }

                return ms.ToArray();
            }
        }

        private static byte[] Decryptbuffer(byte[] cipherData, byte[] key, byte[] iv)
        {
            if (cipherData == null)
            {
                throw new ArgumentNullException("cipherData");
            }

            if (key == null)
            {
                throw new ArgumentNullException("key");
            }

            if (iv == null)
            {
                throw new ArgumentNullException("iv");
            }

            using (var ms = new MemoryStream())
            {
                using (var alg = TripleDES.Create())
                {
                    alg.Key = key;
                    alg.IV = iv;
                    using (var cs = new CryptoStream(ms, alg.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherData, 0, cipherData.Length);
                    }
                }

                return ms.ToArray();
            }
        }
    }
}