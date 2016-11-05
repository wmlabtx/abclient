namespace ABClient.PostFilter
{
    using System;
    using Helpers;
    using MyHelpers;

    internal static partial class Filter
    {
        private static byte[] ButPhp(byte[] array)
        {
            var html = Russian.Codepage.GetString(array);
            var now = DateTime.Now;
            var shour = HelperStrings.SubString(html, "hour=", "&");
            var smin = HelperStrings.SubString(html, "min=", "&");
            var ssec = HelperStrings.SubString(html, "sec=", "\"");
            if (!string.IsNullOrEmpty(shour) && !string.IsNullOrEmpty(smin) && !string.IsNullOrEmpty(ssec))
            {
                int hour;
                if (int.TryParse(shour, out hour))
                {
                    int min;
                    if (int.TryParse(smin, out min))
                    {
                        int sec;
                        if (int.TryParse(ssec, out sec))
                        {
                            var clock = new DateTime(now.Year, now.Month, now.Day, hour, min, sec);
                            AppVars.Profile.ServDiff = now.Subtract(clock);
                            if (AppVars.Profile.ServDiff > new TimeSpan(1,0,0,0))
                                AppVars.Profile.ServDiff = new TimeSpan(0);
                        }
                    }
                }
            }

            html = html.Replace(@"/b1.gif", @"/b1.gif name=butinp");
            html = html.Replace("smile_open('')", "window.external.ShowSmiles(1)");
            html = html.Replace("smile_open('2')", "window.external.ShowSmiles(2)");

            //string obj = ;
            //var regex = new Regex("<object\\b[^>]*>[^<]*(?:(?!</?object\\b)<[^<]*)*</object\\s*>");
            //html = regex.Replace(html, string.Empty);

            //const string s1 = "<OBJECT";
            //const string s2 = "</OBJECT>";

            //html = HelperStrings.Replace(html, s1, s2, string.Empty);
            //html = html.Replace(s1, string.Empty);
            //html = html.Replace(s2, string.Empty);

            return Russian.Codepage.GetBytes(html);
        }
    }
}