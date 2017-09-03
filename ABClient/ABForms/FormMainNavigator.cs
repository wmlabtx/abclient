using System.Collections;
using System.Collections.Specialized;
using ABClient.MyForms;

namespace ABClient.ABForms
{
    using System;
    using System.Windows.Forms;
    using ExtMap;

    internal sealed partial class FormMain
    {
        internal void MoveToDialog(string dest)
        {
            if (string.IsNullOrEmpty(dest))
            {
                MessageBox.Show(
                    @"Местоположение неизвестно." + Environment.NewLine + @"Сначала посмотрите на карту!",
                    AppVars.AppVersion.ProductShortVersion,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Stop);
                return;
            }

            if (buttonNavigator.Checked)
            {
                buttonNavigator.Checked = false;
                AppVars.AutoMoving = false;
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
                using (var ff = new FormNavigator(dest, null, null, null))
                {
                    if (ff.ShowDialog() != DialogResult.OK) return;
                    buttonNavigator.Checked = true;
                    UpdateFishOff();
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
            }
        }

        internal void MoveToSafe(string dest)
        {
            if (InvokeRequired)
            {
                BeginInvoke((MethodInvoker)(() => MoveToSafe(dest)));
                return;
            }

            MoveTo(dest);
        }

        internal void MoveTo(string dest)
        {
            buttonNavigator.Checked = true;
            AppVars.AutoMoving = true;
            AppVars.AutoMovingDestinaton = dest;
            var path = new MapPath(AppVars.Profile.MapLocation, new[] { dest });
            if (path.IsIslandRequired)
                AppVars.MainForm.FastStartSafe("Телепорт (Остров Туротор)", AppVars.Profile.UserNick);

            AppVars.AutoMovingMapPath = path;

            UpdateFishOff();
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

        internal static string FindNextDestForBox()
        {
            var idx = new[] {0, 0, -1, 1, -1, 1, -1, 1};
            var idy = new[] {-1, 1, 0, 0, -1, -1, 1, 1};

            var ht = new Hashtable();
            var sn = new StringCollection();
            ht.Clear();
            ht.Add(AppVars.Profile.MapLocation, 0);
            sn.Clear();
            sn.Add(AppVars.Profile.MapLocation);

            var iter = 0;
            var snew = new StringCollection();
            while (sn.Count > 0)
            {
                foreach (var c1 in sn)
                {
                    Cell cell;
                    if (!Map.Cells.TryGetValue(c1, out cell))
                    {
                        continue;
                    }

                    for (var i = 0; i < 8; i++)
                    {
                        var newLoc = Map.Move(c1, idx[i], idy[i]);
                        if (string.IsNullOrEmpty(newLoc))
                            continue;

                        if (ht.ContainsKey(newLoc))
                            continue;

                        ht.Add(newLoc, iter + 1);
                        snew.Add(newLoc);

                        if (DateTime.Now.Subtract(Map.AbcCells[newLoc].Visited).TotalDays >= 1.0)
                            return newLoc;
                    }
                }

                sn.Clear();
                foreach (var c2 in snew)
                {
                    sn.Add(c2);
                }

                snew.Clear();
                iter++;
            }

            return null;
        }
    }
}