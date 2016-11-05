namespace ABClient.PostFilter
{
    using System.Text;
    using Helpers;

    internal static partial class Filter
    {
        private static byte[] MsgPhp(byte[] array)
        {
            var sb = new StringBuilder(Russian.Codepage.GetString(array));
            if (AppVars.Profile.ChatKeepGame && !string.IsNullOrEmpty(AppVars.Chat))
            {
                sb.Replace(" id=msg>", " id=msg>" + AppVars.Chat);
            }

            return Russian.Codepage.GetBytes(sb.ToString());
        }
    }
}