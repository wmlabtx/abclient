using System.IO;

namespace ABClient.PostFilter
{
    using Helpers;

    internal static partial class Filter
    {
        private static byte[] ChRoomPhp(byte[] array)
        {
            /*
            string html;            
            try
            {
                html = File.ReadAllText("abclient_test_ch.php.txt");
            }
            catch (FileNotFoundException)
            {
                html = Russian.Codepage.GetString(array);
            }
            */

            var html = Russian.Codepage.GetString(array);
            html = RoomManager.Process(html);
            return Russian.Codepage.GetBytes(html);
        }
    }
}