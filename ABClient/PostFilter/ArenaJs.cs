using ABClient.Properties;

namespace ABClient.PostFilter
{
    using System.Text;

    internal static partial class Filter
    {
        private static byte[] ArenaJs()
        {
            var sb = new StringBuilder(Resources.arena_v04);
            if (AppVars.Profile.ChatKeepMoving)
            {
                sb.Replace("top.clr_chat();", string.Empty);
            }

            return Helpers.Russian.Codepage.GetBytes(sb.ToString());
        }
    }
}