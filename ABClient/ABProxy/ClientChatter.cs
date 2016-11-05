namespace ABClient.ABProxy
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Net.Sockets;
    using System.Text;
    using ABForms;
    using MyHelpers;

    internal sealed class ClientChatter
    {
        private readonly Session _session;

        private int _intBodySeekProgress;
        private int _intEntityBodyOffset;
        private MemoryStream _requestData;
        private string _hostFromURI;

        internal ClientChatter(Session session)
        {
            _session = session;
            _requestData = new MemoryStream(0x800);
        }

        internal HttpRequestHeaders Headers { get; private set; }

        internal Socket ClientSocket { get; set; }

        internal string Host
        {
            get
            {
                return Headers != null ? Headers["Host"] : string.Empty;
            }
        }

        internal string this[string strHeader]
        {
            set
            {
                if (Headers != null)
                {
                    Headers[strHeader] = value;
                }
            }
        }

        internal void FailSession(int intError, string strErrorStatusText, string strErrorBody)
        {
            if ((intError >= 400) && (strErrorBody.Length < 0x200))
            {
                strErrorBody = strErrorBody.PadRight(0x200, ' ');
            }

            _session.ResponseBodyBytes = Encoding.UTF8.GetBytes(strErrorBody);
            _session.Response.Headers = new HttpResponseHeaders
                                            {
                                                HttpResponseCode = intError,
                                                HttpResponseStatus = string.Format(
                                                    CultureInfo.InvariantCulture,
                                                    "{0} {1}",
                                                    intError,
                                                    strErrorStatusText)
                                            };
            _session.Response.Headers.Add("Content-Type", "text/html");
            _session.Response.Headers.Add("Connection", "close");
            _session.ReturnResponse(false);

            if (!string.IsNullOrEmpty(_session.Url))
            {
                var address = "http://" + _session.Url;
                try
                {
                    if (AppVars.MainForm != null)
                    {
                        AppVars.MainForm.BeginInvoke(
                            new UpdateTexLogDelegate(AppVars.MainForm.UpdateTexLog),
                            new object[] { "Ошибка с " + address });
                    }
                }
                catch (InvalidOperationException)
                {
                }
            }

            var sb = new StringBuilder(
                HelperErrors.Head() +
                "Connection error<br>" +
                @"<br><span class=""gray"">");
            sb.Append(intError);
            sb.Append(" ");
            sb.AppendLine(strErrorStatusText);
            sb.Append(strErrorBody);
            sb.Append(
                "</span>" +
                "</body>" +
                "</html>");

            if (!string.IsNullOrEmpty(_session.Url) && (_session.Url.Equals("www.neverlands.ru/") ||
                                                       _session.Url.StartsWith("www.neverlands.ru/index.cgi") ||
                                                       _session.Url.StartsWith("www.neverlands.ru/game.php") ||
                                                       _session.Url.StartsWith("www.neverlands.ru/main.php")))
            {
                AppVars.ContentMainPhp = sb.ToString();
            }

            intError = 200;
            strErrorStatusText = "OK";

            _session.ResponseBodyBytes = Encoding.UTF8.GetBytes(sb.ToString());
            _session.Response.Headers = new HttpResponseHeaders
            {
                HttpResponseCode = intError,
                HttpResponseStatus = string.Format(CultureInfo.InvariantCulture, "{0} {1}", intError, strErrorStatusText)
            };

            _session.Response.Headers.Add("Content-Type", "text/html");
            _session.Response.Headers.Add("Connection", "close");
            _session.ReturnResponse(false);
        }

        internal bool ReadRequest()
        {
            if (_requestData == null || ClientSocket == null)
            {
                return false;
            }

            int readCount = 0;
            bool flagReadError = false;
            bool flag2 = false;
            var data = new byte[0x2000];
            do
            {
                try
                {
                    readCount = ClientSocket.Receive(data);
                }
                catch (Exception)
                {
                    flagReadError = true;
                }

                if (readCount <= 0)
                {
                    flag2 = true;
                }
                else
                {
                    if (_requestData.Length == 0L)
                    {
                        int index = 0;
                        while ((index < readCount) && ((data[index] == 13) || (data[index] == 10)))
                        {
                            index++;
                        }

                        _requestData.Write(data, index, readCount - index);
                    }
                    else
                    {
                        _requestData.Write(data, 0, readCount);
                    }
                }
            }
            while ((!flag2 && !flagReadError) && !IsRequestComplete());

            data = null;
            if (flagReadError)
            {
                return false;
            }

            if (Headers == null)
            {
                return false;
            }

            if (_hostFromURI != null)
            {
                if (Headers.Exists("Host") && !Utilities.AreOriginsEquivalent(_hostFromURI, Headers["Host"], 80))
                {
                    Headers["Host"] = _hostFromURI;
                }
                else if (!Headers.Exists("Host"))
                {
                    Headers["Host"] = _hostFromURI;
                }
            }

            return Headers.Exists("Host");
        }

        internal byte[] TakeEntity()
        {
            byte[] bytes;
            try
            {
                bytes = new byte[_requestData.Length - _intEntityBodyOffset];
                _requestData.Position = _intEntityBodyOffset;
                _requestData.Read(bytes, 0, bytes.Length);
            }
            catch (OutOfMemoryException)
            {
                bytes = Encoding.ASCII.GetBytes("Out of memory");
            }

            _requestData.Dispose();
            _requestData = null;
            return bytes;
        }

        private bool HeadersAvailable()
        {
            byte[] buffer = _requestData.GetBuffer();
            long length = _requestData.Length;
            again:
            bool flag = false;
            while (_intBodySeekProgress < (length - 1L))
            {
                _intBodySeekProgress++;
                if (10 != buffer[_intBodySeekProgress - 1])
                {
                    continue;
                }

                flag = true;
                break;
            }

            if (flag)
            {
                if ((13 != buffer[_intBodySeekProgress]) && (10 != buffer[_intBodySeekProgress]))
                {
                    _intBodySeekProgress++;
                    goto again;
                }

                if (10 == buffer[_intBodySeekProgress])
                {
                    _intEntityBodyOffset = _intBodySeekProgress + 1;
                    return true;
                }

                _intBodySeekProgress++;
                if ((_intBodySeekProgress < length) && (10 == buffer[_intBodySeekProgress]))
                {
                    _intEntityBodyOffset = _intBodySeekProgress + 1;
                    return true;
                }

                if (_intBodySeekProgress > 3)
                {
                    _intBodySeekProgress -= 4;
                }
                else
                {
                    _intBodySeekProgress = 0;
                }
            }

            return false;
        }

        private bool IsRequestComplete()
        {
            if (Headers == null)
            {
                if (!HeadersAvailable())
                {
                    return false;
                }

                if (!ParseRequestForHeaders())
                {
                    if (Headers == null)
                    {
                        Headers = new HttpRequestHeaders { HttpMethod = "BAD" };
                        Headers["Host"] = "BAD-REQUEST";
                        Headers.RequestPath = "/BAD_REQUEST";
                    }

                    FailSession(400, "Bad Request", "Request Header parsing failed.");
                    return true;
                }
            }

            if (Headers == null)
            {
                return false;
            }

            if (Headers.ExistsAndEquals("Transfer-encoding", "chunked"))
            {
                return Utilities.IsChunkedBodyComplete(_requestData, _intEntityBodyOffset);
            }

            if (Headers.Exists("Content-Length"))
            {
                try
                {
                    long result;
                    if (!long.TryParse(Headers["Content-Length"], NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out result) || (result < 0L))
                    {
                        FailSession(400, "Bad Request", "Request Content-Length header parsing failed.");
                        return true;
                    }

                    if ((result == 0L) && ("GET" != Headers.HttpMethod))
                    {
                        FailSession(400, "Bad Request", "This HTTP method requires a request body.");
                    }

                    return _requestData.Length >= (_intEntityBodyOffset + result);
                }
                catch
                {
                    FailSession(400, "Bad Request", "Unknown error: Check content length header.");
                    return false;
                }
            }

            if ("GET" != Headers.HttpMethod)
            {
                FailSession(0x19b, "Bad Request", "This HTTP method requires a request body.");
            }

            return true;
        }

        private bool ParseRequestForHeaders()
        {
            if ((_requestData == null) || (_intEntityBodyOffset < 4))
            {
                return false;
            }

            Headers = new HttpRequestHeaders();
            byte[] bytes = _requestData.GetBuffer();
            int index = 0;
            int num2 = 0;
            int length = 0;
            int num4 = 0;
            do
            {
                switch (bytes[num4])
                {
                    case 0x20:
                        if (num2 == 0)
                        {
                            num2 = num4 + 1;
                        }
                        else if (length == 0)
                        {
                            length = num4 - num2;
                        }

                        break;
                    case 10:
                        index = num4 + 1;
                        break;
                }

                num4++;
            }
            while (index == 0);

            if ((num2 < 1) || (length < 1))
            {
                return false;
            }

            Headers.HttpMethod = Encoding.ASCII.GetString(bytes, 0, num2 - 1).ToUpper();
            Headers.HttpVersion = Encoding.ASCII.GetString(bytes, (num2 + length) + 1, ((index - length) - num2) - 2).Trim().ToUpper();
            int num5 = 0;
            if (((length > 7) && (bytes[num2 + 4] == 0x3a)) && ((bytes[num2 + 5] == 0x2f) && (bytes[num2 + 6] == 0x2f)))
            {
                num5 = num2 + 6;
                num2 += 7;
                length -= 7;
            }
            else if (((length > 8) && (bytes[num2 + 5] == 0x3a)) && ((bytes[num2 + 6] == 0x2f) && (bytes[num2 + 7] == 0x2f)))
            {
                num5 = num2 + 7;
                num2 += 8;
                length -= 8;
            }
            else if (((length > 6) && (bytes[num2 + 3] == 0x3a)) && ((bytes[num2 + 4] == 0x2f) && (bytes[num2 + 5] == 0x2f)))
            {
                num5 = num2 + 5;
                num2 += 6;
                length -= 6;
            }

            if (num5 > 0)
            {
                while ((length > 0) && (bytes[num2] != 0x2f))
                {
                    num2++;
                    length--;
                }

                if (length == 0)
                {
                    num2 = num5;
                    length = 1;
                }

                int num6 = num5 + 1;
                int count = num2 - num6;
                if (count > 0)
                {
                    _hostFromURI = AppVars.Codepage.GetString(bytes, num6, count);
                }
            }

            var destinationArray = new byte[length];
            Array.Copy(bytes, num2, destinationArray, 0, length);
            Headers.RawPath = destinationArray;
            string str = AppVars.Codepage.GetString(bytes, index, _intEntityBodyOffset - index).Trim();
            if (str.Length >= 1)
            {
                string[] strArray = str.Replace("\r\n", "\n").Split('\n');
                for (int i = 0; i < strArray.Length; i++)
                {
                    int num8 = strArray[i].IndexOf(':');
                    if ((num8 > 0) && (num8 <= (strArray[i].Length - 1)))
                    {
                        try
                        {
                            var headerName = strArray[i].Substring(0, num8);
                            var headerValue = strArray[i].Substring(num8 + 1).Trim();
                            Headers.Add(headerName, headerValue);
                        }
                        catch (Exception)
                        {
                            Headers = null;
                            return false;
                        }
                    }
                }
            }

            return true;
        }
    }
}