namespace ABClient.ABProxy
{
    using System;
    using System.Collections;
    using System.Globalization;
    using System.IO;
    using System.Text;

    internal sealed class HttpRequestHeaders : HttpHeaders, ICloneable
    {
        private string _path;
        private byte[] _rawPath;

        internal HttpRequestHeaders()
        {
            HttpMethod = string.Empty;
            _path = string.Empty;
        }

        internal string HttpMethod { get; set; }

        internal byte[] RawPath
        {
            set
            {
                _rawPath = (byte[])value.Clone();
                _path = AppVars.Codepage.GetString(_rawPath);
            }
        }

        internal string RequestPath
        {
            get
            {
                return _path;
            }

            set
            {
                _path = value;
                _rawPath = AppVars.Codepage.GetBytes(_path);
            }
        }

        public object Clone()
        {
            var headers = (HttpRequestHeaders)MemberwiseClone();
            headers.Storage = new ArrayList(Storage.Count);
            foreach (HttpHeaderItem item in Storage)
            {
                headers.Storage.Add(item.Clone());
            }

            return headers;
        }

        public override string ToString()
        {
            return ToString(true, false, false);
        }

        internal byte[] ToByteArray(bool prependVerbLine, bool appendEmptyLine, bool includeProtocolInPath)
        {
            if (!prependVerbLine)
            {
                return AppVars.Codepage.GetBytes(ToString(false, appendEmptyLine, false));
            }

            var stream = new MemoryStream();
            var bytes = Encoding.ASCII.GetBytes(HttpMethod + " ");
            var buffer = Encoding.ASCII.GetBytes(" " + HttpVersion + "\r\n");
            var buffer3 = AppVars.Codepage.GetBytes(ToString(false, appendEmptyLine, false));
            stream.Write(bytes, 0, bytes.Length);
            if (includeProtocolInPath && !string.Equals("CONNECT", HttpMethod, StringComparison.OrdinalIgnoreCase))
            {
                var buffer4 = AppVars.Codepage.GetBytes("http://" + base["Host"]);
                stream.Write(buffer4, 0, buffer4.Length);
            }

            stream.Write(_rawPath, 0, _rawPath.Length);
            stream.Write(buffer, 0, buffer.Length);
            stream.Write(buffer3, 0, buffer3.Length);
            return stream.ToArray();
        }

        internal string ToString(bool prependVerbLine, bool appendEmptyLine)
        {
            return ToString(prependVerbLine, appendEmptyLine, false);
        }

        private string ToString(bool prependVerbLine, bool appendEmptyLine, bool includeProtocolInPath)
        {
            var builder = new StringBuilder(0x100);
            if (prependVerbLine)
            {
                if (includeProtocolInPath)
                {
                    builder.AppendFormat(
                        CultureInfo.InvariantCulture, 
                        "{0} http://{1}{2} {3}\r\n", 
                        HttpMethod, 
                        base["Host"], 
                        RequestPath, 
                        HttpVersion);
                }
                else
                {
                    builder.AppendFormat(
                        CultureInfo.InvariantCulture, 
                        "{0} {1} {2}\r\n", 
                        HttpMethod, 
                        RequestPath, 
                        HttpVersion);
                }
            }

            for (var i = 0; i < Storage.Count; i++)
            {
                builder.Append(((HttpHeaderItem)Storage[i]).Name + ": " + ((HttpHeaderItem)Storage[i]).Value + "\r\n");
            }

            if (appendEmptyLine)
            {
                builder.Append("\r\n");
            }

            return builder.ToString();
        }
    }
}