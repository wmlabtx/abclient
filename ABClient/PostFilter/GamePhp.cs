namespace ABClient.PostFilter
{
    using Helpers;
    using MyHelpers;
    using System;
    using System.Text;

    internal static partial class Filter
    {
        private static byte[] GamePhp(byte[] array)
        {
            var html = Russian.Codepage.GetString(array);
            html = RemoveDoctype(html);
            if (AppVars.WaitFlash)
            {
                if (!string.IsNullOrEmpty(AppVars.Profile.UserPasswordFlash))
                {
                    // flashvars="plid=827098"
                    const string flashid = @"flashvars=""plid=";
                    var pos = html.IndexOf(flashid, StringComparison.OrdinalIgnoreCase);
                    if (pos > -1)
                    {
                        pos += flashid.Length;
                        var pose = html.IndexOf('"', pos);
                        if (pose > -1)
                        {
                            var pid = html.Substring(pos, pose - pos);
                            var sb = new StringBuilder(
                                HelperErrors.Head() +
                                "Ввод флеш-пароля..." +
                                @"<form action=""./game.php"" method=POST name=ff>" +
                                @"<input name=flcheck type=hidden value=""");
                            sb.Append(AppVars.Profile.UserPasswordFlash);
                            sb.Append(@"""> <input name=nid type=hidden value=""");
                            sb.Append(pid);
                            sb.Append(
                                @"""></form>" +
                                @"<script language=""JavaScript"">" +
                                @"document.ff.submit();" +
                                @"</script></body></html>");
                            AppVars.ContentMainPhp = sb.ToString();
                            return Russian.Codepage.GetBytes(AppVars.ContentMainPhp);
                        }

                        AppVars.ContentMainPhp = html;
                        return Russian.Codepage.GetBytes(AppVars.ContentMainPhp);
                    }
                }
            }
                
            AppVars.WaitFlash = false;
            AppVars.ContentMainPhp = html;
            return Russian.Codepage.GetBytes(AppVars.ContentMainPhp);
        }
    }
}