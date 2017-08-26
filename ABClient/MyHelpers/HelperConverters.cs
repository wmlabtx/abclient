namespace ABClient.MyHelpers
{
    using System;
    using System.Globalization;
    using System.Text;
    using System.Web;
    using Properties;

    internal static class HelperConverters
    {
        /*
        internal static string ByteArrayToHexView(byte[] array, int bytesPerLine)
        {
            return ByteArrayToHexView(array, bytesPerLine, array.Length);
        }

        private static string ByteArrayToHexView(byte[] array, int bytesPerLine, int len)
        {
            if ((array == null) || (len == 0))
            {
                return null;
            }

            if (len > array.Length)
            {
                len = array.Length;
            }

            var builder = new StringBuilder(len * 5);
            for (var i = 0; i < len; i += bytesPerLine)
            {
                var destinationArray = new byte[Math.Min(bytesPerLine, len - i)];
                Array.Copy(array, i, destinationArray, 0, destinationArray.Length);
                for (var j = 0; j < destinationArray.Length; j++)
                {
                    builder.Append(destinationArray[j].ToString("X2", CultureInfo.InvariantCulture));
                    builder.Append(" ");
                }

                if (destinationArray.Length < bytesPerLine)
                {
                    builder.Append(new string(' ', 3 * (bytesPerLine - destinationArray.Length)));
                }

                builder.Append(" ");
                for (var k = 0; k < destinationArray.Length; k++)
                {
                    if (destinationArray[k] < 0x20)
                    {
                        builder.Append(".");
                    }
                    else
                    {
                        builder.Append((char)destinationArray[k]);
                    }
                }

                if (destinationArray.Length < bytesPerLine)
                {
                    builder.Append(new string(' ', bytesPerLine - destinationArray.Length));
                }

                builder.Append("\r\n");
            }

            return builder.ToString();
        }
         */

        /*
        internal static bool TryHexParse(string input, out int output)
        {
            return int.TryParse(input, NumberStyles.HexNumber, NumberFormatInfo.InvariantInfo, out output);
        }

        internal static bool TryIntParse(string input, out int output)
        {
            return int.TryParse(input, out output);
        }

        internal static string LongToString(long size)
        {
            var str = size.ToString(CultureInfo.InvariantCulture) + " байт";
            if (size >= 1024)
            {
                var ksize = (double)size / 1024;
                str = ksize.ToString("0.0", CultureInfo.CurrentCulture) + " Кбайт";
                if (ksize >= 1024)
                {
                    ksize = ksize / 1024;
                    str = ksize.ToString("0.00", CultureInfo.CurrentCulture) + " Мбайт";
                }
            }

            return str;
        }
         */ 

        internal static string TimeIntervalToNow(long tick)
        {
            var dt = DateTime.FromBinary(tick);
            var ts = DateTime.Now - dt;
            var sb = new StringBuilder();
            if (ts.Days > 0)
            {
                sb.AppendFormat("{0}д ", ts.Days);
            }

            if (ts.Hours > 0)
            {
                sb.AppendFormat("{0}ч ", ts.Hours);
            }

            sb.AppendFormat("{0}мин", ts.Minutes);
            return sb.ToString();
        }

        internal static string TimeSpanToString(TimeSpan ts)
        {
            return ts.TotalHours >= 1
                       ? string.Format(
                             CultureInfo.InvariantCulture,
                             "({0}:{1:00}:{2:00})",
                             ts.Hours,
                             ts.Minutes,
                             ts.Seconds)
                       : (ts.TotalMinutes >= 1
                              ? string.Format(
                                    CultureInfo.InvariantCulture,
                                    "({0}:{1:00})",
                                    ts.Minutes,
                                    ts.Seconds)
                              : string.Format(
                                    CultureInfo.InvariantCulture,
                                    "(0:{0:00})",
                                    ts.Seconds));            
        }

        /*
        internal static string NickToProc(string url)
        {
            var sb = new StringBuilder(url);
            sb.Replace(" ", "%20");
            sb.Replace("+", "%2B");
            sb.Replace("#", "%23");
            sb.Replace("=", "%3D");
            return sb.ToString();
        }

        internal static string NickTxt2Url(string nick)
        {
            if (nick == null)
            {
                return null;
            }

            var s1 = nick.Replace('+', '|');
            var s2 = HttpUtility.UrlEncode(s1, Helpers.Russian.Codepage);
            if (s2 == null)
            {
                return null;
            }

            var s3 = s2.Replace("+", "%20");
            var s4 = s3.Replace("%7c", "%2b");
            return s4;
        }

        internal static string AddressToProc(string address)
        {
            if (address.StartsWith(Resources.AddressPInfo, StringComparison.OrdinalIgnoreCase))
            {
                return Resources.AddressPInfo + NickToProc(address.Substring(Resources.AddressPInfo.Length));
            }

            if (address.StartsWith(Resources.AddressPName, StringComparison.OrdinalIgnoreCase))
            {
                return Resources.AddressPName + NickToProc(address.Substring(Resources.AddressPName.Length));
            }

            return address;
        }
         */

        internal static string NickDecode(string nick)
        {
            if (nick == null)
                return null;

            var s = nick.Replace('+', ' ');
            var decodedUrl = HttpUtility.UrlDecode(s, Helpers.Russian.Codepage);
            var sb = new StringBuilder(decodedUrl);
            sb.Replace("|", " ");
            sb.Replace("%20", " ");
            sb.Replace("%2B", "+");
            sb.Replace("%23", "#");
            sb.Replace("%3D", "=");
            return sb.ToString();
        }

        public static string NickEncode(string nick)
        {
            if (nick == null)
                return null;

            var s = nick.Replace('+', '|');
            s = HttpUtility.UrlEncode(s, Helpers.Russian.Codepage);
            if (s == null)
                return null;

            s = s.Replace("+", "%20");
            s = s.Replace("%7c", "%2b");
            return s;
        }

        internal static string AddressEncode(string address)
        {
            if (address.StartsWith(Resources.AddressPInfo, StringComparison.OrdinalIgnoreCase))
            {
                return Resources.AddressPInfo + NickEncode(address.Substring(Resources.AddressPInfo.Length));
            }

            if (address.StartsWith(Resources.AddressPName, StringComparison.OrdinalIgnoreCase))
            {
                return Resources.AddressPName + NickEncode(address.Substring(Resources.AddressPName.Length));
            }

            const string pinfo = "http://neverlands.ru/pinfo.cgi?";
            if (address.StartsWith(pinfo, StringComparison.OrdinalIgnoreCase))
            {
                return pinfo + NickEncode(address.Substring(pinfo.Length));
            }

            return address;
        }

        internal static string MinsToStr(int mins)
        {
            return "(" + (mins/60) + ":" + (mins%60).ToString("00") + ":00)";
        }
    }
}