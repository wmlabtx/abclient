using ABClient.ABForms;

namespace ABClient
{
    using System;
    using System.Collections.Generic;
    using MyHelpers;

    internal class ParsedDressed
    {
        internal bool Valid;
        internal string Wid;
        internal string Vcod;
        internal bool Empty1, Empty2;
        internal bool InRightSlot;
        internal string Hand1, Hand2;

        private readonly List<string> slist = new List<string>();
        private readonly List<string> dlist = new List<string>();

        internal ParsedDressed(string html)
        {
            Valid = false;

            AppVars.AutoFishHand1 = string.Empty;
            AppVars.AutoFishHand2 = string.Empty;
            AppVars.AutoFishHand1D = string.Empty;
            AppVars.AutoFishHand2D = string.Empty;

            var slotsinv = HelperStrings.SubString(html, "slots_inv(", ");");
            if (string.IsNullOrEmpty(slotsinv))
            {
                var slotspla = HelperStrings.SubString(html, "slots_pla(", ");");
                if (string.IsNullOrEmpty(slotspla))
                {
                    return;
                }

                var farg = slotspla.Split(',');
                if (farg.Length < 5)
                {
                    return;
                }

                var fmain = farg[2].Split('@');
                if (fmain.Length < 13)
                {
                    return;
                }

                var fdo = farg[3].Split('@');
                if (fdo.Length < 13)
                {
                    return;
                }

                var fhand1 = fmain[2].Split(':');
                if (fhand1.Length < 2)
                {
                    return;
                }

                Hand1 = fhand1[1];
                Empty1 = Hand1.StartsWith("Слот", StringComparison.OrdinalIgnoreCase);
                var fcurdlg1 = string.Empty;
                var fmaxdlg1 = string.Empty;
                if (!Empty1)
                {
                    fcurdlg1 = fdo[2];
                    fmaxdlg1 = fhand1[2].Split(new[] { '|' }, StringSplitOptions.None)[7];
                }

                var fhand2 = fmain[12].Split(':');
                if (fhand2.Length < 2)
                {
                    return;
                }

                Hand2 = fhand2[1];
                Empty2 = Hand2.StartsWith("Слот", StringComparison.OrdinalIgnoreCase);
                var fcurdlg2 = string.Empty;
                var fmaxdlg2 = string.Empty;
                if (!Empty2)
                {
                    fcurdlg2 = fdo[12];
                    fmaxdlg2 = fhand2[2].Split(new[] { '|' }, StringSplitOptions.None)[7];
                }

                if (!Empty1)
                {
                    slist.Add(Hand1);
                    dlist.Add(fcurdlg1 + "/" + fmaxdlg1);
                }

                if (!Empty2)
                {
                    slist.Add(Hand2);
                    dlist.Add(fcurdlg2 + "/" + fmaxdlg2);
                }

                Valid = true;
                return;
            }

            var pslots = slotsinv.Split(',');
            if (pslots.Length < 6)
            {
                return;
            }

            var slmain = pslots[2].Split('@');
            if (slmain.Length < 13)
            {
                return;
            }

            var slwid = pslots[3].Split('@');
            if (slwid.Length < 3)
            {
                return;
            }

            Wid = slwid[2];

            var slvcod = pslots[4].Split('@');
            if (slvcod.Length < 3)
            {
                return;
            }

            Vcod = slvcod[2];

            var sldlg = pslots[5].Split(new[] { '@' }, StringSplitOptions.None);
            if (sldlg.Length < 13)
            {
                return;
            }

            var slhand1 = slmain[2].Split(':');
            if (slhand1.Length < 2)
            {
                return;
            }

            Hand1 = slhand1[1];
            Empty1 = Hand1.StartsWith("Слот", StringComparison.OrdinalIgnoreCase);
            var curdlg1 = string.Empty;
            var maxdlg1 = string.Empty;
            if (!Empty1)
            {
                curdlg1 = sldlg[2];
                maxdlg1 = slhand1[2].Split(new[] { '|' }, StringSplitOptions.None)[7];
            }

            var slhand2 = slmain[12].Split(':');
            if (slhand2.Length < 2)
            {
                return;
            }

            Hand2 = slhand2[1];
            Empty2 = Hand2.StartsWith("Слот", StringComparison.OrdinalIgnoreCase);
            var curdlg2 = string.Empty;
            var maxdlg2 = string.Empty;
            if (!Empty2)
            {
                curdlg2 = sldlg[12];
                maxdlg2 = slhand2[2].Split(new[] { '|' }, StringSplitOptions.None)[7];
            }

            if (!Empty1)
            {
                slist.Add(Hand1);
                dlist.Add(curdlg1 + "/" + maxdlg1);
            }

            if (!Empty2)
            {
                slist.Add(Hand2);
                dlist.Add(curdlg2 + "/" + maxdlg2);
            }

            Valid = true;
        }

        internal bool IsWear1()
        {
            var iswear1 = false;
            InRightSlot = false;
            if (!AppVars.Profile.FishAutoWear ||
                AppVars.Profile.FishHandOne.Equals("нет", StringComparison.OrdinalIgnoreCase))
            {
                iswear1 = true;
            }
            else
            {
                if (AppVars.Profile.FishHandOne.Equals("Любая удочка", StringComparison.OrdinalIgnoreCase))
                {
                    if ((slist.Count > 0) && (slist[0].IndexOf("удочка", StringComparison.CurrentCultureIgnoreCase) != -1 || slist[0].IndexOf("спиннинг", StringComparison.CurrentCultureIgnoreCase) != -1))
                    {
                        iswear1 = true;
                        AppVars.AutoFishHand1 = slist[0];
                        AppVars.AutoFishHand1D = dlist[0];
                        slist.RemoveAt(0);
                        dlist.RemoveAt(0);
                    }
                    else
                    {
                        if ((slist.Count > 1) && (slist[1].IndexOf("удочка", StringComparison.CurrentCultureIgnoreCase) != -1 || slist[0].IndexOf("спиннинг", StringComparison.CurrentCultureIgnoreCase) != -1))
                        {
                            iswear1 = true;
                            InRightSlot = true;
                            AppVars.AutoFishHand1 = slist[1];
                            AppVars.AutoFishHand1D = dlist[1];
                            slist.RemoveAt(1);
                            dlist.RemoveAt(1);
                        }
                    }
                }
                else
                {
                    if ((slist.Count > 0) && (slist[0].IndexOf(AppVars.Profile.FishHandOne, StringComparison.CurrentCultureIgnoreCase) >= 0))
                    {
                        iswear1 = true;
                        AppVars.AutoFishHand1 = slist[0];
                        AppVars.AutoFishHand1D = dlist[0];
                        slist.RemoveAt(0);
                        dlist.RemoveAt(0);
                    }
                    else
                    {
                        if ((slist.Count > 1) && (slist[1].IndexOf(AppVars.Profile.FishHandOne, StringComparison.CurrentCultureIgnoreCase) >= 0))
                        {
                            iswear1 = true;
                            InRightSlot = true;
                            AppVars.AutoFishHand1 = slist[1];
                            AppVars.AutoFishHand1D = dlist[1];
                            slist.RemoveAt(1);
                            dlist.RemoveAt(1);
                        }
                    }
                }
            }

            return iswear1;
        }

        internal bool IsWear2()
        {
            var iswear2 = false;
            if (!AppVars.Profile.FishAutoWear ||
                AppVars.Profile.FishHandTwo.Equals("нет", StringComparison.OrdinalIgnoreCase))
            {
                iswear2 = true;
            }
            else
            {
                if (AppVars.Profile.FishHandTwo.Equals("Любая удочка", StringComparison.OrdinalIgnoreCase))
                {
                    if ((slist.Count > 0) && (slist[0].IndexOf("удочка", StringComparison.CurrentCultureIgnoreCase) != -1 || slist[0].IndexOf("спиннинг", StringComparison.CurrentCultureIgnoreCase) != -1))
                    {
                        AppVars.AutoFishHand2 = slist[0];
                        AppVars.AutoFishHand2D = dlist[0];
                        iswear2 = true;
                    }
                    else
                    {
                        if ((slist.Count > 1) && (slist[1].IndexOf("удочка", StringComparison.CurrentCultureIgnoreCase) != -1 || slist[0].IndexOf("спиннинг", StringComparison.CurrentCultureIgnoreCase) != -1))
                        {
                            AppVars.AutoFishHand2 = slist[1];
                            AppVars.AutoFishHand2D = dlist[1];
                            iswear2 = true;
                        }
                    }
                }
                else
                {
                    if ((slist.Count > 0) && (slist[0].IndexOf(AppVars.Profile.FishHandTwo, StringComparison.CurrentCultureIgnoreCase) >= 0))
                    {
                        AppVars.AutoFishHand2 = slist[0];
                        AppVars.AutoFishHand2D = dlist[0];
                        iswear2 = true;
                    }
                    else
                    {
                        if ((slist.Count > 1) && (slist[1].IndexOf(AppVars.Profile.FishHandTwo, StringComparison.CurrentCultureIgnoreCase) >= 0))
                        {
                            AppVars.AutoFishHand2 = slist[1];
                            AppVars.AutoFishHand2D = dlist[1];
                            iswear2 = true;
                        }
                    }
                }
            }

            return iswear2;
        }

        internal bool IsWearKnife()
        {
            var list = new[] {"Малый Разделочный Нож", "Охотничий Нож", "Вороненый Охотничий Нож", "Разделочный Топорик", "Арисайский Охотничий Нож" };

            for (var i = 0; i < slist.Count; i++)
            {
                for (var j = 0; j < list.Length; j++)
                {
                    if (slist[i].IndexOf(list[j], StringComparison.CurrentCultureIgnoreCase) >= 0)
                    {
                        if (AppVars.Profile.SkinAuto &&
                            (AppVars.AutoSkinHand != slist[i] || AppVars.AutoSkinHandD != dlist[i]))
                        {
                            try
                            {
                                if (AppVars.MainForm != null)
                                {
                                    AppVars.MainForm.BeginInvoke(
                                        new UpdateWriteChatMsgDelegate(AppVars.MainForm.WriteChatMsg), $"Разделочный нож: <span style=\"color:#009933;font-weight:bold;\">«{slist[i]} {dlist[i]}»<span>");
                                }
                            }
                            catch (InvalidOperationException)
                            {
                            }
                        }

                        AppVars.AutoSkinHand = slist[i];
                        AppVars.AutoSkinHandD = dlist[i];
                        return true;
                    }
                }
            }

            return false;
        }
    }
}