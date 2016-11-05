namespace ABClient.PostFilter
{
    using Helpers;

    internal static partial class Filter
    {
        private static byte[] HpJs(byte[] array)
        {
            var html = Russian.Codepage.GetString(array);
            html = html.Replace(
                "s.substring(0, s.lastIndexOf(':')+1) + \"[<font color=#bb0000><b>\" + Math.round(curHP)+\"</b>/<b>\"+maxHP+\"</b></font> | <font color=#336699><b>\"+Math.round(curMA)+\"</b>/<b>\"+maxMA+\"</b></font>]\"",
                "window.external.ShowHpMaTimers(s,curHP,maxHP,intHP,curMA,maxMA,intMA)");

            return Russian.Codepage.GetBytes(html);
        }
    }
}