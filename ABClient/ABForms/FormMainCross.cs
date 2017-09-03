using System.Collections.Generic;
using ABClient.Helpers;

namespace ABClient.ABForms
{
    using System;
    using System.Globalization;
    using System.Text;
    using System.Threading;
    using System.Windows.Forms;
    using MyProfile;
    using MySounds;
    using ExtMap;

    internal sealed partial class FormMain
    {
        internal static void FishOverload()
        {
            if (!AppVars.Profile.FishStopOverWeight)
            {
                return;
            }

            try
            {
                if (AppVars.MainForm != null)
                {
                    AppVars.MainForm.BeginInvoke(
                        new UpdateFishOffDelegate(AppVars.MainForm.UpdateFishOff),
                        new object[] { });
                }
            }
            catch (InvalidOperationException)
            {
            }
        }

        internal static string InsertGuaDiv(string code)
        {
            AppVars.CodeAddress = code;
            if (!string.IsNullOrEmpty(AppVars.CodeAddress))
            {
                if (AppVars.Profile.DoGuamod)
                {
                    AppVars.FightLink = "????";
                }

                if (!AppVars.Profile.DoGuamod)
                {
                    if (AppVars.MainForm != null && AppVars.MainForm.TrayIsDigitsWaitTooLong())
                    {
                        try
                        {
                            if (AppVars.MainForm != null)
                            {
                                AppVars.MainForm.BeginInvoke(
                                    new UpdateGuamodTurnOnDelegate(AppVars.MainForm.UpdateGuamodTurnOn),
                                    new object[] { });
                            }
                        }
                        catch (InvalidOperationException)
                        {
                        }
                    }
                    else
                    {
                        EventSounds.PlayDigits();
                        try
                        {
                            if (AppVars.MainForm != null)
                            {
                                AppVars.MainForm.BeginInvoke(
                                    new UpdateTrayFlashDelegate(AppVars.MainForm.UpdateTrayFlash),
                                    new object[] { "Ввод цифр" });
                            }
                        }
                        catch (InvalidOperationException)
                        {
                        }
                    }

                    try
                    {
                        if (AppVars.MainForm != null)
                        {
                            AppVars.MainForm.BeginInvoke(
                                new UpdateTrayFlashDelegate(AppVars.MainForm.UpdateTrayFlash),
                                new object[] { "Ввод цифр" });
                        }
                    }
                    catch (InvalidOperationException)
                    {
                    }
                }
            }

            return AppVars.Profile.DoGuamod ? @"<br><img src=http://image.neverlands.ru/1x1.gif width=1 height=8><br><span id=guamod3><font class=nickname><font color=#004A7F><b>* * * *</b></font></font></span>" : string.Empty;
        }

        internal static string CheckPri(string namePri, int myst)
        {
            if (AppVars.PriSelected || myst <= 4)
            {
                return string.Empty;
            }

            if (namePri.Equals("Хлеб", StringComparison.OrdinalIgnoreCase) && (AppVars.Profile.FishEnabledPrims & Prims.Bread) != 0)
            {
                AppVars.PriSelected = true;
                AppVars.NamePri = namePri;
                AppVars.ValPri = myst;
                return " CHECKED";
            }

            if (namePri.Equals("Червяк", StringComparison.OrdinalIgnoreCase) && (AppVars.Profile.FishEnabledPrims & Prims.Worm) != 0)
            {
                AppVars.PriSelected = true;
                AppVars.NamePri = namePri;
                AppVars.ValPri = myst;
                return " CHECKED";
            }

            if (namePri.Equals("Крупный червяк", StringComparison.OrdinalIgnoreCase) && (AppVars.Profile.FishEnabledPrims & Prims.BigWorm) != 0)
            {
                AppVars.PriSelected = true;
                AppVars.NamePri = namePri;
                AppVars.ValPri = myst;
                return " CHECKED";
            }

            if (namePri.Equals("Опарыш", StringComparison.OrdinalIgnoreCase) && (AppVars.Profile.FishEnabledPrims & Prims.Stink) != 0)
            {
                AppVars.PriSelected = true;
                AppVars.NamePri = namePri;
                AppVars.ValPri = myst;
                return " CHECKED";
            }

            if (namePri.Equals("Мотыль", StringComparison.OrdinalIgnoreCase) && (AppVars.Profile.FishEnabledPrims & Prims.Fly) != 0)
            {
                AppVars.PriSelected = true;
                AppVars.NamePri = namePri;
                AppVars.ValPri = myst;
                return " CHECKED";
            }

            if (namePri.Equals("Блесна", StringComparison.OrdinalIgnoreCase) && (AppVars.Profile.FishEnabledPrims & Prims.Light) != 0)
            {
                AppVars.PriSelected = true;
                AppVars.NamePri = namePri;
                AppVars.ValPri = myst;
                return " CHECKED";
            }

            if (namePri.Equals("Донка", StringComparison.OrdinalIgnoreCase) && (AppVars.Profile.FishEnabledPrims & Prims.Donka) != 0)
            {
                AppVars.PriSelected = true;
                AppVars.NamePri = namePri;
                AppVars.ValPri = myst;
                return " CHECKED";
            }

            if (namePri.Equals("Мормышка", StringComparison.OrdinalIgnoreCase) && (AppVars.Profile.FishEnabledPrims & Prims.Morm) != 0)
            {
                AppVars.PriSelected = true;
                AppVars.NamePri = namePri;
                AppVars.ValPri = myst;
                return " CHECKED";
            }

            if (namePri.Equals("Заговоренная блесна", StringComparison.OrdinalIgnoreCase) && (AppVars.Profile.FishEnabledPrims & Prims.HiFlight) != 0)
            {
                AppVars.PriSelected = true;
                AppVars.NamePri = namePri;
                AppVars.ValPri = myst;
                return " CHECKED";
            }

            return string.Empty;
        }

        internal static bool IsCellExists(int x, int y)
        {
            var coor = Map.MakePosition(x, y);
            if (!Map.Location.ContainsKey(coor))
            {
                return false;
            }

            var regnum = Map.Location[coor].RegNum;
            if (regnum == null)
            {
                return false;
            }

            var result = Map.Cells.ContainsKey(regnum);
            return result;
        }

        internal static string GetCellLabel(int x, int y)
        {
            var coor = Map.MakePosition(x, y);
            if (!Map.Location.ContainsKey(coor))
            {
                return string.Empty;
            }

            var regnum = Map.Location[coor].RegNum;
            if (regnum == null)
            {
                return string.Empty;
            }

            if (!Map.Cells.ContainsKey(regnum))
            {
                return string.Empty;
            }

            var result = Map.Cells[regnum].Tooltip;
            return result;
        }

        internal static string GetRegionBorders(string framelabel, int x, int y)
        {
            if (!AppVars.Profile.MapDrawRegion)
                return "00001";

            var sb = new StringBuilder(6);
            sb.Append("00000");

            var label = GetCellLabel(x, y);
            if (label.Equals(framelabel, StringComparison.CurrentCultureIgnoreCase))
                sb[4] = '1';
            
            var nlabel = GetCellLabel(x, y - 1);
            if ((label.Equals(framelabel, StringComparison.CurrentCultureIgnoreCase) || nlabel.Equals(framelabel, StringComparison.CurrentCultureIgnoreCase)) &&
                !label.Equals(nlabel, StringComparison.CurrentCultureIgnoreCase))
                sb[0] = '1';

            nlabel = GetCellLabel(x - 1, y);
            if ((label.Equals(framelabel, StringComparison.CurrentCultureIgnoreCase) || nlabel.Equals(framelabel, StringComparison.CurrentCultureIgnoreCase)) &&
                !label.Equals(nlabel, StringComparison.CurrentCultureIgnoreCase))
                sb[1] = '1';

            nlabel = GetCellLabel(x + 1, y);
            if ((label.Equals(framelabel, StringComparison.CurrentCultureIgnoreCase) || nlabel.Equals(framelabel, StringComparison.CurrentCultureIgnoreCase)) &&
                !label.Equals(nlabel, StringComparison.CurrentCultureIgnoreCase))
                sb[2] = '1';

            nlabel = GetCellLabel(x, y + 1);
            if ((label.Equals(framelabel, StringComparison.CurrentCultureIgnoreCase) || nlabel.Equals(framelabel, StringComparison.CurrentCultureIgnoreCase)) &&
                !label.Equals(nlabel, StringComparison.CurrentCultureIgnoreCase))
                sb[3] = '1';

            return sb.ToString();
        }

        internal static string GenMoveLink(int x, int y)
        {
            var coor = Map.MakePosition(x, y);
            if (coor == null)
            {
                return string.Empty;
            }

            Position position;
            if (Map.Location.TryGetValue(coor, out position))
            {
                var regnum = Map.Location[coor].RegNum;
                return regnum;
            }

            return string.Empty;            
        }

        internal static void MakeVisit(int x, int y)
        {
            var coor = Map.MakePosition(x, y);
            if (coor == null)
                return;

            Position position;
            if (Map.Location.TryGetValue(coor, out position))
            {
                var regnum = Map.Location[coor].RegNum;
                AbcCell abccell;
                if (Map.AbcCells.TryGetValue(regnum, out abccell))
                {
                    abccell.Visited = (AppVars.Profile.ServDiff == TimeSpan.MinValue) ? DateTime.Now : DateTime.Now.Subtract(AppVars.Profile.ServDiff); ;
                }
            }
        }

        internal void UpdateServerTime(DateTime serverDateTime)
        {
            AppVars.ServerDateTime = serverDateTime;

            /*
            if (AppVars.NoHomeServerAvailable)
            {
                return;
            }
             */ 

            /*
            if (Key.KeyFile.Status == Key.KeyFileStatus.UnknownUser)
            {
                Key.KeyFile.Status = Key.KeyFileStatus.NoNeverLicence;

                var key = Encoding.ASCII.GetBytes("p@ssw0rdDR0wSS@P6660juht");
                var iv = Encoding.ASCII.GetBytes("p@ssw0rd");
                var askCode = string.Format(
                    CultureInfo.InvariantCulture, 
                    "{0}|{1}|{2}", 
                    AppVars.AppVersion.Product,
                    AppVars.Profile.UserNick, 
                    DateTime.Now);
                var data = AppVars.Codepage.GetBytes(askCode);
                var tdes = TripleDES.Create();
                tdes.IV = iv;
                tdes.Key = key;
                tdes.Mode = CipherMode.CBC;
                tdes.Padding = PaddingMode.Zeros;
                var ict = tdes.CreateEncryptor();
                var enc = ict.TransformFinalBlock(data, 0, data.Length);
                var ask = Convert.ToBase64String(enc);
                WebResponse response = null;
                try
                {                    
                    var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://www.neverlands.ru/modules/abclient/auth.php");
                    httpWebRequest.Method = "POST";
                    httpWebRequest.Proxy = AppVars.LocalProxy;
                    var postData = string.Format(
                        CultureInfo.InvariantCulture,
                        "ask={0}",
                        HttpUtility.UrlEncode(ask));
                    var byteArray = Encoding.ASCII.GetBytes(postData);
                    httpWebRequest.ContentLength = byteArray.Length;
                    httpWebRequest.ContentType = "application/x-www-form-urlencoded";
                    var postStream = httpWebRequest.GetRequestStream();
                    postStream.Write(byteArray, 0, byteArray.Length);
                    postStream.Close();
                    response = httpWebRequest.GetResponse();
                    var receiveStream = response.GetResponseStream();
                    if (receiveStream != null)
                        using (var readStream = new StreamReader(receiveStream, Helpers.Russian.Codepage))
                        {
                            var result = readStream.ReadToEnd();
                            var encoded = HttpUtility.UrlDecode(result);
                            var datad = Convert.FromBase64String(encoded);
                            var ictd = tdes.CreateDecryptor();
                            var dec = ictd.TransformFinalBlock(datad, 0, datad.Length);
                            var restr = AppVars.Codepage.GetString(dec);
                            var par = restr.Split('|');
                            if (par.Length < 2 || (!par[0].Equals("0", StringComparison.Ordinal) && !par[0].Equals("1", StringComparison.Ordinal)))
                            {
                                Key.KeyFile.Status = Key.KeyFileStatus.NoNeverLicence;
                                MessageBox.Show(
                                    "Ошибочный серверный ключ",
                                    "Ошибка проверки лицензии",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                            }
                            else
                            {
                                if (par[0].Equals("1", StringComparison.Ordinal))
                                {
                                    Key.KeyFile.Status = Key.KeyFileStatus.Ok;
                                    TurnPay();
                                }
                                else
                                {
                                    Key.KeyFile.Status = Key.KeyFileStatus.NoNeverLicence;    
                                }
                            }
                        }
                }
                catch (Exception exception)
                {
                    Key.KeyFile.Status = Key.KeyFileStatus.NoNeverLicence;
                    MessageBox.Show(
                        exception.Message,
                        "Ошибка проверки лицензии",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
                finally
                {
                    if (response != null)
                    {
                        response.Close();
                    }
                }
            }
            else
            {
             */
                //Key.KeyFile.CheckExpiration(serverDateTime);

                

                //if (Key.KeyFile.Status != Key.KeyFileStatus.Ok)
        }

        internal void AddAddressToStatusString(string address)
        {
            if (InvokeRequired)
            {
                BeginInvoke((MethodInvoker)(() => AddAddressToStatusString(address)));
                return;
            }

            try
            {
                LockAddressStatus.AcquireWriterLock(5000);
                try
                {
                    labelAddress.Text = LoadingUrlList.Add(address);
                }
                finally
                {
                    LockAddressStatus.ReleaseWriterLock();
                }
            }
            catch (ApplicationException)
            {
            }
        }

        internal void RemoveAddressFromStatusString(string address)
        {
            if (InvokeRequired)
            {
                BeginInvoke((MethodInvoker)(() => RemoveAddressFromStatusString(address)));
                return;
            }

            try
            {
                LockAddressStatus.AcquireWriterLock(5000);
                try
                {
                    var actualaddress = LoadingUrlList.Remove(address);
                    labelAddress.Text = actualaddress;
                }
                finally
                {
                    LockAddressStatus.ReleaseWriterLock();
                }
            }
            catch (ApplicationException)
            {
            }
        }

        internal void ShowActivity(int numberOfThreads)
        {
            var index = numberOfThreads > 0 ? 1 : 0;
            statuslabelActivity.Image = imagelistDownload.Images[index];
            var indication = numberOfThreads > 9 ? 9 : numberOfThreads;
            statuslabelNumberOfThreads.Text = indication.ToString();
        }

        internal void UpdateHttpLog(string message)
        {
            // UpdateTexLog(message);
        }

        internal void UpdateTexLog(string message)
        {
            if (!AppVars.Profile.DoTexLog)
            {
                return;
            }

            var sb = new StringBuilder();
            sb.AppendFormat(
                CultureInfo.InvariantCulture,
                "{0:00}:{1:00}:{2:00}  {3}",
                DateTime.Now.Hour, 
                DateTime.Now.Minute, 
                DateTime.Now.Second,
                message);
            sb.AppendLine();
            sb.Append(textboxTexLog.Text);
            while (sb.Length > 0x4000)
            {
                sb.Length = 0x4000;
            }

            textboxTexLog.Text = sb.ToString();
        }

        private void UpdateXPInc(long xpinc)
        {
            Interlocked.Add(ref AppVars.Profile.Stat.XP, xpinc);
            UpdateXP();
        }

        private void UpdateThingInc(string timestr, List<string> thinglist)
        {
            if (AppVars.Profile.ShowTrayBaloons)
            {
                var sbline = new StringBuilder();
                foreach (var thing in thinglist)
                {
                    if (sbline.Length > 0)
                        sbline.Append(", ");

                    sbline.Append(thing);
                }

                UpdateTrayBaloon(sbline.ToString());
            }

            foreach (var thing in thinglist)
            {
                var str = string.Format("{0} {1}", timestr, thing);
                var sp = AppVars.Profile.Stat.Drop.Split('|');
                int i;
                for (i = 0; i < sp.Length; i++)
                {
                    if (str.Equals(sp[i], StringComparison.OrdinalIgnoreCase))
                    {
                        break;
                    }
                }

                if (i != sp.Length)
                {
                    continue;
                }

                if (thing.EndsWith("NV", StringComparison.OrdinalIgnoreCase))
                {
                    int nvinc;
                    if (int.TryParse(thing.Substring(0, thing.Length - 3), out nvinc))
                    {
                        Interlocked.Add(ref AppVars.Profile.Stat.NV, nvinc);
                        UpdateNV();
                    }
                }
                else
                {
                    var sb = new StringBuilder(AppVars.Profile.Stat.Drop);
                    if (sb.Length > 0)
                        sb.Append('|');

                    sb.Append(str);
                    AppVars.Profile.Stat.Drop = sb.ToString();

                    var item = new TypeItemDrop {Name = thing, Count = 1};
                    var added = false;
                    var index = 0;
                    while (index < AppVars.Profile.Stat.ItemDrop.Count)
                    {
                        var result = String.Compare(thing, AppVars.Profile.Stat.ItemDrop[index].Name,
                                                    StringComparison.Ordinal);
                        if (result == 0)
                        {
                            AppVars.Profile.Stat.ItemDrop[index].Count++;
                            added = true;
                            break;
                        }

                        if (result < 0)
                        {
                            AppVars.Profile.Stat.ItemDrop.Insert(index, item);
                            added = true;
                            break;
                        }

                        index++;
                    }

                    if (!added)
                    {
                        AppVars.Profile.Stat.ItemDrop.Add(item);
                    }
                }
            }
        }

        internal void UpdateFishNV(int nvinc)
        {
            Interlocked.Add(ref AppVars.Profile.Stat.FishNV, nvinc);
            UpdateFishNV();
        }

        internal void UpdateAccountError(string error)
        {
            AppVars.AccountError = error;
            AppVars.DoPromptExit = false;
            Close();
        }

        /*
        internal void UpdateLocation(string location)
        {
            AppVars.LocationReal = string.IsNullOrEmpty(location) ? "?-???" : location;
            statuslabelLocation.Text = AppVars.LocationReal;
        }
         */ 

        internal void UpdateLocationSafe(string location)
        {
            if (InvokeRequired)
            {
                BeginInvoke((MethodInvoker)(() => UpdateLocationSafe(location)));
                return;
            }

            AppVars.LocationReal = string.IsNullOrEmpty(location) ? "?-???" : location;
            statuslabelLocation.Text = AppVars.LocationReal;

            if (Map.AbcCells.ContainsKey(AppVars.LocationReal))
            {
                Map.AbcCells[AppVars.LocationReal].Visited = DateTime.Now;
            }
        }

        /*
        internal void UpdateTopFrame(string message)
        {
            UpdateTexLog(message);
            ReloadMainTopSafe();
        }
         */ 

        internal void AutoboiOff()
        {
            AppVars.Profile.LezDoAutoboi = false;
            ChangeAutoboiState(AutoboiState.AutoboiOff);
        }

        internal void FuryOff()
        {
            AppVars.DoFury = false;
            buttonFury.Checked = false;
        }

        internal void UpdateGame(string message)
        {
            if (AppVars.MustReload)
                return;

            try
            {
                if (AppVars.MainForm != null)
                {
                    AppVars.MainForm.BeginInvoke(
                        new UpdateTexLogDelegate(AppVars.MainForm.UpdateTexLog),
                        new object[] { $"UpdateGame({message})" });
                }
            }
            catch (InvalidOperationException)
            {
            }

            AppVars.MustReload = true;
        }

        internal void UpdateAutoboiReset()
        {
            if (AppVars.Autoboi != AutoboiState.AutoboiOff)
            {
                ChangeAutoboiState(AutoboiState.AutoboiOn);
            }
        }

        internal void UpdateTied(int tied)
        {
            AppVars.Tied = (tied < 0) ? 0 : tied;
            var sb = new StringBuilder("Усталость: ");
            sb.Append(AppVars.Tied);
            sb.Append('%');
            statuslabelTied.Text = sb.ToString();
            AppVars.LastTied = DateTime.Now;
            AppVars.SwitchToPerc = false;

            if (AppVars.Tied > 0)
            {
                return;
            }

            AppVars.AutoDrink = false;
            AppVars.AutoFishDrink = false;
            buttonDrink.Checked = false;
        }

        internal static void UpdateCheckTied()
        {
            CheckTied();
        }

        internal void UpdateTrayBaloon(string message)
        {
            try
            {
                LockBaloon.AcquireWriterLock(5000);
                try
                {
                    trayIcon.ShowBalloonTip(
                        10000,
                        AppVars.AppVersion.ProductShortVersion,
                        message,
                        ToolTipIcon.None);
                }
                finally
                {
                    LockBaloon.ReleaseWriterLock();
                }
            }
            catch (ApplicationException)
            {
            }
        }

        internal void UpdateTrayFlash(string message)
        {
            TrayFlash(message);
        }

        internal void UpdateNavigatorOff()
        {
            buttonNavigator.Checked = false;
            menuitemDoSearchBox.Checked = false;
            AppVars.DoSearchBox = false;
            AppVars.AutoMoving = false;
        }

        internal void UpdateFishOff()
        {
            buttonAutoFish.Checked = false;
            AppVars.Profile.FishAuto = false;
            AppVars.Profile.Save();
        }

        internal void UpdateComplects(string[] complects)
        {
            var sb = new StringBuilder();
            miWearAfter.DropDownItems.Clear();
            // this.menuitemCheckCell.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D1)));
            int d = 0;
            Keys[] keys =
            {
                Keys.Control | Keys.D1,
                Keys.Control | Keys.D2,
                Keys.Control | Keys.D3,
                Keys.Control | Keys.D4,
                Keys.Control | Keys.D5,
                Keys.Control | Keys.D6,
                Keys.Control | Keys.D7,
                Keys.Control | Keys.D8,
                Keys.Control | Keys.D9,
                Keys.Control | Keys.D0
            };
            
            foreach (var complect in complects)
            {
                var tsmi = new ToolStripMenuItem(complect);                
                if (d < keys.Length)
                    tsmi.ShortcutKeys = keys[d];
                //if (d == 0)
                //    tsmi.ShortcutKeys = Keys.Control | Keys.D1;

                d++;
                tsmi.Click += TsmiClick;
                miWearAfter.DropDownItems.Add(tsmi);
                if (sb.Length > 0)
                {
                    sb.Append("|");
                }

                sb.Append(complect);
            }

            AppVars.Profile.Complects = sb.ToString();
        }

        void TsmiClick(object sender, EventArgs e)
        {
            var complect = ((ToolStripMenuItem) sender).Text;
            foreach (ToolStripMenuItem tsmi in miWearAfter.DropDownItems)
            {
                if (tsmi.Text.Equals(complect))
                {
                    if (!tsmi.Checked)
                    {
                        tsmi.Checked = true;
                        AppVars.WearComplect = complect;
                        try
                        {
                            if (AppVars.MainForm != null)
                            {
                                AppVars.MainForm.BeginInvoke(
                                    new UpdateChatDelegate(AppVars.MainForm.UpdateChat),
                                    new object[] { string.Format("Заказано одевание комплекта ({0})", complect) });
                            }
                        }
                        catch (InvalidOperationException)
                        {
                        }

                        try
                        {
                            if (AppVars.MainForm != null)
                            {
                                AppVars.MainForm.BeginInvoke(
                                    new ReloadMainPhpInvokeDelegate(AppVars.MainForm.ReloadMainPhpInvoke),
                                    new object[] { });
                            }
                        }
                        catch (InvalidOperationException)
                        {
                        }
                    }
                    else
                    {
                        tsmi.Checked = false;                        
                        try
                        {
                            if (AppVars.MainForm != null)
                            {
                                AppVars.MainForm.BeginInvoke(
                                    new UpdateChatDelegate(AppVars.MainForm.UpdateChat),
                                    new object[] { string.Format("Одевание комплекта '{0}' отменено", complect) });
                            }
                        }
                        catch (InvalidOperationException)
                        {
                        }

                        AppVars.WearComplect = string.Empty;
                    }
                }
                else
                {
                    tsmi.Checked = false;
                }
            }
        }

        internal void UpdateRoom(ToolStripItem[] tsmi, string trtext, ToolStripItem[] tstr)
        {
            dropdownPv.DropDownItems.Clear();
            dropdownPv.Text = (tsmi.Length == 0) ? string.Empty : "ПВ: " + tsmi.Length;
            if (tsmi.Length > 0)
            {
                dropdownPv.DropDownItems.AddRange(tsmi);
                dropdownPv.Enabled = true;
            }
            else
            {
                dropdownPv.Enabled = false;
            }

            dropdownTravm.DropDownItems.Clear();
            dropdownTravm.Text = trtext;
            if (tstr.Length > 0)
            {
                dropdownTravm.DropDownItems.AddRange(tstr);
                dropdownTravm.Enabled = true;
            }
            else
            {
                dropdownTravm.Enabled = false;
            }
        }

        internal void UpdateChat(string message)
        {
            WriteChatMsg(message);
        }

        internal void UpdateContact(Contact ce)
        {
            ContactsManager.Update(treeContacts, ce);
        }

        internal void TraceDrinkPotion(string wnickname, string wnametxt)
        {
            if (!wnickname.Equals(AppVars.Profile.UserNick, StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            var h = 0;
            switch (wnametxt)
            {
                case "Зелье Метаболизма":
                    h = 4;
                    break;
                case "Зелье Сильной Спины":
                    h = 20;
                    break;
                case "Зелье Просветления":
                    h = 5;
                    break;
                case "Зелье Сокрушительных Ударов":
                    h = 3;
                    break;
                case "Зелье Стойкости":
                    h = 3;
                    break;
                case "Зелье Недосягаемости":
                    h = 3;
                    break;
                case "Зелье Точного Попадания":
                    h = 3;
                    break;
                case "Зелье Ловких Ударов":
                    h = 2;
                    break;
                case "Зелье Мужества":
                    h = 2;
                    break;
                case "Зелье Жизни":
                    h = 3;
                    break;
                case "Зелье Удачи":
                    h = 3;
                    break;
                case "Зелье Силы":
                    h = 3;
                    break;
                case "Зелье Ловкости":
                    h = 3;
                    break;
                case "Зелье Гения":
                    h = 3;
                    break;
                case "Зелье Боевой Славы":
                    h = 2;
                    break;
                case "Зелье Невидимости":
                    h = 3;
                    break;
                case "Зелье Секрет Волшебника":
                    h = 2;
                    break;
                case "Зелье Медитации":
                    h = 4;
                    break;
                case "Зелье Иммунитета":
                    h = 10;
                    break;
                case "Зелье Огненного Ореола":
                    h = 3;
                    break;
                case "Зелье Колкости":
                    h = 2;
                    break;
                case "Зелье Загрубелой Кожи":
                    h = 3;
                    break;
                case "Зелье Панциря":
                    h = 2;
                    break;
                case "Зелье Человек-гора":
                    h = 2;
                    break;
                case "Зелье Скорости":
                    h = 1;
                    break;
                case "Зелье Подвижности":
                    h = 3;
                    break;
                case "Зелье Соколиный Взор":
                    h = 3;
                    break;
                case "Секретное Зелье":
                    h = 2;
                    break;
            }

            if (h <= 0)
            {
                return;
            }

            var appTimer = new AppTimer
                               {
                                   Description = string.Format("Действует {0}", wnametxt),
                                   TriggerTime = DateTime.Now.AddHours(h)
                               };
            AppTimerManager.AddAppTimer(appTimer);
            UpdateTimers();
        }

        internal void ShowMiniMap(bool show)
        {
            AppVars.Profile.MapShowMiniMap = show;

            try
            {
                if (AppVars.MainForm != null)
                {
                    AppVars.MainForm.BeginInvoke(
                        new ReloadMainPhpInvokeDelegate(AppVars.MainForm.ReloadMainPhpInvoke),
                        new object[] { });
                }
            }
            catch (InvalidOperationException)
            {
            }
        }

        /*
        internal void UpdateGuamodProgress()
        {
            if (AppVars.Autoboi == AutoboiState.Guamod)
            {
                ChangeAutoboiState(AutoboiState.Guamod);
            }
        }
         */ 

        internal void UpdateGuamodTurnOn()
        {
            if (!menuitemGuamod.Enabled || menuitemGuamod.Checked)
            {
                return;
            }

            menuitemGuamod.Checked = true;
            AppVars.Profile.DoGuamod = menuitemGuamod.Checked;
            try
            {
                if (AppVars.MainForm != null)
                {
                    AppVars.MainForm.BeginInvoke(
                        new UpdateChatDelegate(AppVars.MainForm.UpdateChat),
                        new object[] { "Код долго не вводится, принудительно включен автоввод" });
                }
            }
            catch (InvalidOperationException)
            {
            }

            timerTray.Stop();
            trayIcon.Text = AppVars.Profile.UserNick;
            TrayShowFrame(0);

            try
            {
                if (AppVars.MainForm != null)
                {
                    AppVars.MainForm.BeginInvoke(
                        new ReloadMainPhpInvokeDelegate(AppVars.MainForm.ReloadMainPhpInvoke),
                        new object[] { });
                }
            }
            catch (InvalidOperationException)
            {
            }
        }

        internal void UpdateGuamodMessage(string message)
        {
            WriteMessageToGuamod("<font class=nickname><font color=#004A7F><b>" + message + "</b></font></font>");
        }

        /*
        internal void WriteFishingCode(string code)
        {
            EnterFishCode(code);
        }
         */ 

        internal void NavigatorOffInvoke()
        {
            buttonNavigator.Checked = false;
            AppVars.AutoMoving = false;
        }

        internal void UpdateCheckTiedSafe()
        {
            if (InvokeRequired)
            {
                BeginInvoke((MethodInvoker)UpdateCheckTiedSafe);
                return;
            }

            CheckTied();
        }

        internal void UpdateTrayBaloonSafe(string message)
        {
            if (InvokeRequired)
            {
                BeginInvoke((MethodInvoker)(() => UpdateTrayBaloonSafe(message)));
                return;
            }

            try
            {
                LockBaloon.AcquireWriterLock(5000);
                try
                {
                    trayIcon.ShowBalloonTip(
                        10000,
                        AppVars.AppVersion.ProductShortVersion,
                        message,
                        ToolTipIcon.None);
                }
                finally
                {
                    LockBaloon.ReleaseWriterLock();
                }
            }
            catch (ApplicationException)
            {
            }
        }

        internal static string ShowHpMaTimers(string inner, double curHP, int maxHP, double intHP, double curMA, int maxMA, double intMA)
        {            
            var sb = new StringBuilder("<FONT class=hpfont>: ");
            sb.Append("[<font color=#bb0000>");
            sb.AppendFormat("<b>{0}</b>", (int)curHP);
            sb.Append("/");
            sb.AppendFormat("<b>{0}</b>", maxHP);

            var seconds = (int)Math.Round(((maxHP - curHP) * intHP) / maxHP);
            if (seconds > 0)
                sb.AppendFormat(" (<b>{0:00}:{1:00}:{2:00}</b>)", seconds / 3600, (seconds / 60) % 60, seconds % 60);

            sb.Append("</font>]");

            sb.Append(" | ");

            sb.Append("[<font color=#336699>");
            sb.AppendFormat("<b>{0}</b>", (int)curMA);
            sb.Append("/");
            sb.AppendFormat("<b>{0}</b>", maxMA);

            seconds = (int)Math.Round(((maxMA - curMA) * intMA) / maxMA);
            if (seconds > 0)
                sb.AppendFormat(" (<b>{0:00}:{1:00}:{2:00}</b>)", seconds / 3600, (seconds / 60) % 60, seconds % 60);

            sb.Append("</font>]</font>");

            return sb.ToString();

            //"\"[<font color=#bb0000><b>\" + Math.round(curHP)+\"</b>/<b>\"+maxHP+\"</b></font> | <font color=#336699><b>\"+Math.round(curMA)+\"</b>/<b>\"+maxMA+\"</b></font>]\"",
        }
    }
}