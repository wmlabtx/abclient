using ABClient.Properties;

namespace ABClient.PostFilter
{
    internal static partial class Filter
    {
        private static byte[] MapJs(byte[] array)
        {
            //var html = Russian.Codepage.GetString(array);
            /*
            //html = html.Replace("var width = 3;", "var width = window.external.GetHalfMapWidth();");
            //html = html.Replace("var height = 1;", "var height = window.external.GetHalfMapHeight();");

            html = html.Replace("var ua = navigator.userAgent.toLowerCase();",
                "var ua = navigator.userAgent.toLowerCase(); var scale = window.external.GetMapScale(); var abcmapwidth = (((width * 2) + 1) * scale) + (width * 2) + 2; var abcmapheight = (((height * 2) + 1) * scale) + (height * 2) + 2;");

            return Russian.Codepage.GetBytes(html);
             */ 

            return AppVars.Codepage.GetBytes(Resources.map);
        }
    }
}