using System;
using System.Net;

namespace ABClient
{
    public class CookieAwareWebClient : WebClient
    {
        private readonly CookieContainer _cookieContainer = new CookieContainer();

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