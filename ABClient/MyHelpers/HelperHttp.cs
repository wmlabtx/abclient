namespace ABClient.MyHelpers
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.IO.Compression;
    using System.Net;
    using Helpers;

    internal static class HelperHttp
    {
        internal static bool IsChunkedBodyComplete(MemoryStream objData, int intEntityBodyOffset)
        {
            int num3;
            for (var i = intEntityBodyOffset; i < objData.Length; i += num3 + 2)
            {
                objData.Position = i;
                var buffer = new byte[0x20];
                objData.Read(buffer, 0, buffer.Length);
                var strInput = Russian.Codepage.GetString(buffer);
                var index = strInput.IndexOf("\r\n", StringComparison.Ordinal);
                if (index > -1)
                {
                    i += index + 2;
                    strInput = strInput.Substring(0, index);
                }
                else
                {
                    return false;
                }

                index = strInput.IndexOf(';');
                if (index > -1)
                {
                    strInput = strInput.Substring(0, index);
                }

                if (!HelperConverters.TryHexParse(strInput, out num3))
                {
                    return true;
                }

                if (num3 != 0)
                {
                    continue;
                }

                objData.Position = objData.Length;
                return true;
            }

            objData.Position = objData.Length;
            return false;
        }

        internal static bool HttpMethodRequiresBody(string strMethod)
        {
            if (!("PROPPATCH" == strMethod))
            {
                return "LOCK" == strMethod;
            }

            return true;
        }

        internal static byte[] DoUnchunk(byte[] writeData)
        {
            if ((writeData == null) || (writeData.Length == 0))
            {
                return null;
            }

            var stream = new MemoryStream(writeData.Length);
            var sourceIndex = 0;
            var flag = false;
            var destinationArray = new byte[0x20];
            while (!flag && (sourceIndex < (writeData.Length - 3)))
            {
                Array.Copy(writeData, sourceIndex, destinationArray, 0, Math.Min(destinationArray.Length, writeData.Length - sourceIndex));
                var s = Russian.Codepage.GetString(destinationArray, 0, Math.Min(destinationArray.Length, writeData.Length - sourceIndex));
                var index = s.IndexOf("\r\n", StringComparison.Ordinal);
                if (index > -1)
                {
                    sourceIndex += index + 2;
                    s = s.Substring(0, index);
                }
                else
                {
                    return writeData;
                }

                index = s.IndexOf(';');
                if (index > -1)
                {
                    s = s.Substring(0, index);
                }

                int count;
                if (!int.TryParse(s, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out count))
                {
                    count = 0;
                }

                if (count <= 0)
                {
                    flag = true;
                }
                else
                {
                    if (writeData.Length < (count + sourceIndex))
                    {
                        return null;
                    }

                    stream.Write(writeData, sourceIndex, count);
                    sourceIndex += count + 2;
                }
            }

            var buffer = new byte[stream.Length];
            stream.Position = 0L;
            stream.Read(buffer, 0, (int)stream.Length);
            return buffer;
        }

        internal static byte[] GZipExpand(byte[] compressedData)
        {
            if ((compressedData == null) || (compressedData.Length == 0))
            {
                return null;
            }

            try
            {
                var inner = new MemoryStream(compressedData);
                var stream2 = new MemoryStream();

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
            catch (InvalidDataException)
            {
                return null;
            }
        }

        internal static byte[] DeflateExpand(byte[] compressedData)
        {
            if ((compressedData == null) || (compressedData.Length == 0))
            {
                return null;
            }

            try
            {
                var inner = new MemoryStream(compressedData);
                var stream2 = new MemoryStream();

                using (var stream3 = new DeflateStream(inner, CompressionMode.Decompress))
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
            catch (InvalidDataException)
            {
                return null;
            }
        }

        internal static void CrackHostAndPort(string strHostPort, out string strHost, ref int intPort)
        {
            var length = strHostPort.LastIndexOf(':');
            if ((length > -1) && (length > strHostPort.LastIndexOf("]", StringComparison.Ordinal)))
            {
                if (!HelperConverters.TryIntParse(strHostPort.Substring(length + 1), out intPort))
                {
                    intPort = 0;
                }

                strHost = strHostPort.Substring(0, length);
            }
            else
            {
                strHost = strHostPort;
            }

            if (strHost.StartsWith("[", StringComparison.Ordinal) && strHost.EndsWith("]", StringComparison.Ordinal))
            {
                strHost = strHost.Substring(1, strHost.Length - 2);
            }
        }

        internal static IPAddress IPFromString(string host)
        {
            for (var i = 0; i < host.Length; i++)
            {
                if (((((host[i] != '.') && (host[i] != ':')) && ((host[i] < '0') || (host[i] > '9'))) && ((host[i] < 'A') || (host[i] > 'F'))) && ((host[i] < 'a') || (host[i] > 'f')))
                {
                    return null;
                }
            }

            try
            {
                return IPAddress.Parse(host);
            }
            catch (FormatException)
            {
                return null;
            }
        }

        internal static IPEndPoint IPEndpointFromHostPortString(string strHostAndPort)
        {
            if (string.IsNullOrEmpty(strHostAndPort))
            {
                return null;
            }
            
            if (strHostAndPort.IndexOf(';') > -1)
            {
                strHostAndPort = strHostAndPort.Substring(0, strHostAndPort.IndexOf(';'));
            }

            string str;
            var intPort = 80;
            CrackHostAndPort(strHostAndPort, out str, ref intPort);
            var address = IPFromString(str);
            if (address == null)
            {
                var hostEntry = Dns.GetHostEntry(str);
                address = hostEntry.AddressList[0];
            }

            return new IPEndPoint(address, intPort);
        }

        internal static bool IsLocalhost(string hostPort)
        {
            string str;
            var intPort = 0;
            CrackHostAndPort(hostPort, out str, ref intPort);
            if (((string.Compare(str, "localhost", StringComparison.OrdinalIgnoreCase) != 0) &&
                 (string.Compare(str, "localhost.", StringComparison.OrdinalIgnoreCase) != 0)) &&
                (string.Compare(str, "127.0.0.1", StringComparison.Ordinal) != 0))
            {
                return string.Compare(str, "::1", StringComparison.Ordinal) == 0;
            }

            return true;
        }

        /*
        internal static bool IsTopFrameUrl(string address)
        {
            return address.StartsWith("http://www.neverlands.ru/main.php", StringComparison.OrdinalIgnoreCase);
        }

        internal static bool IsCriticalUrl(string address)
        {
            return
                address.Equals("http://www.neverlands.ru/", StringComparison.OrdinalIgnoreCase) ||
                address.Equals("http://www.neverlands.ru/index.cgi", StringComparison.OrdinalIgnoreCase) ||
                address.StartsWith("http://www.neverlands.ru/game.php", StringComparison.OrdinalIgnoreCase);
        }
         */ 
    }
}