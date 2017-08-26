using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Forms;
using ABClient.AppControls;
using System.Threading;
using System.Xml;
using ABClient.ABForms;

namespace ABClient
{
    public static class ContactsManager
    {
        public static readonly ReaderWriterLock Rwl = new ReaderWriterLock();

        public static void Init(TreeViewEx tree)
        {
            if (AppVars.Profile.Contacts.Count == 0)
            {
                AppVars.Profile.Contacts.Add("Мастер Создатель".ToLower(), new Contact("Мастер Создатель", 0, 0, "Платите деньги за солнечный свет...", true, false));
                AppVars.Profile.Contacts.Add("Шандор-Волшебник".ToLower(), new Contact("Шандор-Волшебник", 0, 0, "Массовик-затейник", true, false));
                AppVars.Profile.Contacts.Add("Черный".ToLower(), new Contact("Черный", 2, 0, "Автор клиента ABClient (2007-2016)\r\n\nE-mail: wmlab@hotmail.com\r\n\nSkype: wmlab.home", true, false));
            }

            try
            {
                Rwl.AcquireWriterLock(5000);
                try
                {
                    tree.BeginUpdate();
                    tree.Nodes.Clear();

                    foreach (var contact in AppVars.Profile.Contacts)
                        Add(tree, contact.Value);

                    tree.EndUpdate();
                }
                finally
                {
                    Rwl.ReleaseWriterLock();
                }
            }
            catch (ApplicationException)
            {
            }
        }

        public static void LoadBossUsers()
        {
            try
            {
                Rwl.AcquireWriterLock(5000);
                try
                {
                    if (File.Exists("bossusers.xml"))
                    {
                        string bossusers;
                        try
                        {
                            bossusers = File.ReadAllText("bossusers.xml", Encoding.UTF8);
                        }
                        catch
                        {
                            return;
                        }

                        AppVars.BossContacts = new SortedList<string, BossContact>();
                        var rawList = new List<BossContact>();
                        var xmlDocument = new XmlDocument();
                        xmlDocument.LoadXml(bossusers);
                        var bossusersNodeList = xmlDocument.GetElementsByTagName("contactentry");
                        foreach (XmlNode bossUser in bossusersNodeList)
                        {
                            if (bossUser.Attributes == null)
                                continue;

                            var name = bossUser.Attributes["name"].Value;
                            var lastbossupdated = Convert.ToDateTime(bossUser.Attributes["lastbossupdated"].Value, CultureInfo.InvariantCulture);
                            var contact = new BossContact(name, true, lastbossupdated);
                            rawList.Add(contact);
                        }

                        rawList.Sort(SortByLastBossUpdated);
                        var count = Math.Min(rawList.Count, 100);
                        for (var i = 0; i < count; i++)
                        {
                            AppVars.BossContacts.Add(rawList[i].Name.ToLower(), rawList[i]);
                        }
                    }
                }
                finally
                {
                    Rwl.ReleaseWriterLock();
                }
            }
            catch (ApplicationException)
            {
            }
        }

        private static int SortByLastBossUpdated(BossContact x, BossContact y)
        {
            return y.LastBossUpdated.CompareTo(x.LastBossUpdated);
        }

        public static void SaveBossUsers()
        {
            try
            {
                Rwl.AcquireWriterLock(5000);
                try
                {
                    var wSettings = new XmlWriterSettings { Indent = true };
                    var ms = new MemoryStream();
                    var xmlWriter = XmlWriter.Create(ms, wSettings);
                    xmlWriter.WriteStartDocument();
                    xmlWriter.WriteStartElement("bossusers");

                    foreach (var contact in AppVars.BossContacts)
                    {
                        xmlWriter.WriteStartElement("contactentry");

                        xmlWriter.WriteStartAttribute("name");
                        xmlWriter.WriteString(contact.Value.Name ?? string.Empty);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteStartAttribute("lastbossupdated");
                        xmlWriter.WriteValue(contact.Value.LastBossUpdated);
                        xmlWriter.WriteEndAttribute();

                        xmlWriter.WriteEndElement();
                    }

                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteEndDocument();
                    xmlWriter.Flush();

                    try
                    {
                        var fileStream = new FileStream("bossusers_new.xml", FileMode.Create);
                        ms.WriteTo(fileStream);
                        fileStream.Close();
                        ms.Close();

                        if (File.Exists("bossusers.xml"))
                            File.Delete("bossusers.xml");

                        File.Move("bossusers_new.xml", "bossusers.xml");
                    }
                    catch (IOException)
                    {
                    }
                }
                finally
                {
                    Rwl.ReleaseWriterLock();
                }
            }
            catch (ApplicationException)
            {
            }
        }

        public static void AddUsers(string args)
        {
            var added = false;
            try
            {
                Rwl.AcquireWriterLock(5000);
                try
                {
                    var users = args.Split(new[] { "],[" }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var user in users)
                    {
                        var spar = user.Split(',');
                        if (spar.Length < 5)
                            continue;

                        if (spar[0].Equals("1"))
                        {
                            var nick = spar[1].Trim('\"');

                            if (AppVars.BossContacts.ContainsKey(nick.ToLower()))
                            {
                                AppVars.BossContacts[nick.ToLower()].LastBossUpdated =
                                    DateTime.Now.Subtract(AppVars.Profile.ServDiff);
                            }
                            else
                            {
                                var contact = new BossContact(nick, true, DateTime.Now.Subtract(AppVars.Profile.ServDiff));
                                AppVars.BossContacts.Add(contact.Name.ToLower(), contact);
                                var message = $"Контакт [{nick}] добавлен в слежение";
                                try
                                {
                                    if (AppVars.MainForm != null)
                                    {
                                        AppVars.MainForm.BeginInvoke(
                                            new UpdateChatDelegate(AppVars.MainForm.UpdateChat), message);
                                    }
                                }
                                catch (InvalidOperationException)
                                {
                                }

                                added = true;
                            }
                        }
                    }
                }
                finally
                {
                    Rwl.ReleaseWriterLock();
                }
            }
            catch (ApplicationException)
            {
            }

            if (added)
                SaveBossUsers();
        }


        public static void Pulse()
        {
            var nextContactKey = string.Empty;
            var isBossContact = false;
            try
            {
                Rwl.AcquireWriterLock(5000);
                try
                {
                    var nextCheck = DateTime.MaxValue;
                    if (AppVars.Profile.DoContactTrace)
                    {
                        foreach (var contact in AppVars.Profile.Contacts)
                        {
                            if (!contact.Value.Tracing)
                                continue;

                            if (!string.IsNullOrEmpty(nextContactKey) && (contact.Value.NextCheck >= nextCheck))
                                continue;

                            nextContactKey = contact.Key;
                            nextCheck = contact.Value.NextCheck;
                        }
                    }

                    if (AppVars.Profile.DoBossTrace && AppVars.BossContacts != null)
                    {
                        foreach (var contact in AppVars.BossContacts)
                        {
                            if (!string.IsNullOrEmpty(nextContactKey) && contact.Value.NextCheck >= nextCheck)
                                continue;

                            nextContactKey = contact.Key;
                            isBossContact = true;
                            nextCheck = contact.Value.NextCheck;
                        }
                    }
                }
                finally
                {
                    Rwl.ReleaseWriterLock();
                }
            }
            catch (ApplicationException)
            {
            }

            if (string.IsNullOrEmpty(nextContactKey))
                return;

            if (!isBossContact)
            {
                if (AppVars.Profile.Contacts.ContainsKey(nextContactKey))
                {
                    AppVars.Profile.Contacts[nextContactKey].NextCheck =
                        AppVars.Profile.Contacts[nextContactKey].NextCheck.AddMinutes(1);

                    ThreadPool.QueueUserWorkItem(ProcessAsync, nextContactKey);
                }
            }
            else
            {
                if (AppVars.BossContacts.ContainsKey(nextContactKey))
                {
                    AppVars.BossContacts[nextContactKey].NextCheck =
                        AppVars.BossContacts[nextContactKey].NextCheck.AddMinutes(1);

                    ThreadPool.QueueUserWorkItem(ProcessBossAsync, nextContactKey);
                }
            }
        }

        private static void ProcessAsync(object state)
        {
            var nextContactKey = (string)state;
            Contact contact;
            if (!AppVars.Profile.Contacts.TryGetValue(nextContactKey, out contact))
                return;

            var nick = contact.Name;
            var userInfo = NeverApi.GetAll(nick);
            if (!AppVars.Profile.Contacts.TryGetValue(nextContactKey, out contact))
                return;

            contact.Process(userInfo);
        }

        private static void ProcessBossAsync(object state)
        {
            var nextContactKey = (string)state;
            BossContact contact;
            if (!AppVars.BossContacts.TryGetValue(nextContactKey, out contact))
                return;

            var nick = contact.Name;
            var userInfo = NeverApi.GetAll(nick);
            if (!AppVars.BossContacts.TryGetValue(nextContactKey, out contact))
                return;

            contact.Process(userInfo);
        }

        private static string GetParentName(Contact contact)
        {
            var admins = new[] { "Мастер Создатель", "Шандор-Волшебник", "Иксуй", "Хатор-Законник", "Хранитель" };

            if (Array.IndexOf(admins, contact.Name) >= 0 || 
                (!string.IsNullOrEmpty(contact.Sign) && (contact.Sign.StartsWith("c279", StringComparison.OrdinalIgnoreCase) || contact.Sign.StartsWith("c280", StringComparison.OrdinalIgnoreCase))))
            {
                return "Администраторы";
            }

            if (!string.IsNullOrEmpty(contact.Sign) && contact.Sign.StartsWith("pv", StringComparison.OrdinalIgnoreCase))
            {
                return "Представители власти";
            }

            return string.IsNullOrEmpty(contact.Clan) ? string.Empty : contact.Clan;
        }

        private static void Add(TreeView tree, Contact contact)
        {
            try
            {
                Rwl.AcquireWriterLock(5000);
                try
                {
                    var tn = MakeTreeNode(contact);
                    var nameGroup = GetParentName(contact);
                    if (string.IsNullOrEmpty(nameGroup))
                    {
                        tree.Nodes.Add(tn);
                    }
                    else
                    {
                        if (!tree.Nodes.ContainsKey(nameGroup))
                        {
                            var tnparent = MakeGroupNode(nameGroup, contact);
                            tree.Nodes.Insert(0, tnparent);
                            tnparent.Nodes.Add(tn);
                            AppVars.Profile.Contacts[contact.Name.ToLower()].Parent = nameGroup;
                            UpdateGroupCounter(tnparent);
                        }
                        else
                        {
                            var tnparent = tree.Nodes[nameGroup];
                            tnparent.Nodes.Add(tn);
                            if (tn.Checked && !tnparent.Checked)
                            {
                                tnparent.Checked = true;
                            }

                            AppVars.Profile.Contacts[contact.Name.ToLower()].Parent = nameGroup;
                            UpdateGroupCounter(tnparent);
                        }
                    }
                }
                finally
                {
                    Rwl.ReleaseWriterLock();
                }
            }
            catch (ApplicationException)
            {
            }
        }

        internal static void Add(TreeViewEx tree, string nick)
        {
            try
            {
                Rwl.AcquireWriterLock(5000);
                try
                {
                    var contact = new Contact(nick, 0, 0, string.Empty, true, false);
                    if (AppVars.Profile.Contacts.ContainsKey(nick.ToLower()))
                        return;

                    AppVars.Profile.Contacts.Add(nick.ToLower(), contact);
                    Add(tree, contact);
                }
                finally
                {
                    Rwl.ReleaseWriterLock();
                }
            }
            catch (ApplicationException)
            {
            }
        }

        private static TreeNode MakeTreeNode(Contact ce)
        {
            var tn = new TreeNode
            {
                Name = ce.TreeNode,
                Text = ce.ToString(),
                ContextMenuStrip = AppVars.MainForm.CmPerson,
                Checked = ce.Tracing,
                ForeColor = Color.LightBlue,
                Tag = ce
            };

            tn.ImageKey = tn.SelectedImageKey = PrepareContactSign(ce);
            return tn;
        }

        private static TreeNode MakeGroupNode(string name, Contact ce)
        {
            var tn = new TreeNode
            {
                Name = name,
                Text = name,
                ContextMenuStrip = AppVars.MainForm.CmGroup,
                Checked = ce.Tracing,
                ForeColor = Color.Black,
                Tag = null
            };

            tn.ImageKey = tn.SelectedImageKey = PrepareContactSign(ce);
            return tn;
        }

        internal static int GetClassIdOfContact(string nick)
        {
            if (!AppVars.Profile.Contacts.ContainsKey(nick.ToLower()))
                return -1;

            var classid = AppVars.Profile.Contacts[nick.ToLower()].ClassId;
            return classid;
        }

        internal static int GetToolIdOfContact(string nick)
        {
            if (!AppVars.Profile.Contacts.ContainsKey(nick.ToLower()))
                return -1;

            var toolid = AppVars.Profile.Contacts[nick.ToLower()].ToolId;
            return toolid;
        }

        internal static Color GetColorOfContact(Contact contact)
        {
            switch (contact.ClassId)
            {
                case 0:
                    return contact.IsOnline ? Color.Black : Color.LightGray;
                case 1:
                    return contact.IsOnline ? Color.DarkRed : Color.LightPink;
                case 2:
                    return contact.IsOnline ? Color.DarkGreen : Color.MediumAquamarine;
                default:
                    return Color.Black;
            }
        }

        private static Color GetColorOfGroup(int classid, bool isOnline)
        {
            switch (classid)
            {
                case 0:
                    return isOnline ? Color.Black : Color.LightGray;
                case 1:
                    return isOnline ? Color.DarkRed : Color.LightPink;
                case 2:
                    return isOnline ? Color.DarkGreen : Color.MediumAquamarine;
                default:
                    return Color.Black;
            }
        }

        internal static void Update(TreeViewEx tree, Contact contact)
        {
            try
            {
                Rwl.AcquireWriterLock(5000);
                try
                {
                    if (AppVars.Profile.Contacts.ContainsKey(contact.Name.ToLower()))
                    {
                        if (!AppVars.Profile.Contacts[contact.Name.ToLower()].Tracing)
                        {
                            return;
                        }
                    }

                    var tn = FindNode(tree, contact);
                    if (tn == null)
                    {
                        return;
                    }

                    tn.Text = contact.ToString();
                    tn.ForeColor = GetColorOfContact(contact);
                    tn.ImageKey = tn.SelectedImageKey = PrepareContactSign(contact);
                    tn.ToolTipText = contact.Location;

                    var nameGroup = GetParentName(contact);
                    if (!nameGroup.Equals(contact.Parent, StringComparison.OrdinalIgnoreCase))
                    {
                        if (string.IsNullOrEmpty(contact.Parent))
                        {
                            tree.Nodes.Remove(tn);
                        }
                        else
                        {
                            if (tree.Nodes.ContainsKey(contact.Parent))
                            {
                                var tnoldparent = tree.Nodes[contact.Parent];
                                tnoldparent.Nodes.Remove(tn);
                                UpdateGroupCounter(tnoldparent);
                            }
                        }

                        if (string.IsNullOrEmpty(nameGroup))
                        {
                            tree.Nodes.Add(tn);
                        }
                        else
                        {
                            if (!tree.Nodes.ContainsKey(nameGroup))
                            {
                                var tnparent = MakeGroupNode(nameGroup, contact);
                                tree.Nodes.Insert(0, tnparent);
                                tnparent.Nodes.Add(tn);
                                UpdateGroupCounter(tnparent);
                                if (AppVars.Profile.Contacts.ContainsKey(contact.Name.ToLower()))
                                    AppVars.Profile.Contacts[contact.Name.ToLower()].Parent = nameGroup;
                            }
                            else
                            {
                                var tnparent = tree.Nodes[nameGroup];
                                tnparent.Nodes.Add(tn);
                                if (AppVars.Profile.Contacts.ContainsKey(contact.Name.ToLower()))
                                    AppVars.Profile.Contacts[contact.Name.ToLower()].Parent = nameGroup;
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(nameGroup) && tree.Nodes.ContainsKey(nameGroup))
                    {
                        var tnparent = tree.Nodes[nameGroup];
                        UpdateGroupCounter(tnparent);
                    }
                }
                finally
                {
                    Rwl.ReleaseWriterLock();
                }
            }
            catch (ApplicationException)
            {
            }
        }

        private static void UpdateGroupCounter(TreeNode tngroup)
        {
            var countTracing = 0;
            var countOnline = 0;
            var countClass = new int[3];
            foreach (TreeNode node in tngroup.Nodes)
            {
                var contact = (Contact)node.Tag;
                if (contact == null)
                {
                    continue;
                }

                if ((contact.ClassId >= 0) && (contact.ClassId <= 2))
                {
                    countClass[contact.ClassId]++;
                }

                if (contact.Tracing)
                {
                    countTracing++;
                    if (contact.IsOnline)
                    {
                        countOnline++;
                    }
                }
            }

            tngroup.Text = countTracing == 0 ? tngroup.Name : string.Format("{0} ({1}/{2})", tngroup.Name, countOnline, countTracing);
            if (!tngroup.Checked)
            {
                tngroup.ForeColor = Color.LightBlue;
            }
            else
            {
                if ((countClass[0] > 0) && (countClass[1] == 0) && (countClass[2] == 0))
                    tngroup.ForeColor = GetColorOfGroup(0, countOnline > 0);
                else
                {
                    if ((countClass[0] == 0) && (countClass[1] > 0) && (countClass[2] == 0))
                        tngroup.ForeColor = GetColorOfGroup(1, countOnline > 0);
                    else
                    {
                        if ((countClass[0] == 0) && (countClass[1] == 0) && (countClass[2] > 0))
                            tngroup.ForeColor = GetColorOfGroup(2, countOnline > 0);
                        else
                        {
                            tngroup.ForeColor = countOnline > 0 ? Color.Black : Color.LightGray;
                        }
                    }
                }
            }
        }

        internal static void UpdateComments(Contact contact, string comment)
        {
            if (AppVars.Profile.Contacts.ContainsKey(contact.Name.ToLower()))
            {
                AppVars.Profile.Contacts[contact.Name.ToLower()].Comments = comment;
            }
        }

        internal static void Remove(TreeViewEx tree, TreeNode tn)
        {
            try
            {
                Rwl.AcquireWriterLock(5000);
                try
                {
                    var contact = (Contact)tn.Tag;
                    tn.Remove();
                    if (!string.IsNullOrEmpty(contact.Parent) && tree.Nodes.ContainsKey(contact.Parent))
                    {
                        var tnparent = tree.Nodes[contact.Parent];
                        UpdateGroupCounter(tnparent);
                    }

                    if (AppVars.Profile.Contacts.ContainsKey(contact.Name.ToLower()))
                    {
                        AppVars.Profile.Contacts.Remove(contact.Name.ToLower());
                    }

                    AppVars.Profile.Save();
                }
                finally
                {
                    Rwl.ReleaseWriterLock();
                }
            }
            catch (ApplicationException)
            {
            }
        }

        internal static void RemoveGroup(TreeViewEx tree, string group)
        {
            try
            {
                Rwl.AcquireWriterLock(5000);
                try
                {
                    if (!tree.Nodes.ContainsKey(group))
                        return;

                    var tngroup = tree.Nodes[group];
                    tree.BeginUpdate();
                    for (int i = tngroup.Nodes.Count - 1; i >= 0; i--)
                    {
                        var tn = tngroup.Nodes[i];
                        var contact = (Contact)tn.Tag;
                        tn.Remove();

                        if (AppVars.Profile.Contacts.ContainsKey(contact.Name.ToLower()))
                        {
                            AppVars.Profile.Contacts.Remove(contact.Name.ToLower());
                        }
                    }

                    tngroup.Remove();
                    tree.EndUpdate();

                    AppVars.Profile.Save();
                }
                finally
                {
                    Rwl.ReleaseWriterLock();
                }
            }
            catch (ApplicationException)
            {
            }
        }

        internal static void AfterCheck(TreeView tree, TreeNode tn)
        {
            try
            {
                Rwl.AcquireWriterLock(5000);
                try
                {
                    if (tn.Tag == null)
                    {
                        foreach (TreeNode node in tn.Nodes)
                        {
                            node.Checked = tn.Checked;
                        }
                    }
                    else
                    {
                        var contact = (Contact)tn.Tag;
                        if (AppVars.Profile.Contacts.ContainsKey(contact.Name.ToLower()))
                        {
                            AppVars.Profile.Contacts[contact.Name.ToLower()].Tracing = tn.Checked;
                        }

                        if (!string.IsNullOrEmpty(contact.Parent) && tree.Nodes.ContainsKey(contact.Parent))
                        {
                            var tnparent = tree.Nodes[contact.Parent];
                            UpdateGroupCounter(tnparent);
                        }

                        if (!tn.Checked)
                        {
                            tn.ForeColor = Color.LightBlue;
                        }
                    }
                }
                finally
                {
                    Rwl.ReleaseWriterLock();
                }
            }
            catch (ApplicationException)
            {
            }
        }

        private static string PrepareContactSign(Contact contact)
        {
            if (contact.Wounds[3] > 0)
            {
                return "injury0";
            }

            if (contact.Wounds[2] > 0)
            {
                return "injury1";
            }

            if (contact.Wounds[1] > 0)
            {
                return "injury4";
            }

            if (contact.Wounds[0] > 0)
            {
                return "injury4";
            }

            if (contact.IsMolch)
            {
                return "molch";
            }

            if (string.IsNullOrEmpty(contact.Sign))
            {
                return "neutral";
            }

            if (string.CompareOrdinal(contact.Sign, "none") == 0)
            {
                if (string.IsNullOrEmpty(contact.Align))
                {
                    return "neutral";
                }

                var ali1 = string.Empty;
                switch (contact.Align)
                {
                    case "1":
                        ali1 = "darks.gif";
                        break;
                    case "2":
                        ali1 = "lights.gif";
                        break;
                    case "3":
                        ali1 = "sumers.gif";
                        break;
                    case "4":
                        ali1 = "chaoss.gif";
                        break;
                    case "5":
                        ali1 = "light.gif";
                        break;
                    case "6":
                        ali1 = "dark.gif";
                        break;
                    case "7":
                        ali1 = "sumer.gif";
                        break;
                    case "8":
                        ali1 = "chaos.gif";
                        break;
                    case "9":
                        ali1 = "angel.gif";
                        break;
                }

                if (string.IsNullOrEmpty(ali1))
                {
                    return "none";
                }

                if (AppVars.MainForm.ImageListContacts.Images.ContainsKey(ali1))
                {
                    return ali1;
                }

                var pathali1 = Path.Combine(Application.StartupPath, @"abcache\image.neverlands.ru\signs\" + ali1);
                if (!File.Exists(pathali1))
                {
                    return "neutral";
                }

                try
                {
                    AppVars.MainForm.ImageListContacts.Images.Add(ali1, Image.FromFile(pathali1));
                }
                catch
                {
                    return "neutral";
                }

                return ali1;
            }

            if (AppVars.MainForm.ImageListContacts.Images.ContainsKey(contact.Sign))
            {
                return contact.Sign;
            }

            var path = Path.Combine(Application.StartupPath, @"abcache\image.neverlands.ru\signs\" + contact.Sign);
            if (!File.Exists(path))
            {
                return "neutral";
            }

            try
            {
                AppVars.MainForm.ImageListContacts.Images.Add(contact.Sign, Image.FromFile(path));
            }
            catch
            {
                return "neutral";
            }

            return contact.Sign;
        }

        private static TreeNode FindNode(TreeView tree, Contact contact)
        {
            if (tree.Nodes.ContainsKey(contact.TreeNode))
            {
                return tree.Nodes[contact.TreeNode];
            }

            if (tree.Nodes.ContainsKey(contact.Parent))
            {
                var tnparent = tree.Nodes[contact.Parent];
                if (tnparent.Nodes.ContainsKey(contact.TreeNode))
                {
                    return tnparent.Nodes[contact.TreeNode];
                }
            }

            return null;
        }
    }
}