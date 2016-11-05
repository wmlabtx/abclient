using System.Threading;
using System.Web;
using System;
using System.Collections.Generic;

namespace ABClient.ABProxy
{
    internal static class CookiesManager
    {
        private static readonly SortedDictionary<string, CookiePack> CookiePackCollection = new SortedDictionary<string, CookiePack>();
        private static readonly ReaderWriterLock Rwl = new ReaderWriterLock();

        internal static void Assign(string host, string data)
        {
            if (host.Equals("www.neverlands.ru", StringComparison.OrdinalIgnoreCase))
            {
                const string nevernick = "NeverNick=";
                if (data.StartsWith(nevernick, StringComparison.OrdinalIgnoreCase))
                {
                    var encnick = data.Substring(nevernick.Length);
                    var nick = HttpUtility.UrlDecode(encnick, Helpers.Russian.Codepage);
                    if (!nick.Equals(AppVars.Profile.UserNick, StringComparison.OrdinalIgnoreCase))
                    {
                        throw new Exception("Неверное имя или пароль.");
                    }

                    AppVars.Profile.UserNick = nick;
                }
            }

            var poseq = data.IndexOf('=');
            if (poseq == -1)
            {
                return;
            }

            var sheader = data.Substring(0, poseq);
            if (string.IsNullOrEmpty(sheader))
            {
                return;
            }

            var posemi = data.IndexOf(';', poseq);
            var svalue = (posemi == -1) ? data.Substring(poseq + 1) : data.Substring(poseq + 1, posemi - poseq - 1);
            if (string.IsNullOrEmpty(svalue))
            {
                return;
            }

            try
            {
                Rwl.AcquireWriterLock(5000);
                try
                {
                    CookiePack cookiePack;
                    if (CookiePackCollection.TryGetValue(host, out cookiePack))
                    {
                        cookiePack.Add(sheader, svalue);
                        CookiePackCollection[host] = cookiePack;
                    }
                    else
                    {
                        cookiePack = new CookiePack();
                        cookiePack.Add(sheader, svalue);
                        CookiePackCollection.Add(host, cookiePack);
                    }
                }
                finally
                {
                    Rwl.ReleaseWriterLock();
                }
            }
            catch (ApplicationException)
            {
            }
        }

        internal static string Obtain(string host)
        {
            CookiePack cookiePack;
            if (host.Equals("forum.neverlands.ru", StringComparison.OrdinalIgnoreCase))
            {
                host = "www.neverlands.ru"; 
            }

            return CookiePackCollection.TryGetValue(host, out cookiePack) ? cookiePack.ToString() : null;
        }

        internal static void ClearGame()
        {
            try
            {
                Rwl.AcquireWriterLock(5000);
                try
                {
                    CookiePackCollection.Clear();

                }
                finally
                {
                    Rwl.ReleaseWriterLock();
                }
            }
            catch (ApplicationException)
            {
            }

            /*
            var url = HelperConverters.AddressEncode(string.Concat("http://www.neverlands.ru/pinfo.cgi?", "Мастер Создатель"));
            using (var wc = new CookieAwareWebClient { Proxy = AppVars.LocalProxy })
            {
                try
                {
                    IdleManager.AddActivity();
                    var buffer = wc.DownloadData(url);
                    if (buffer != null)
                    {
                        var html = AppVars.Codepage.GetString(buffer);
                        if (html.IndexOf("Cookie...", StringComparison.CurrentCultureIgnoreCase) != -1)
                        {
                            buffer = wc.DownloadData(url);
                            if (buffer != null)
                            {
                                var cc = wc.Cookies.GetCookies(new Uri("http://www.neverlands.ru/"));
                                try
                                {
                                    Rwl.AcquireWriterLock(5000);
                                    try
                                    {
                                        CookiePack cookiePack;
                                        foreach (Cookie c in cc)
                                        {
                                            if (CookiePackCollection.TryGetValue(c.Domain, out cookiePack))
                                            {
                                                cookiePack.Add(c.Name, c.Value);
                                                CookiePackCollection[c.Domain] = cookiePack;
                                            }
                                            else
                                            {
                                                cookiePack = new CookiePack();
                                                cookiePack.Add(c.Name, c.Value);
                                                CookiePackCollection.Add(c.Domain, cookiePack);
                                            }
                                        }
                                    }
                                    finally
                                    {
                                        Rwl.ReleaseWriterLock();
                                    }
                                }
                                catch (ApplicationException)
                                {
                                }

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
            */
        }
    }
}