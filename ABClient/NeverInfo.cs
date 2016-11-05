using System;
using ABClient.MyHelpers;
using System.Net;

namespace ABClient
{
    internal static class NeverInfo
    {
        internal static string GetPInfo(string nick)
        {
            var url = HelperConverters.AddressEncode(string.Concat("http://neverlands.ru/pinfo.cgi?", nick));
            return GetInfo(url);
        }

        internal static string GetFlog(string flog)
        {
            var url = HelperConverters.AddressEncode(string.Concat("http://neverlands.ru/logs.fcg?fid=", flog));
            return GetInfo(url);
        }

        /*
        internal static string GetPBots(string id)
        {
            var url = HelperConverters.AddressEncode(string.Concat("http://www.neverlands.ru/pbots.cgi?", id));
            return GetInfo(url);
        }
        */

        private static string GetInfo(string url)
        {
            string html = null;
            using (var wc = new CookieAwareWebClient {Proxy = AppVars.LocalProxy})
            {
                try
                {
                    IdleManager.AddActivity();
                    var buffer = wc.DownloadData(url);
                    if (buffer != null)
                    {
                        html = AppVars.Codepage.GetString(buffer);
                        if (html.IndexOf("Cookie...", StringComparison.CurrentCultureIgnoreCase) != -1)
                        {
                            buffer = wc.DownloadData(url);
                            if (buffer != null)
                            {
                                html = AppVars.Codepage.GetString(buffer);
                            }
                        }
                    }
                }
                    // ReSharper disable once EmptyGeneralCatchClause
                catch (Exception)
                {
                }
                finally
                {
                    IdleManager.RemoveActivity();
                }
            }

            return html;
        }
    }

    internal class CookieAwareWebClient : WebClient
    {
        private readonly CookieContainer _cookieContainer = new CookieContainer();

        //internal CookieContainer Cookies => _cookieContainer;

        protected override WebRequest GetWebRequest(Uri address)
        {
            var basewr = base.GetWebRequest(address);
            var request = basewr as HttpWebRequest;
            if (request != null)
            {
                var wr = request;
                wr.CookieContainer = _cookieContainer;
            }

            return basewr;
        }

        protected override WebResponse GetWebResponse(WebRequest request)
        {
            WebResponse basewr = null;
            try
            {
                basewr = base.GetWebResponse(request);
                var responce = basewr as HttpWebResponse;
                if (responce != null && responce.Cookies != null)
                {
                    _cookieContainer.Add(responce.Cookies);
                }
            }
            catch (WebException)
            {
            }

            return basewr;
        }
    }
}