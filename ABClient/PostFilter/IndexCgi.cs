using System;
using System.Text;
using System.Web;
using ABClient.ABForms;
using ABClient.Helpers;
using ABClient.MyHelpers;

namespace ABClient.PostFilter
{
    internal static partial class Filter
    {
        private static byte[] IndexCgi(byte[] array)
        {
            var html = Russian.Codepage.GetString(array);
            if (html.IndexOf(@"<form method=""post"" id=""auth_form"" action=""./game.php"">", StringComparison.OrdinalIgnoreCase) == -1)
            {
                /*
                if (html.IndexOf("Cookie...", StringComparison.CurrentCultureIgnoreCase) != -1)
                {
                    html = "<HTML><HEAD><title>Please Wait...</title></HEAD><script type=\"text/javascript\">window.location.reload(true);</script><BODY>Cookie...</BODY></HTML>";
                    return Russian.Codepage.GetBytes(html);
                }
                */

                return array;
            }

            const string serror = @"show_warn(""";
            var pos = html.IndexOf(serror, StringComparison.OrdinalIgnoreCase);
            if (pos != -1)
            {
                pos += serror.Length;
                var pose = html.IndexOf('"', pos);
                if (pose != -1)
                {
                    var error = html.Substring(pos, pose - pos);
                    if (!string.IsNullOrEmpty(error))
                    {
                        try
                        {
                            if (AppVars.MainForm != null)
                                AppVars.MainForm.BeginInvoke(
                                    new UpdateAccountErrorDelegate(AppVars.MainForm.UpdateAccountError), error);
                        }
                        catch (InvalidOperationException)
                        {
                        }

                        return Russian.Codepage.GetBytes(string.Empty);
                    }
                }
            }

            var jump = "game.php";
            /*
            if (AppVars.Profile.UserNick.Equals("Alonso", StringComparison.OrdinalIgnoreCase) ||
                AppVars.Profile.UserNick.Equals("FENDER", StringComparison.OrdinalIgnoreCase) ||
                AppVars.Profile.UserNick.Equals("(ПСИХ)", StringComparison.OrdinalIgnoreCase))
            {
                jump = string.Empty;    
            }
            */

            var sb = new StringBuilder(
                HelperErrors.Head() +
                "Ввод имени и пароля..." +
                @"<form action=""./" + jump + @""" method=POST name=ff>" +
                @"<input name=player_nick type=hidden value=""");
            sb.Append(HttpUtility.HtmlEncode(AppVars.Profile.UserNick));
            sb.Append(@"""> <input name=player_password type=hidden value=""");
            sb.Append(HttpUtility.HtmlEncode(AppVars.Profile.UserPassword));
            sb.Append(
                @"""></form>" +
                @"<script language=""JavaScript"">" +
                @"document.ff.submit();" + 
                @"</script></body></html>");
            AppVars.WaitFlash = true;
            AppVars.ContentMainPhp = sb.ToString();
            return Russian.Codepage.GetBytes(AppVars.ContentMainPhp);
        }
    }
}