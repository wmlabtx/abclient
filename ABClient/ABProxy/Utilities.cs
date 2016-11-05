namespace ABClient.ABProxy
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.IO.Compression;
    using System.Net;
    using System.Text;

    internal static class Utilities
    {
        internal static IPEndPoint IPEndPointFromHostPortString(string hostAndPort)
        {
            if ((hostAndPort == null) || string.IsNullOrEmpty(hostAndPort.Trim()))
            {
                return null;
            }

            if (hostAndPort.IndexOf(';') > -1)
            {
                hostAndPort = hostAndPort.Substring(0, hostAndPort.IndexOf(';'));
            }

            try
            {
                string str;
                int port = 80;
                CrackHostAndPort(hostAndPort, out str, ref port);
                return new IPEndPoint(DNSResolver.GetIPAddress(str, true), port);
            }
            catch (Exception)
            {
                return null;
            }
        }

        internal static IPAddress IPFromString(string host)
        {
            for (int i = 0; i < host.Length; i++)
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
            catch
            {
                return null;
            }
        }

        internal static bool IsChunkedBodyComplete(MemoryStream data, int entityBodyOffset)
        {
            int num3;
            for (int i = entityBodyOffset; i < data.Length; i += num3 + 2)
            {
                data.Position = i;
                var buffer = new byte[0x20];
                data.Read(buffer, 0, buffer.Length);
                var input = Encoding.ASCII.GetString(buffer);
                int index = input.IndexOf("\r\n", StringComparison.Ordinal);
                if (index > -1)
                {
                    i += index + 2;
                    input = input.Substring(0, index);
                }
                else
                {
                    return false;
                }

                index = input.IndexOf(';');
                if (index > -1)
                {
                    input = input.Substring(0, index);
                }

                num3 = 0;
                if (!int.TryParse(input, NumberStyles.HexNumber, NumberFormatInfo.InvariantInfo, out num3))
                {
                    return true;
                }

                if (num3 != 0)
                {
                    continue;
                }

                data.Position = data.Length;
                return true;
            }

            data.Position = data.Length;
            return false;
        }

        internal static bool AreOriginsEquivalent(string host1, string host2, int defaultPort)
        {
            if (string.Equals(host1, host2, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            if (!host1.Contains(":"))
            {
                host1 = string.Format(
                    CultureInfo.InvariantCulture,
                    "{0}:{1}",
                    host1,
                    defaultPort);
            }

            if (!host2.Contains(":"))
            {
                host2 = string.Format(
                    CultureInfo.InvariantCulture,
                    "{0}:{1}",
                    host2,
                    defaultPort);
            }

            return string.Equals(host1, host2, StringComparison.OrdinalIgnoreCase);
        }

        internal static void CrackHostAndPort(string hostPort, out string host, ref int port)
        {
            int length = hostPort.LastIndexOf(':');
            if ((length > -1) && (length > hostPort.LastIndexOf(']')))
            {
                if (!int.TryParse(hostPort.Substring(length + 1), NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out port))
                {
                    port = -1;
                }

                host = hostPort.Substring(0, length);
            }
            else
            {
                host = hostPort;
            }

            if (host.StartsWith("[", StringComparison.Ordinal) && host.EndsWith("]", StringComparison.Ordinal))
            {
                host = host.Substring(1, host.Length - 2);
            }
        }


        internal static byte[] DoUnchunk(byte[] writeData)
        {
            if ((writeData == null) || (writeData.Length == 0))
            {
                return new byte[0];
            }

            using (var stream = new MemoryStream(writeData.Length))
            {
                int sourceIndex = 0;
                bool flag = false;
                var destinationArray = new byte[0x20];
                while (!flag && (sourceIndex < (writeData.Length - 3)))
                {
                    Array.Copy(writeData, sourceIndex, destinationArray, 0, Math.Min(destinationArray.Length, writeData.Length - sourceIndex));
                    string s = AppVars.Codepage.GetString(destinationArray, 0, Math.Min(destinationArray.Length, writeData.Length - sourceIndex));
                    int index = s.IndexOf("\r\n", StringComparison.Ordinal);
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

                    int count = int.Parse(s, NumberStyles.HexNumber);
                    if (count == 0)
                    {
                        flag = true;
                    }
                    else
                    {
                        if (writeData.Length < (count + sourceIndex))
                        {
                            return new byte[0];
                        }

                        stream.Write(writeData, sourceIndex, count);
                        sourceIndex += count + 2;
                    }
                }

                var buffer = new byte[stream.Length];
                stream.Position = 0L;
                stream.Read(buffer, 0, (int) stream.Length);
                return buffer;
            }
        }

        internal static byte[] GzipExpand(byte[] compressedData)
        {
            if ((compressedData == null) || (compressedData.Length == 0))
            {
                return null;
            }

            try
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
            catch (Exception)
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
                using (var inner = new MemoryStream(compressedData))
                {
                    using (var stream2 = new MemoryStream())
                    {
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
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}