using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace ABClient
{
    internal static class BossMap
    {
        private static readonly List<KeyValuePair<string, string>> Terrain = new List<KeyValuePair<string, string>>();

        public static void LoadMap()
        {
            Terrain.Clear();
            var map2 = File.ReadAllText(AppConsts.FileMap, Encoding.UTF8);
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(map2);
            var cellsNodeList = xmlDocument.GetElementsByTagName("cell");
            foreach (XmlNode cellNode in cellsNodeList)
            {
                if (cellNode.Attributes != null)
                {
                    var tooltip = cellNode.Attributes["label"].Value;
                    var regnum = cellNode.Attributes["regnum"].Value;
                    Terrain.Add(new KeyValuePair<string, string>(tooltip, regnum));
                }
            }
        }

        public static string GetRegNum(string location)
        {
            var sb = new StringBuilder();
            foreach (var kp in Terrain)
            {
                if (!location.Equals(kp.Key))
                    continue;

                if (sb.Length > 0)
                    sb.Append(", ");

                sb.Append(kp.Value);
            }

            return sb.ToString();
        }
    }
}
