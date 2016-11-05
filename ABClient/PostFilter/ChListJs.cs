namespace ABClient.PostFilter
{
    using Helpers;
    using Properties;

    internal static partial class Filter
    {
        private static byte[] ChListJs()
        {
            return Russian.Codepage.GetBytes(Resources.ch_list.Replace("alt=", "title="));
        }
    }
}