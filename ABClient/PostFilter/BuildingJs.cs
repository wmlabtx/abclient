namespace ABClient.PostFilter
{
    using System.Text;
    using Helpers;

    internal static partial class Filter
    {
        private static byte[] BuildingJs(byte[] array)
        {
            var sb = new StringBuilder(Russian.Codepage.GetString(array));
            if (AppVars.Profile.ChatKeepMoving)
            {
                sb.Replace("parent.clr_chat();", string.Empty);
            }

            return Russian.Codepage.GetBytes(sb.ToString());
        }
    }
}