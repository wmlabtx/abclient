namespace ABClient.MyHelpers
{
    using System;
    using System.IO;
    using System.IO.Compression;
    using System.Text;

    internal static class HelperPacks
    {
        public static byte[] PackArray(byte[] writeData)
        {
            using (var inner = new MemoryStream())
            {
                using (var stream2 = new GZipStream(inner, CompressionMode.Compress))
                {
                    stream2.Write(writeData, 0, writeData.Length);
                }

                return inner.ToArray();
            }
        }
        
        public static byte[] UnpackArray(byte[] compressedData)
        {
            using (var inner = new MemoryStream(compressedData))
            {
                using (var stream2 = new MemoryStream())
                {
                    using (var stream3 = new GZipStream(inner, CompressionMode.Decompress))
                    {
                        var buffer = new byte[0x8000];
                        int count;
                        while ((count = stream3.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            stream2.Write(buffer, 0, count);
                        }
                    }

                    return stream2.ToArray();
                }
            }
        }

        public static string PackString(string data)
        {
            var buffer = Encoding.UTF8.GetBytes(data);
            var pack = PackArray(buffer);
            return Convert.ToBase64String(pack);
        }

        public static string UnpackString(string data)
        {
            var pack = Convert.FromBase64String(data);
            var buffer = UnpackArray(pack);
            return Encoding.UTF8.GetString(buffer);
        }
    }
}