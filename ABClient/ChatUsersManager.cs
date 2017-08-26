using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using System.Xml;
using ABClient.MyHelpers;

namespace ABClient
{
    public static class ChatUsersManager
    {
        private static readonly SortedDictionary<string, ChatUser> UserList = new SortedDictionary<string, ChatUser>();
        private static readonly ReaderWriterLock UserListLock = new ReaderWriterLock();

        public static bool Exists(string userNick)
        {
            var result = UserList.ContainsKey(userNick.ToLower());
            if (result)
                return true;

            var userInfo = NeverApi.GetAll(userNick);
            if (userInfo == null)
                return false;

            var nick = userInfo.Nick;
            var sign = userInfo.ClanSign;
            var level = userInfo.Level;
            var status = $"{userInfo.ClanName}, {userInfo.ClanStatus}";
            var user = new ChatUser(nick, level, sign, status);
            UserList.Add(nick.ToLower(), user);
            return true;
        }

        public static ChatUser GetUserData(string userNick)
        {
            ChatUser chatUser;
            return UserList.TryGetValue(userNick.ToLower(), out chatUser) ? chatUser : null;
        }

        public static void AddUser(ChatUser chatUser)
        {
            try
            {
                UserListLock.AcquireWriterLock(5000);
                try
                {
                    ChatUser existingChatUser;
                    if (UserList.TryGetValue(chatUser.Nick.ToLower(), out existingChatUser))
                        UserList[chatUser.Nick.ToLower()] = chatUser;
                    else
                        UserList.Add(chatUser.Nick.ToLower(), chatUser);
                }
                finally
                {
                    UserListLock.ReleaseWriterLock();
                }
            }
            catch (ApplicationException)
            {
            }
        }

        public static void Save()
        {
            try
            {
                UserListLock.AcquireWriterLock(5000);
                try
                {
                    var wSettings = new XmlWriterSettings {Indent = true};
                    var ms = new MemoryStream();
                    var xmlWriter = XmlWriter.Create(ms, wSettings);
                    xmlWriter.WriteStartDocument();
                    xmlWriter.WriteStartElement("chatusers");

                    foreach (var user in UserList)
                    {
                        xmlWriter.WriteStartElement("user");

                        xmlWriter.WriteStartAttribute("nick");
                        xmlWriter.WriteString(user.Value.Nick ?? string.Empty);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("sign");
                        xmlWriter.WriteString(user.Value.Sign ?? string.Empty);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("status");
                        xmlWriter.WriteString(user.Value.Status ?? string.Empty);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("level");
                        xmlWriter.WriteString(user.Value.Level ?? string.Empty);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("lastupdated");
                        xmlWriter.WriteString(user.Value.LastUpdated.ToString(CultureInfo.InvariantCulture));
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteEndElement();
                    }

                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteEndDocument();
                    xmlWriter.Flush();

                    try
                    {
                        var fileStream = new FileStream("chatusers_new.xml", FileMode.Create);
                        ms.WriteTo(fileStream);
                        fileStream.Close();
                        ms.Close();

                        if (File.Exists("chatusers.xml"))
                            File.Delete("chatusers.xml");

                        File.Move("chatusers_new.xml", "chatusers.xml");
                    }
                    catch (IOException)
                    {
                    }
                }
                finally
                {
                    UserListLock.ReleaseWriterLock();
                }
            }
            catch (ApplicationException)
            {
            }
        }

        internal static void Load()
        {
            if (!File.Exists("chatusers.xml"))
                return;

            try
            {
                UserListLock.AcquireWriterLock(5000);
                try
                {
                    string chatusers;
                    try
                    {
                        chatusers = File.ReadAllText("chatusers.xml", Encoding.UTF8);
                    }
                    catch
                    {
                        return;
                    }

                    AppVars.BossContacts = new SortedList<string, BossContact>();
                    var xmlDocument = new XmlDocument();
                    xmlDocument.LoadXml(chatusers);
                    var chatusersNodeList = xmlDocument.GetElementsByTagName("user");
                    foreach (XmlNode chatUser in chatusersNodeList)
                    {
                        if (chatUser.Attributes == null)
                            continue;

                        var lastUpdated = Convert.ToDateTime(chatUser.Attributes["lastupdated"].Value, CultureInfo.InvariantCulture);
                        if (DateTime.Now.Subtract(lastUpdated).TotalDays > 1.0)
                            continue;

                        var nick = chatUser.Attributes["nick"].Value;
                        var level = chatUser.Attributes["level"].Value;
                        var sign = chatUser.Attributes["sign"].Value;
                        var status = chatUser.Attributes["status"].Value;
                        var user = new ChatUser(nick, level, sign, status);
                        UserList.Add(nick.ToLower(), user);
                    }
                }
                finally
                {
                    UserListLock.ReleaseWriterLock();
                }
            }
            catch (ApplicationException)
            {
            }
        }
    }
}
