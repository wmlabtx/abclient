namespace ABClient.ABForms
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Windows.Forms;
    using System.Xml;
    using MyForms;
    using MyHelpers;

    internal sealed partial class FormMain
    {
        private void CheckNewVersion(bool manual)
        {
            var wc = new WebClient { Proxy = AppVars.LocalProxy };
            byte[] bd;
            try
            {
                IdleManager.AddActivity();
                var url = $"http://{CuteConsts.ABClientHostName}/{CuteConsts.ABClientCheckVersionPhp}.php";
                bd = wc.DownloadData(new Uri(url));
            }
            catch (Exception)
            {
                bd = null;
            }
            finally
            {
                IdleManager.RemoveActivity();
            }

            if (bd == null)
            {
                try
                {
                    IdleManager.AddActivity();
                    var url = $"http://{CuteConsts.ABClientHostName}/{CuteConsts.ABClientCheckVersionPhp}.php";
                    bd = wc.DownloadData(new Uri(url));
                }
                catch (Exception)
                {
                    bd = null;
                }
                finally
                {
                    IdleManager.RemoveActivity();
                }
            }

            CheckNewVersionCompleted(bd, manual);
        }

        private void CheckNewVersionCompleted(byte[] bd, bool manual)
        {
            if (bd == null)
                return;

            string textdata = System.Text.Encoding.UTF8.GetString(bd);
            if (string.IsNullOrEmpty(textdata))
                return;

            var par = HelperStrings.SubString(textdata, AppConsts.SpanEnabledVersions, AppConsts.SpanClose);
            if (par == null)
                return;

            try
            {
                var buffer = Convert.FromBase64String(par);
                par = Encoding.ASCII.GetString(buffer);
            }
            catch (FormatException)
            {
                par = null;
            }

            if (par == null)
                return;

            var arg = par.Split('|');
            if (arg.Length < 2)
                return;

            if (!manual)
            {
                if (!AppVars.AppVersion.IsCurrentVersionAllowed(arg))
                {
                    try
                    {
                        if (AppVars.MainForm != null)
                            AppVars.MainForm.BeginInvoke(
                                new UpdateAccountErrorDelegate(AppVars.MainForm.UpdateAccountError), "Обновите ABClient!");
                    }
                    catch (InvalidOperationException)
                    {
                    }

                    return;
                }
            }

            par = HelperStrings.SubString(textdata, AppConsts.SpanLastVersion, AppConsts.SpanClose);
            if (par == null)
                return;

            try
            {
                var buffer = Convert.FromBase64String(par);
                par = Encoding.ASCII.GetString(buffer);
            }
            catch (FormatException)
            {
                par = null;
            }

            if (par == null)
                return;

            arg = par.Split('|');
            if (arg.Length < 1)
                return;

            var spanObraz = HelperStrings.SubString(textdata, AppConsts.SpanObraz, AppConsts.SpanClose);
            if (spanObraz != null)
            {
                string xmlObraz;
                try
                {
                    var buffer = Convert.FromBase64String(spanObraz);
                    xmlObraz = Helpers.Russian.Codepage.GetString(buffer);
                }
                catch (FormatException)
                {
                    xmlObraz = null;
                }

                if (AppVars.UserObrazes == null)
                {
                    AppVars.UserObrazes = new SortedDictionary<string, string>();
                }
                else
                {
                    try
                    {
                        LockOb.AcquireWriterLock(5000);
                        try
                        {
                            AppVars.UserObrazes.Clear();
                        }
                        finally
                        {
                            LockOb.ReleaseWriterLock();
                        }
                    }
                    catch (ApplicationException)
                    {
                    }
                }
                
                if (!string.IsNullOrEmpty(xmlObraz))
                {
                    var bufferObraz = Helpers.Russian.Codepage.GetBytes(xmlObraz);
                    using (var streamObraz = new MemoryStream(bufferObraz))
                    {
                        var xmlReaderSettings = new XmlReaderSettings
                                                    {
                                                        IgnoreComments = true,
                                                        IgnoreWhitespace = true,
                                                        ConformanceLevel = ConformanceLevel.Auto
                                                    };

                        XmlReader xmlReader = null;
                        try
                        {
                            xmlReader = XmlReader.Create(streamObraz, xmlReaderSettings);
                            while (xmlReader.Read())
                            {
                                if (xmlReader.NodeType != XmlNodeType.Element)
                                {
                                    continue;
                                }

                                if (!xmlReader.Name.Equals(AppConsts.ConstTagObraz, StringComparison.OrdinalIgnoreCase))
                                {
                                    continue;
                                }

                                var enabled = xmlReader[AppConsts.ConstAttibuteObrazEnabled] ?? string.Empty;
                                if (!enabled.Equals(AppConsts.ConstAttibuteObrazIsEnabled))
                                {
                                    continue;
                                }

                                var nick = xmlReader[AppConsts.ConstAttibuteObrazNick] ?? string.Empty;
                                if (string.IsNullOrEmpty(nick))
                                {
                                    continue;
                                }

                                var obraz = xmlReader[AppConsts.ConstAttibuteObrazImage] ?? string.Empty;
                                if (string.IsNullOrEmpty(obraz))
                                {
                                    continue;
                                }

                                try
                                {
                                    LockOb.AcquireWriterLock(5000);
                                    try
                                    {
                                        AppVars.UserObrazes.Add(nick.ToUpper(Helpers.Russian.Culture), obraz);
                                    }
                                    finally
                                    {
                                        LockOb.ReleaseWriterLock();
                                    }
                                }
                                catch (ApplicationException)
                                {
                                }
                            }
                        }
                        catch (IOException)
                        {
                        }
                        catch (ArgumentNullException)
                        {
                        }
                        catch (InvalidOperationException)
                        {
                        }
                        catch (ArgumentException)
                        {
                        }
                        catch (NotSupportedException)
                        {
                        }
                        catch (UnauthorizedAccessException)
                        {
                        }
                        catch (XmlException)
                        {
                        }
                        finally
                        {
                            if (xmlReader != null)
                            {
                                xmlReader.Close();
                            }
                        }
                    }
                }
            }

            if (DateTime.Now > AppVars.Profile.NextCheckVersion)
            {
                if (AppVars.AppVersion.IsNewerVersionOnSite(arg[0]))
                {
                    using (var ff = new FormNewVersion("Доступна версия " + arg[0]))
                    {
                        ff.ShowDialog();
                    }
                }
                else
                {
                    if (manual)
                    {
                        MessageBox.Show(
                            "У Вас свежая версия клиента",
                            AppVars.AppVersion.ProductShortVersion,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                }
            }
        }
    }
}