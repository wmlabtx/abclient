using System;
using System.Text;
using ABClient.Helpers;

namespace ABClient.PostFilter
{
    internal static partial class Filter
    {
        private static byte[] ShopAjaxPhp(byte[] array)
        {
            var html = AppVars.Codepage.GetString(array);
            AppVars.BulkSellOldScript = string.Empty;
            AppVars.ShopList.Clear();
            const string patternStartShop = "</b></div></td></tr>";
            var pos = html.IndexOf(patternStartShop, StringComparison.Ordinal);
            if (pos == -1)
                return array;

            pos += patternStartShop.Length;
            var posStartShop = pos;
            while (true)
            {
                const string patternStartTr = "<tr><td bgcolor=#f9f9f9>";
                if (!html.Substring(pos).StartsWith(patternStartTr, StringComparison.Ordinal))
                    break;

                const string parrernEndTr = "</td></tr></table></td></tr></table></td></tr>";
                var posEnd = html.IndexOf(parrernEndTr, pos, StringComparison.Ordinal);
                if (posEnd == -1)
                    return array;

                posEnd += parrernEndTr.Length;
                var htmlEntry = html.Substring(pos, posEnd - pos);
                var shopEntry = new ShopEntry(htmlEntry);

                if (
                    string.IsNullOrEmpty(AppVars.BulkSellOldScript) &&
                    !string.IsNullOrEmpty(AppVars.BulkSellOldName) &&
                    !string.IsNullOrEmpty(shopEntry.Name) &&
                    shopEntry.Name.Equals(AppVars.BulkSellOldName, StringComparison.CurrentCultureIgnoreCase) &&
                    !string.IsNullOrEmpty(AppVars.BulkSellOldPrice) &&
                    !string.IsNullOrEmpty(shopEntry.Price) &&
                    shopEntry.Price.Equals(AppVars.BulkSellOldPrice, StringComparison.CurrentCultureIgnoreCase))
                {
                    AppVars.BulkSellOldScript = shopEntry.SellCall;
                }

                AppVars.ShopList.Add(shopEntry);
                pos = posEnd;
            }

            if (AppVars.ShopList.Count > 1)
            {
                for (var indexFirst = 0; indexFirst < (AppVars.ShopList.Count - 1); indexFirst++)
                {
                    for (var indexSecond = indexFirst + 1; indexSecond < AppVars.ShopList.Count; indexSecond++)
                    {
                        if (AppVars.ShopList[indexFirst].CompareTo(AppVars.ShopList[indexSecond]) != 0)
                            continue;

                        AppVars.ShopList[indexFirst].Inc();
                        AppVars.ShopList.RemoveAt(indexSecond);
                        indexSecond--;
                    }
                }
            }

            if (string.IsNullOrEmpty(AppVars.BulkSellOldScript))
            {
                AppVars.BulkSellOldName = string.Empty;
                AppVars.BulkSellOldPrice = string.Empty;
            }

            var sb = new StringBuilder();
            for (var index = 0; index < AppVars.ShopList.Count; index++)
                sb.Append(AppVars.ShopList[index]);

            var sbnew = new StringBuilder(html.Substring(0, posStartShop));
            sbnew.Append(sb);
            sbnew.Append(html.Substring(pos));
            html = sbnew.ToString();
            return Russian.Codepage.GetBytes(html);
        }
    }
}
