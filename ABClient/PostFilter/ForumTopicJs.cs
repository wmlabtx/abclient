namespace ABClient.PostFilter
{
    using Helpers;

    internal static partial class Filter
    {
        private static byte[] ForumTopicJs(byte[] array)
        {
            if (!AppVars.Profile.LightForum)
                return array;

            var html = Russian.Codepage.GetString(array);
            html =
                html.Replace(
                    "<br><img src=\"http://image.neverlands.ru/forum/avatars/'+fdata[10]+'.jpg\" width=\"80\" height=\"80\" border=\"0\" vspace=\"3\">",
                    string.Empty);
            html =
                html.Replace(
                    "<br><img src=\"http://image.neverlands.ru/forum/avatars/'+fdata[i][6]+'.jpg\" width=\"80\" height=\"80\" border=\"0\" vspace=\"3\">",
                    string.Empty);

            return Russian.Codepage.GetBytes(html);
        }
    }
}