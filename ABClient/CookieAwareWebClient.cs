using System;
using System.Net;
using ABClient.ABProxy;
using NLog;

namespace ABClient
{
    public class CookieAwareWebClient : WebClient
    {
        private readonly CookieContainer _cookieContainer = new CookieContainer();
        private static Logger logger = LogManager.GetCurrentClassLogger();

        protected override WebRequest GetWebRequest(Uri address)
        {
            var basewr = base.GetWebRequest(address);
            var request = basewr as HttpWebRequest;
            if (request != null)
            {
                var wr = request;
                wr.CookieContainer = _cookieContainer;
            }
            if (basewr.Headers.Get("Cookie") == null)
                basewr.Headers.Add("Cookie", CookiesManager.Obtain("www.neverlands.ru"));
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
            catch (WebException ex)
            {
                logger.Error("Error on request-response: " + ex.Message + " - " + ex.StackTrace);
            }

            return basewr;
        }
    }
}