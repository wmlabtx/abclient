using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace ABClient
{
    internal static class Favorites
    {
        internal static readonly List<Bookmark> Bookmarks = new List<Bookmark>();

        static Favorites()
        {
            var pathFavoritesXml = Path.Combine(Application.StartupPath, "abfavorites.xml");
            if (!File.Exists(pathFavoritesXml))
            {
                return;
            }

            string map2;
            try
            {
                map2 = File.ReadAllText(pathFavoritesXml, Encoding.UTF8);
            }
            catch
            {
                return;
            }

            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(map2);
            var favoritesNodeList = xmlDocument.GetElementsByTagName("favorite");
            foreach (XmlNode favoriteNode in favoritesNodeList)
            {
                var bookmark = new Bookmark();
                if (favoriteNode.Attributes != null && favoriteNode.Attributes["title"] != null)
                {
                    bookmark.Title = favoriteNode.Attributes["title"].Value;
                }
                else
                {
                    bookmark.Title = "Без названия";
                }

                if (favoriteNode.Attributes != null && favoriteNode.Attributes["url"] != null)
                {
                    bookmark.Url = favoriteNode.Attributes["url"].Value;
                    if (!bookmark.Url.StartsWith("http://"))
                    {
                        bookmark.Url = "http://" + bookmark.Url;
                    }
                }
                else
                {
                    bookmark.Url = "http://www.neverlands.ru";
                }

                if (favoriteNode.Attributes != null && favoriteNode.Attributes["icon"] != null)
                {
                    var iconFile = Path.Combine(Application.StartupPath, favoriteNode.Attributes["icon"].Value);
                    if (File.Exists(iconFile))
                    {
                        try
                        {
                            bookmark.SmallIcon = Image.FromFile(iconFile);
                        }
                        catch (OutOfMemoryException)
                        {
                        }
                    }
                }

                Bookmarks.Add(bookmark);
            }
        }
    }
}
