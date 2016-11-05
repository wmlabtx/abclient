namespace ABClient.PostFilter
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using ABForms;

    internal static partial class Filter
    {
        private static readonly List<InvEntry> InvList = new List<InvEntry>();

        private static string MainPhpInv(string html)
        {
            const string patternStartInv = "</b></font></td></tr>";
            var pos = html.IndexOf(patternStartInv, StringComparison.Ordinal);
            if (pos == -1)
            {
                return html;
            }

            pos += patternStartInv.Length;
            var posStartInv = pos;
            InvList.Clear();
            while (true)
            {
                const string parrernStartTr = "<tr><td bgcolor=#F5F5F5>";
                if (!html.Substring(pos, parrernStartTr.Length).StartsWith(parrernStartTr, StringComparison.Ordinal))
                {
                    break;
                }

                const string parrernEndTr = "<td bgcolor=#FCFAF3><img src=http://image.neverlands.ru/1x1.gif width=5 height=1></td></tr></table></td></tr></table></td></tr>";
                var posEnd = html.IndexOf(parrernEndTr, pos, StringComparison.Ordinal);
                if (posEnd == -1)
                {
                    const string parrernEndTrShort = "<img src=http://image.neverlands.ru/1x1.gif width=1 height=5></td></tr></table></td></tr>";
                    posEnd = html.IndexOf(parrernEndTrShort, pos, StringComparison.Ordinal);
                    if (posEnd == -1)
                    {
                        return html;
                    }

                    posEnd += parrernEndTrShort.Length;
                }
                else
                {
                    posEnd += parrernEndTr.Length;    
                }
                
                var htmlEntry = html.Substring(pos, posEnd - pos);
                var invEntry = new InvEntry(htmlEntry);

                if (
                    invEntry.IsExpired() ||
                    (!string.IsNullOrEmpty(AppVars.BulkDropThing) &&
                    !string.IsNullOrEmpty(AppVars.BulkDropPrice) &&
                    AppVars.BulkDropThing.Equals(invEntry.DropThing, StringComparison.CurrentCultureIgnoreCase) &&
                    AppVars.BulkDropPrice.Equals(invEntry.DropPrice, StringComparison.CurrentCultureIgnoreCase)))
                {
                    html = BuildRedirect($"Выбрасывание предмета <b>&laquo;{invEntry.DropThing}&raquo;</b>...", invEntry.DropLink);
                    return html;
                }

                if (
                    !string.IsNullOrEmpty(invEntry.PssLink) &&
                    !string.IsNullOrEmpty(AppVars.BulkSellThing) &&
                    !string.IsNullOrEmpty(invEntry.PssThing) &&
                    (AppVars.BulkSellThing.Equals(invEntry.PssThing)) &&
                    (AppVars.BulkSellPrice == invEntry.PssPrice)
                   )
                {
                    AppVars.BulkSellSum += AppVars.BulkSellPrice;
                    var messageSell =
                        $"Продажа предмета <b>&laquo;{invEntry.PssThing}&raquo;</b>. Выручка {AppVars.BulkSellSum} NV...";
                    html = BuildRedirect(messageSell, invEntry.PssLink);
                    return html;                    
                }

                InvList.Add(invEntry);
                pos = posEnd;
            }

            if (!string.IsNullOrEmpty(AppVars.BulkDropThing))
            {
                var messageBulkDropFinished = $"Выбрасывание пачки <b>&laquo;{AppVars.BulkDropThing}&raquo;</b> завершено.";
                try
                {
                    if (AppVars.MainForm != null)
                    {
                        AppVars.MainForm.BeginInvoke(
                            new UpdateChatDelegate(AppVars.MainForm.UpdateChat), messageBulkDropFinished);
                    }
                }
                catch (InvalidOperationException)
                {
                }

                AppVars.BulkDropThing = string.Empty;
            }

            if (
                !string.IsNullOrEmpty(AppVars.BulkSellThing)
                )
            {
                var messageBulkSellFinished =
                    $"Продажа пачки <b>&laquo;{AppVars.BulkSellThing}&raquo;</b> завершена. Выручка составила <b>{AppVars.BulkSellSum}</b> NV.";
                try
                {
                    if (AppVars.MainForm != null)
                    {
                        AppVars.MainForm.BeginInvoke(
                            new UpdateChatDelegate(AppVars.MainForm.UpdateChat), messageBulkSellFinished);
                    }
                }
                catch (InvalidOperationException)
                {
                }

                AppVars.BulkSellThing = string.Empty;
            }

            if (InvList.Count > 1)
            {
                // Удаляем дубли
                if (AppVars.Profile.DoInvPack)
                {
                    for (var indexFirst = 0; indexFirst < (InvList.Count - 1); indexFirst++)
                    {
                        for (var indexSecond = indexFirst + 1; indexSecond < InvList.Count; indexSecond++)
                        {
                            if (InvList[indexFirst].CompareTo(InvList[indexSecond]) != 0)
                            {
                                continue;
                            }

                            if (InvList[indexFirst].CompareDolg(InvList[indexSecond]) > 0)
                            {
                                InvList[indexFirst] = (InvEntry) InvList[indexSecond].Clone();
                            }

                            InvList[indexFirst].Inc();
                            InvList.RemoveAt(indexSecond);
                            indexSecond--;
                        }
                    }
                }
            }

            // Вставка кнопки массовой продажи
            for (var index = 0; index < InvList.Count; index++)
            {
                InvList[index].AddBulkSell();
            }

            // Вставка кнопки массового выброса
            for (var index = 0; index < InvList.Count; index++)
            {
                InvList[index].AddBulkDelete();
            }

            // Сортировка
            if (AppVars.Profile.DoInvSort)
            {
                var invComparer = new InvComparer();
                InvList.Sort(invComparer);
            }

            var sb = new StringBuilder();
            for (var index = 0; index < InvList.Count; index++)
            {
                sb.Append(InvList[index].Build());
            }

            var sbnew = new StringBuilder(html.Substring(0, posStartInv));
            sbnew.Append(sb);
            sbnew.Append(html.Substring(pos));
            return sbnew.ToString();
        }
    }
}