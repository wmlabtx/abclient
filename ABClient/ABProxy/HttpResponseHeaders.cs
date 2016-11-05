namespace ABClient.ABProxy
{
    using System;
    using System.Collections;
    using System.Text;

    internal sealed class HttpResponseHeaders : HttpHeaders, ICloneable
    {
        internal HttpResponseHeaders()
        {
            HttpResponseStatus = string.Empty;
        }

        internal int HttpResponseCode { get; set; }
        
        internal string HttpResponseStatus { get; set; }

        public object Clone()
        {
            var headers = (HttpResponseHeaders)MemberwiseClone();
            headers.Storage = new ArrayList(Storage.Count);
            foreach (HttpHeaderItem item in Storage)
            {
                headers.Storage.Add(item.Clone());
            }

            return headers;
        }

        public override string ToString()
        {
            return ToString(true, false);
        }

        internal string ToString(bool prependStatusLine, bool appendEmptyLine)
        {
            var builder = new StringBuilder(0x100);
            if (prependStatusLine)
            {
                builder.AppendFormat("HTTP/1.1 {0}\r\n", HttpResponseStatus);
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