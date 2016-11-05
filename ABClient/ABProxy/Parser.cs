namespace ABClient.ABProxy
{
    using System;

    internal static class Parser
    {
        internal static HttpResponseHeaders ParseResponse(string strResponse)
        {
            var index = strResponse.IndexOf("\r\n\r\n", StringComparison.Ordinal);
            if (index < 1)
            {
                index = strResponse.Length;
            }

            if (index >= 1)
            {
                var strArray = strResponse.Substring(0, index).Replace("\r\n", "\n").Split('\n');
                if (strArray.Length < 1)
                {
                    return null;
                }

                var headers = new HttpResponseHeaders();
                var length = strArray[0].IndexOf(' ');
                if (length > 0)
                {
                    headers.HttpVersion = strArray[0].Substring(0, length).ToUpperInvariant();
                    strArray[0] = strArray[0].Substring(length + 1).Trim();
                    if (string.Compare(headers.HttpVersion, 0, "HTTP/", 0, 5, StringComparison.OrdinalIgnoreCase) != 0)
                    {
                        return null;
                    }

                    headers.HttpResponseStatus = strArray[0];
                    length = strArray[0].IndexOf(' ');

                    int code;
                    if (length > 0)
                    {
                        if (!int.TryParse(strArray[0].Substring(0, length).Trim(), out code))
                        {
                            return null;
                        }
                    }
                    else
                    {
                        if (!int.TryParse(strArray[0].Trim(), out code))
                        {
                            return null;
                        }
                    }

                    headers.HttpResponseCode = code;
                    for (var i = 1; i < strArray.Length; i++)
                    {
                        length = strArray[i].IndexOf(':');
                        if ((length > 0) && (length <= (strArray[i].Length - 1)))
                        {
                            headers.Add(strArray[i].Substring(0, length), strArray[i].Substring(length + 1).Trim());
                        }
                    }

                    return headers;
                }
            }

            return null;
        }
    }
}