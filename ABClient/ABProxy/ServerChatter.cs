namespace ABClient.ABProxy
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.Web;
    using ABForms;

    internal sealed class ServerChatter
    {
        private readonly Session _session;

        private bool _leakedHeaders;
        private long _lngLeakedOffset;
        private int _intBodySeekProgress;
        private int _intEntityBodyOffset;
        private MemoryStream _responseData;

        internal ServerChatter(Session session)
        {
            _session = session;
            _responseData = new MemoryStream(0x8000);
        }

        internal ServerChatter(Session session, string strHeaders)
        {
            _session = session;
            Headers = Parser.ParseResponse(strHeaders);
        }

        internal Socket ServerSocket { get; set; }

        internal bool WasForwarded { get; set; }

        internal HttpResponseHeaders Headers { get; set; }

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

        internal void AssignData(byte[] data)
        {
            Headers.HttpResponseCode = 200;
            Headers.HttpResponseStatus = "200 OK without Headers";
            Headers["Content-Length"] = data.LongLength.ToString(CultureInfo.InvariantCulture);
            _session.ResponseBodyBytes = data;
        }

        internal bool ReadResponse()
        {
            bool flag = false;
            bool flag2 = false;
            bool flag3 = !_session.BufferResponse;
            bool flag4 = true;
            var data = new byte[0x8000];
            do
            {
                try
                {
                    if (flag4 && (Headers != null))
                    {
                        flag4 = false;
                        flag3 = !_session.BufferResponse;
                    }

                    int readCount = ServerSocket.Receive(data);
                    if (readCount <= 0)
                    {
                        flag = true;
                    }
                    else
                    {
                        _responseData.Write(data, 0, readCount);
                        if (flag3)
                        {
                            LeakResponseBytes();
                        }
                    }
                }
                catch (Exception)
                {
                    flag2 = true;
                }
            }
            while ((!flag && !IsResponseComplete()) && !flag2);

            if (_responseData.Length == 0L)
            {
                flag2 = true;
            }
            else
            {
                try
                {
                    if (AppVars.MainForm != null)
                    {
                        AppVars.MainForm.UpdateTrafficSafe(_responseData.Length);
                    }
                }
                catch (InvalidOperationException)
                {
                }
            }

            if ((Headers == null) && !flag2)
            {
                Headers = new HttpResponseHeaders
                              {
                                  HttpResponseCode = 200,
                                  HttpResponseStatus = "200 This buggy server did not return headers"
                              };

                _intEntityBodyOffset = 0;
                return true;
            }

            if (Headers != null && AppVars.Profile.DoHttpLog)
            {
                var sb = new StringBuilder();
                /*
                if (_session != null)
                {
                    sb.AppendLine("Session #" + _session.Id.ToString(CultureInfo.InvariantCulture));
                }
                 */ 

                sb.Append(Headers);
                try
                {
                    if (AppVars.MainForm != null)
                    {
                        AppVars.MainForm.BeginInvoke(
                            new UpdateHttpLogDelegate(AppVars.MainForm.UpdateHttpLog),
                            new object[] { sb.ToString() });
                    }
                }
                catch (InvalidOperationException)
                {
                }
            }

            return !flag2;
        }

        internal bool ResendRequest(int deep)
        {
            if (deep > 2)
            {
                return false;
            }

            if ((ServerSocket == null) && !ConnectToHost())
            {
                return false;
            }

            try
            {
                if (!WasForwarded)
                {
                    _session.Request.Headers.RenameHeaderItems("Proxy-Connection", "Connection");
                }

                if (!_session.Request.Headers.Exists("Accept-encoding"))
                {
                    _session.Request.Headers.Add("Accept-encoding", "gzip");
                }

                var headerarray = _session.Request.Headers.ToByteArray(true, true, WasForwarded);
                if (AppVars.Profile.DoHttpLog)
                {
                    var sb = new StringBuilder();
                    /*sb.AppendLine("Session #" + _session.Id.ToString(CultureInfo.InvariantCulture));*/
                    sb.Append(AppVars.Codepage.GetString(headerarray));
                    if (_session.RequestBodyBytes != null)
                    {
                        sb.AppendLine(AppVars.Codepage.GetString(_session.RequestBodyBytes));
                    }

                    try
                    {
                        if (AppVars.MainForm != null)
                        {
                            AppVars.MainForm.BeginInvoke(
                                new UpdateHttpLogDelegate(AppVars.MainForm.UpdateHttpLog),
                                new object[] { sb.ToString() });
                        }
                    }
                    catch (InvalidOperationException)
                    {
                    }
                }

                if (ServerSocket != null && _session != null && _session.RequestBodyBytes != null)
                {
                    ServerSocket.Send(headerarray);
                    ServerSocket.Send(_session.RequestBodyBytes);
                }
            }
            catch (SocketException)
            {
                ServerSocket = null;
                return ResendRequest(deep + 1);
            }

            return true;
        }

        internal byte[] TakeEntity()
        {
            byte[] bytes;
            try
            {
                bytes = new byte[_responseData.Length - _intEntityBodyOffset];
            }
            catch (OutOfMemoryException)
            {
                bytes = Encoding.ASCII.GetBytes("Out of memory");
            }

            _responseData.Position = _intEntityBodyOffset;
            _responseData.Read(bytes, 0, bytes.Length);
            _responseData.Dispose();
            _responseData = null;
            return bytes;
        }

        private bool ConnectToHost()
        {
            string str;
            int port = _session.Port;
            string hostPort = _session.Host;
            Utilities.CrackHostAndPort(hostPort, out str, ref port);
            var remoteEp = Proxy.Gateway;
            if (remoteEp != null)
            {
                WasForwarded = true;
            }
            else
            {
                IPAddress address;
                try
                {
                    address = DNSResolver.GetIPAddress(str, true);
                }
                catch
                {
                    _session.Request.FailSession(0x1f6, "DNS Lookup Failed", "DNS Lookup for " + HttpUtility.HtmlEncode(str) + " failed.");
                    return false;
                }

                if ((port < 0) || (port > 0xffff))
                {
                    _session.Request.FailSession(0x1f6, "Invalid Request", "HTTP Request specified an invalid port number.");
                    return false;
                }

                remoteEp = new IPEndPoint(address, port);
            }

            try
            {
                var socket = new Socket(remoteEp.AddressFamily, SocketType.Stream, ProtocolType.Tcp)
                                 {
                                     NoDelay = true,
                                     ReceiveTimeout = 10000,
                                     SendTimeout = 10000
                                 };
                ServerSocket = socket;
                ServerSocket.Connect(remoteEp);
                return true;
            }
            catch (Exception exception)
            {
                if (WasForwarded)
                {
                    _session.Request.FailSession(
                        0x1f6, 
                        "Gateway Connection Failed", 
                        string.Format(CultureInfo.InvariantCulture, "Connection to Gateway failed.<BR>Exception Text: {0}", exception.Message));
                }
                else
                {
                    _session.Request.FailSession(
                        0x1f6, 
                        "Connection Failed",
                        string.Format(CultureInfo.InvariantCulture, "Connection to {0} failed.<BR>Exception Text: {1}", HttpUtility.HtmlEncode(str), exception.Message));
                }

                return false;
            }
        }

        private void DeleteInformationalMessage()
        {
            Headers = null;
            var buffer = new byte[_responseData.Length - _intEntityBodyOffset];
            _responseData.Position = _intEntityBodyOffset;
            _responseData.Read(buffer, 0, buffer.Length);
            _responseData.Dispose();
            _responseData = new MemoryStream(buffer.Length);
            _responseData.Write(buffer, 0, buffer.Length);
            _intEntityBodyOffset = 0;
            _intBodySeekProgress = 0;
        }

        private bool HeadersAvailable()
        {
            if (_intEntityBodyOffset > 0)
            {
                return true;
            }

            if (_responseData == null)
            {
                return false;
            }

            var buffer = _responseData.GetBuffer();
            var length = _responseData.Length;
            again:
            var flag = false;
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

        private bool IsResponseComplete()
        {
            if (Headers == null)
            {
                if (!HeadersAvailable())
                {
                    return false;
                }

                if (!ParseResponseForHeaders())
                {
                    _session.Request.FailSession(500, "Bad Response", "Response Header parsing failed. This can be caused by an illegal HTTP response earlier on this socket, like a 304 response which contains a body.");
                    return true;
                }
            }

            if (Headers != null)
            {
                if (Headers != null && Headers.HttpResponseCode == 100)
                {
                }
                else
                {
                    if (Headers != null && ((Headers.HttpResponseCode > 0x63) && (Headers.HttpResponseCode < 200)))
                    {
                        DeleteInformationalMessage();
                        return IsResponseComplete();
                    }
                }

                if (_session.Request.Headers.HttpMethod == "HEAD")
                {
                    return true;
                }

                if (Headers != null && (Headers.HttpResponseCode == 0xcc || Headers.HttpResponseCode == 0xcd || Headers.HttpResponseCode == 0x130))
                {
                    return true;
                }

                if (Headers != null && Headers.ExistsAndEquals("Transfer-encoding", "chunked"))
                {
                    return Utilities.IsChunkedBodyComplete(_responseData, _intEntityBodyOffset);
                }

                if (Headers != null && Headers.Exists("Content-Length"))
                {
                    long num;
                    if (!long.TryParse(Headers["Content-Length"], NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out num) || (num < 0L))
                    {
                        return true;
                    }

                    return _responseData.Length >= (_intEntityBodyOffset + num);
                }
            }

            return false;
        }

        private void LeakResponseBytes()
        {
            if ((_session.Request == null) || (_session.Request.ClientSocket == null))
            {
                return;
            }

            if (!_leakedHeaders && !HeadersAvailable())
            {
                return;
            }

            again:
            if (!_leakedHeaders && HeadersAvailable())
            {
                if (!ParseResponseForHeaders())
                {
                    return;
                }

                if ((Headers.HttpResponseCode > 0x63) && (Headers.HttpResponseCode < 200))
                {
                    DeleteInformationalMessage();
                    goto again;
                }

                if (((0x191 == Headers.HttpResponseCode) && Headers["WWW-Authenticate"].StartsWith("N", StringComparison.OrdinalIgnoreCase)) || 
                    ((0x197 == Headers.HttpResponseCode) && Headers["Proxy-Authenticate"].StartsWith("N", StringComparison.OrdinalIgnoreCase)))
                {
                    Headers["Proxy-Support"] = "Session-Based-Authentication";
                }

                _session.Request.ClientSocket.Send(AppVars.Codepage.GetBytes(Headers.ToString(true, true)));
                _leakedHeaders = true;
                _lngLeakedOffset = _intEntityBodyOffset;
            }

            var data = _responseData.GetBuffer();
            if (data != null)
            {
                var offset = (int) _lngLeakedOffset;
                var count = (int)(_responseData.Length - _lngLeakedOffset);
                if ((offset + count) > data.LongLength)
                {
                    count = data.Length - offset;
                }

                if (count >= 1)
                {
                    _session.Request.ClientSocket.Send(data, offset, count, SocketFlags.None);
                }
            }
 
            _lngLeakedOffset = _responseData.Length;
        }

        private bool ParseResponseForHeaders()
        {
            if ((_responseData != null) && (_intEntityBodyOffset >= 4))
            {
                Headers = new HttpResponseHeaders();
                byte[] bytes = _responseData.GetBuffer();
                string str = AppVars.Codepage.GetString(bytes, 0, _intEntityBodyOffset).Trim();
                if (string.IsNullOrEmpty(str))
                {
                    Headers = null;
                    return false;
                }

                string[] strArray = str.Replace("\r\n", "\n").Split('\n');
                if (strArray.Length >= 1)
                {
                    int index = strArray[0].IndexOf(' ');
                    if (index > 0)
                    {
                        Headers.HttpVersion = strArray[0].Substring(0, index).ToUpper();
                        strArray[0] = strArray[0].Substring(index + 1).Trim();
                        if (!Headers.HttpVersion.StartsWith("HTTP/", StringComparison.OrdinalIgnoreCase))
                        {
                            return false;
                        }

                        Headers.HttpResponseStatus = strArray[0];
                        index = strArray[0].IndexOf(' ');
                        int responseCode;
                        bool flag = index > 0 ?
                                                  int.TryParse(strArray[0].Substring(0, index).Trim(), NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out responseCode) :
                                                                                                                                                                                   int.TryParse(strArray[0].Trim(), NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out responseCode);
                        Headers.HttpResponseCode = responseCode;

                        if (!flag)
                        {
                            return false;
                        }

                        for (int i = 1; i < strArray.Length; i++)
                        {
                            index = strArray[i].IndexOf(':');
                            if ((index > 0) && (index <= (strArray[i].Length - 1)))
                            {
                                try
                                {
                                    Headers.Add(strArray[i].Substring(0, index), strArray[i].Substring(index + 1).Trim());
                                }
                                catch
                                {
                                    return false;
                                }
                            }
                        }

                        return true;
                    }
                }
            }

            return false;
        }
    }
}