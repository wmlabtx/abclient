namespace ABClient.ABProxy
{
    using System;
    using System.Globalization;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading;
    using ABForms;
    using MyHelpers;
    using PostFilter;

    internal sealed class Session
    {
        private Session _nextSession;
        private bool _isCustomFilter;
        private bool _isCache;
        private bool _isGameHost;

        private Session(Socket clientSocket, Socket serverSocket, bool boolUseUpstreamProxySemantics)
        {
            BufferResponse = true;
            Request = new ClientChatter(this)
                          {
                              ClientSocket = clientSocket
                          };

            Response = new ServerChatter(this)
                           {
                               ServerSocket = serverSocket,
                               WasForwarded = boolUseUpstreamProxySemantics
                           };
        }

        internal ClientChatter Request { get; private set; }

        internal byte[] RequestBodyBytes { get; private set; }

        internal ServerChatter Response { get; private set; }

        internal bool BufferResponse { get; private set; }

        internal string Host
        {
            get { return Request.Headers != null ? Request.Host : string.Empty; }
        }

        internal int Port
        {
            get
            {
                string str;
                var intPort = 80;
                Utilities.CrackHostAndPort(Request.Host, out str, ref intPort);
                return intPort;
            }
        }

        internal byte[] ResponseBodyBytes { private get; set; }

        internal string Url
        {
            get
            {
                return string.Concat(Host, PathAndQuery);
            }
        }

        private string PathAndQuery
        {
            get
            {
                return Request.Headers != null ? Request.Headers.RequestPath : string.Empty;
            }
        }

        public override string ToString()
        {
            return ToString(false);
        }

        internal static void CreateAndExecute(object oParams)
        {
            try
            {
                var clientSocket = (Socket)oParams;
                clientSocket.NoDelay = true;
                var session = new Session(clientSocket, null, false);
                session.Execute(null);
            }
            catch
            {
            }
        }

        private void Execute(object objThreadstate)
        {
            var flag = false;
            var flag2 = false;
            var flagsavedtrafic = false;
            if ((Request != null) && (Response != null))
            {
                if (!Request.ReadRequest())
                {
                    CloseSessionPipes();
                    return;
                }

                RequestBodyBytes = Request.TakeEntity();
                ExecuteBasicRequestManipulations();

                RequestBodyBytes = Filter.PreProcess("http://" + Url, RequestBodyBytes);

                if (Url.StartsWith("www.neverlands.ru/cgi-bin/go.cgi?uid=", StringComparison.OrdinalIgnoreCase))
                {
                    flag = true;
                    Response = new ServerChatter(this, "HTTP/1.1 304 Not Modified\r\nServer: Cache\r\n\r\n");
                    goto afterresponse;
                }

                if (Url.Contains("top.list.ru") || Url.Contains("counter.yadro.ru"))
                {
                    flag = true;
                    Response = new ServerChatter(this, "HTTP/1.1 304 Not Modified\r\nServer: Cache\r\n\r\n");
                    goto afterresponse;
                }                

                if (!string.IsNullOrEmpty(Proxy.BasicAuth))
                {
                    Request.Headers.Add("Proxy-Authorization", Proxy.BasicAuth);
                }

                if (_isCustomFilter)
                {
                    if (Request.Headers.Exists("If-Modified-Since"))
                    {
                        Request.Headers.Remove("If-Modified-Since");
                    }

                    if (Request.Headers.Exists("If-None-Match"))
                    {
                        Request.Headers.Remove("If-None-Match");
                    }
                }
                else
                {
                    if (Request.Headers.Exists("If-Modified-Since") || Request.Headers.Exists("If-None-Match"))
                    {
                        flag = true;
                        Response = new ServerChatter(this, "HTTP/1.1 304 Not Modified\r\nServer: Cache\r\n\r\n");
                        goto afterresponse;
                    }

                    flagsavedtrafic = true;
                }

                Request.Headers.Remove("Cookie");

                var cookiedata = CookiesManager.Obtain(Host);
                if (!string.IsNullOrEmpty(cookiedata))
                {
                    Request.Headers.Add("Cookie", cookiedata);
                }

                if (_isCache)
                {
                    var data = Cache.Get(Url, AppVars.CacheRefresh);
                    if (data != null)
                    {
                        if (flagsavedtrafic)
                        {
                            try
                            {
                                if (AppVars.MainForm != null)
                                {
                                    AppVars.MainForm.UpdateSavedTrafficSafe(data.Length);
                                }
                            }
                            catch (InvalidOperationException)
                            {
                            }
                        }

                        flag = true;
                        Response = new ServerChatter(this, "HTTP/1.1 200 OK\r\nServer: Cache\r\n\r\n");
                        if (_isCustomFilter)
                        {
                            _isCustomFilter = false;
                            var pdata = Filter.Process("http://" + Url, data);
                            Response.AssignData(pdata);
                        }
                        else
                        {
                            Response.AssignData(data);
                        }

                        _isCache = false;
                        goto afterresponse;
                    }
                }

                // Begin of request

                IdleManager.AddActivity();

                try
                {
                    if (AppVars.MainForm != null)
                    {
                        AppVars.MainForm.AddAddressToStatusString(Url);
                    }
                }
                catch (InvalidOperationException)
                {
                }

                flag = Response.ResendRequest(0);
                if (flag)
                {
                    flag = Response.ReadResponse();
                }

                if (!flag)
                {
                    CloseSessionPipes();
                }
                else
                {
                    ResponseBodyBytes = Response.TakeEntity();
                }

                try
                {
                    if (AppVars.MainForm != null)
                    {
                        AppVars.MainForm.RemoveAddressFromStatusString(Url);
                    }
                }
                catch (InvalidOperationException)
                {
                }

                // End of request

                IdleManager.RemoveActivity();
            }

            afterresponse:
            ExecuteBasicResponseManipulations();

            if (Response != null)
            {
                if (Url.StartsWith("www.neverlands.ru/main.php", StringComparison.OrdinalIgnoreCase))
                {
                    if (Response.Headers.Exists("Date"))
                    {
                        var stringDateTime = Response.Headers["Date"];
                        DateTime serverDateTime;
                        if (DateTime.TryParse(stringDateTime, AppVars.EnUsCulture, DateTimeStyles.None, out serverDateTime))
                        {
                            try
                            {
                                if (AppVars.MainForm != null)
                                {
                                    AppVars.MainForm.BeginInvoke(
                                        new UpdateServerTimeDelegate(AppVars.MainForm.UpdateServerTime),
                                        new object[] { serverDateTime });
                                }
                            }
                            catch (InvalidOperationException)
                            {
                            }
                        }
                    }
                }

                for (var i = 0; i < Response.Headers.Count(); i++)
                {
                    if (Response.Headers[i].Name.Equals("Set-Cookie", StringComparison.OrdinalIgnoreCase))
                    {
                        CookiesManager.Assign(Host, Response.Headers[i].Value);
                    }
                }

                Response.Headers.Remove("Set-Cookie");
                if (Response != null && Response.Headers != null && Response.Headers.HttpResponseCode == 200)
                {
                    if (Url.StartsWith("www.neverlands.ru/modules/code/code.php?", StringComparison.OrdinalIgnoreCase))
                    {
                        AppVars.CodePng = ResponseBodyBytes;
                    }
                    else
                    {
                        if (_isCache)
                        {
                            Cache.Store(Url, ResponseBodyBytes, _isGameHost);
                        }

                        if (_isCustomFilter && ResponseBodyBytes != null)
                        {
                            var pdata = Filter.Process("http://" + Url, ResponseBodyBytes);
                            Response.AssignData(pdata);
                        }
                    }
                }
                else
                {
                    if (Response != null && Response.Headers != null && Response.Headers.HttpResponseCode >= 400 & Response.Headers.HttpResponseCode != 404 && _isCustomFilter)
                    {
                        var sb = new StringBuilder(
                            HelperErrors.Head() +
                            "Отказ сервера <br>" +
                            @"<br><span class=""gray"">");
                        sb.AppendLine(Response.Headers.HttpResponseStatus);
                        sb.Append(
                            "</span>" +
                            "</body>" +
                            "</html>");

                        Response.AssignData(Encoding.UTF8.GetBytes(sb.ToString()));
                        Response.Headers = new HttpResponseHeaders
                                               {
                                                   HttpResponseCode = 200,
                                                   HttpResponseStatus = "OK"
                                               };
                    }
                }
            }

            var flag3 = false;
            if (!BufferResponse)
            {
                flag3 = true;
            }

            if (Response != null && Response.Headers != null)
            {
                if (((Response.Headers.HttpResponseCode == 0x191) &&
                     (string.Compare(Response.Headers["WWW-Authenticate"], 0, "N", 0, 1, StringComparison.OrdinalIgnoreCase) == 0)) ||
                    ((Response.Headers.HttpResponseCode == 0x197) &&
                     (string.Compare(Response.Headers["Proxy-Authenticate"], 0, "N", 0, 1, StringComparison.OrdinalIgnoreCase) == 0)))
                {
                    flag2 = DoNTLM(0x197 == Response.Headers.HttpResponseCode);
                }
                else if (Response.ServerSocket != null)
                {
                    if (Response.Headers.ExistsAndEquals("Connection", "close") || 
                        (!Response.ServerSocket.Connected || (Response.Headers.HttpVersion != "HTTP/1.1")))
                    {
                        if (Response.ServerSocket.Connected)
                        {
                            try
                            {
                                if (Response.ServerSocket != null)
                                {
                                    Response.ServerSocket.Shutdown(SocketShutdown.Both);
                                    Response.ServerSocket.Close();
                                }
                            }
                            catch (Exception)
                            {
                            }

                            Response.ServerSocket = null;
                        }
                    }

                    Response.ServerSocket = null;
                }
            }

            if (!flag3)
            {
                if (flag)
                {
                    ReturnResponse(flag2);
                }

                /*
                if (!flag)
                {
                    if (Request != null)
                        if (Response != null)
                            Request.FailSession(400, "Bad Response", "Reading response failed. Response was:\n" + Response);
                }
                 */ 
            }

            if (Request != null && ((Request.ClientSocket != null) && flag3))
            {
                if (Response != null && Response.Headers != null)
                {
                    if (flag2 ||
                        (((!Response.Headers.ExistsAndEquals("Connection", "close")) &&
                          !Request.Headers.ExistsAndEquals("Connection", "close")) &&
                         ((Response.Headers.HttpVersion == "HTTP/1.1") ||
                          Response.Headers.ExistsAndContains("Connection", "Keep-Alive"))))
                    {
                        if (!flag2)
                        {
                            Thread.Sleep(0x19);
                        }

                        if (flag2 || (0 < Request.ClientSocket.Available))
                        {
                            _nextSession = flag2 ? 
                                new Session(Request.ClientSocket, Response.ServerSocket, Response.WasForwarded) : 
                                new Session(Request.ClientSocket, null, false);

                            Request.ClientSocket = null;
                            goto next;
                        }
                    }
                }

                try
                {
                    if (Request.ClientSocket != null)
                    {
                        Request.ClientSocket.Shutdown(SocketShutdown.Both);
                        Request.ClientSocket.Close();
                    }
                }
                catch (Exception)
                {
                }

                Request.ClientSocket = null;
            }

            next:
            if (_nextSession != null)
            {
                _nextSession.Execute(null);
            }
        }

        internal bool ReturnResponse(bool boolKeepNTLMSockets)
        {
            var flag = false;
            try
            {
                if ((Request.ClientSocket != null) && Request.ClientSocket.Connected)
                {
                    var responseheader = Response.Headers.ToString(true, true);
                    
                    /*
                    System.Net.Sockets.SocketException occurred 
                    Message=An existing connection was forcibly closed by the remote host
                    Source=System
                    ErrorCode=10054
                    NativeErrorCode=10054
                    */

                    Request.ClientSocket.Send(AppVars.Codepage.GetBytes(responseheader));
                    if (ResponseBodyBytes != null)
                    {
                        Request.ClientSocket.Send(ResponseBodyBytes);
                    }

                    if (boolKeepNTLMSockets || (((!Response.Headers.ExistsAndEquals("Connection", "close")) && !Request.Headers.ExistsAndEquals("Connection", "close")) && ((Response.Headers.HttpVersion == "HTTP/1.1") || Response.Headers.ExistsAndContains("Connection", "Keep-Alive"))))
                    {
                        if (!boolKeepNTLMSockets)
                        {
                            Thread.Sleep(0x19);
                        }

                        if (boolKeepNTLMSockets || (0 < Request.ClientSocket.Available))
                        {
                            _nextSession = boolKeepNTLMSockets ? 
                                new Session(Request.ClientSocket, Response.ServerSocket, Response.WasForwarded) : 
                                new Session(Request.ClientSocket, null, false);

                            Request.ClientSocket = null;
                            return true;
                        }
                    }

                    try
                    {
                        if (Request.ClientSocket != null)
                        {
                            Request.ClientSocket.Shutdown(SocketShutdown.Both);
                            Request.ClientSocket.Close();
                        }
                    }
                    catch (Exception)
                    {
                    }

                    Request.ClientSocket = null;
                    flag = true;
                }
                else
                {
                    Request.ClientSocket = null;
                }
            }
            catch (SocketException ex)
            {
                if (ex.SocketErrorCode != SocketError.ConnectionReset)
                {
                    Request.FailSession(400, "Bad Request", string.Format(CultureInfo.InvariantCulture, "Return response failed.\n{0}", ex.Message));
                }
            }
            
            return flag;
        }

        private void UtilDecodeResponse()
        {
            if (((Response == null) || (Response.Headers == null)) || (!Response.Headers.Exists("Transfer-Encoding") && 
                                                                       !Response.Headers.Exists("Content-Encoding")))
            {
                return;
            }

            if (Response.Headers.ExistsAndContains("Transfer-Encoding", "chunked"))
            {
                ResponseBodyBytes = Utilities.DoUnchunk(ResponseBodyBytes);
            }

            if (Response.Headers.ExistsAndContains("Transfer-Encoding", "gzip") ||
                Response.Headers.ExistsAndContains("Content-Encoding", "gzip"))
            {
                ResponseBodyBytes = Utilities.GzipExpand(ResponseBodyBytes);
            }

            if (Response.Headers.ExistsAndContains("Transfer-Encoding", "deflate") ||
                Response.Headers.ExistsAndContains("Content-Encoding", "deflate"))
            {
                ResponseBodyBytes = Utilities.DeflateExpand(ResponseBodyBytes);
            }

            Response.Headers.Remove("Transfer-Encoding");
            Response.Headers.Remove("Content-Encoding");
            Response["Content-Length"] = (ResponseBodyBytes == null) ? "0" : ResponseBodyBytes.LongLength.ToString(CultureInfo.InvariantCulture);
        }

        private void UtilDecodeRequest()
        {
            if (((Request == null) || (Request.Headers == null)) || (!Request.Headers.Exists("Transfer-Encoding") && 
                                                                     !Request.Headers.Exists("Content-Encoding")))
            {
                return;
            }

            try
            {
                if ((RequestBodyBytes != null) && (RequestBodyBytes.LongLength > 0L))
                {
                    if (Request.Headers.ExistsAndContains("Transfer-Encoding", "chunked"))
                    {
                        RequestBodyBytes = Utilities.DoUnchunk(RequestBodyBytes);
                    }

                    if (Request.Headers.ExistsAndContains("Transfer-Encoding", "gzip") || Request.Headers.ExistsAndContains("Content-Encoding", "gzip"))
                    {
                        RequestBodyBytes = Utilities.GzipExpand(RequestBodyBytes);
                    }

                    if (Request.Headers.ExistsAndContains("Transfer-Encoding", "deflate") || Request.Headers.ExistsAndContains("Content-Encoding", "deflate"))
                    {
                        RequestBodyBytes = Utilities.DeflateExpand(RequestBodyBytes);
                    }
                }

                Request.Headers.Remove("Transfer-Encoding");
                Request.Headers.Remove("Content-Encoding");
                Request["Content-Length"] = (RequestBodyBytes == null) ? "0" : RequestBodyBytes.LongLength.ToString(CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
            }
        }

        private string ToString(bool headersOnly)
        {
            string str;
            if (!headersOnly)
            {
                str = Request.Headers.ToString(true, true);
                if (RequestBodyBytes != null)
                {
                    str = str + Encoding.UTF8.GetString(RequestBodyBytes);
                }

                if ((Response != null) && (Response.Headers != null))
                {
                    str = str + "\r\n" + Response.Headers.ToString(true, true);
                    if (ResponseBodyBytes != null)
                    {
                        str = str + Encoding.UTF8.GetString(ResponseBodyBytes);
                    }
                }

                return str;
            }

            str = Request.Headers.ToString();
            if ((Response != null) && (Response.Headers != null))
            {
                str = str + "\r\n" + Response.Headers;
            }

            return str;
        }

        private void ExecuteBasicRequestManipulations()
        {
            if (Host.Equals("neverlands.ru", StringComparison.OrdinalIgnoreCase) ||
                Host.EndsWith(".neverlands.ru", StringComparison.OrdinalIgnoreCase))
            {
                _isGameHost = true;
            }

            var str = Url;
            if (str.EndsWith(".gif", StringComparison.OrdinalIgnoreCase) ||
                str.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
                str.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase) ||
                str.EndsWith(".png", StringComparison.OrdinalIgnoreCase) ||
                str.EndsWith(".swf", StringComparison.OrdinalIgnoreCase) ||
                str.EndsWith(".ico", StringComparison.OrdinalIgnoreCase))
            {
                _isCache = true;
            }
            else
            {
                if (str.EndsWith(".css", StringComparison.OrdinalIgnoreCase))
                {
                    _isCache = true;
                }
                else
                {
                    if (str.IndexOf(".js", StringComparison.OrdinalIgnoreCase) != -1)
                    {
                        _isCache = true;
                    }

                    if (_isGameHost)
                    {
                        _isCustomFilter = true;
                    }
                }
            }

            UtilDecodeRequest();
        }

        private void ExecuteBasicResponseManipulations()
        {
            UtilDecodeResponse();
        }

        private void CloseSessionPipes()
        {
            if (((Request != null) && (Request.ClientSocket != null)) && Request.ClientSocket.Connected)
            {
                try
                {
                    if (Request.ClientSocket != null)
                    {
                        Request.ClientSocket.Shutdown(SocketShutdown.Both);
                        Request.ClientSocket.Close();
                    }
                }
                catch (Exception)
                {
                }

                Request.ClientSocket = null;
            }

            if (((Response != null) && (Response.ServerSocket != null)) && Response.ServerSocket.Connected)
            {
                try
                {
                    if (Response.ServerSocket != null)
                    {
                        Response.ServerSocket.Shutdown(SocketShutdown.Both);
                        Response.ServerSocket.Close();
                    }
                }
                catch (Exception)
                {
                }

                Response.ServerSocket = null;
            }
        }

        private bool DoNTLM(bool boolIsProxy)
        {
            Response.Headers["Proxy-Support"] = "Session-Based-Authentication";
            if (boolIsProxy)
            {
                if (string.IsNullOrEmpty(Request.Headers["Proxy-Authorization"]))
                {
                    return false;
                }

                if (!Response.Headers.Exists("Proxy-Authenticate") || (Response.Headers["Proxy-Authenticate"].Length < 6))
                {
                    return false;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(Request.Headers["Authorization"]))
                {
                    return false;
                }

                if (!Response.Headers.Exists("WWW-Authenticate") || (Response.Headers["WWW-Authenticate"].Length < 6))
                {
                    return false;
                }
            }

            return true;
        }
    }
}