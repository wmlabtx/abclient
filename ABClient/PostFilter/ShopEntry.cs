using System;
using ABClient.MyHelpers;

namespace ABClient.PostFilter
{
    public class ShopEntry : IComparable
    {
        public string Name;
        public string Price;
        public string SellCall;

        private readonly string _raw;
        private int _count;

        public ShopEntry(string html)
        {
            _raw = html;
            _count = 1;
            Name = HelperStrings.SubString(html, "<font class=nickname><b>", "</b>");
            Price = HelperStrings.SubString(html, "value=\"Продать за ", " NV\">");
            SellCall = HelperStrings.SubString(html, " onclick=\"shop_item_sell(", ")\" ");
        }

        public int CompareTo(object obj)
        {
            if (!(obj is ShopEntry))
                return 1;

            var other = (ShopEntry)obj;
            if (string.IsNullOrEmpty(Price) || string.IsNullOrEmpty(other.Price))
                return 1;

            var result = string.Compare(Name, other.Name, StringComparison.Ordinal);
            return result != 0 ? result : string.Compare(Price, other.Price, StringComparison.CurrentCultureIgnoreCase);
        }

        public override string ToString()
        {
            if (_count == 1 || string.IsNullOrEmpty(Price))
                return _raw;

            var pos = _raw.IndexOf("<font class=nickname><b>", StringComparison.CurrentCultureIgnoreCase);
            if (pos != -1)
            {
                pos = _raw.IndexOf("</b>", pos, StringComparison.CurrentCultureIgnoreCase);
                if (pos != -1)
                {
                    var html = _raw.Insert(pos, $" ({_count} шт.)");
                    var pssStart = html.IndexOf("<input type=button class=invbut onclick=\"shop_item_sell", StringComparison.CurrentCultureIgnoreCase);
                    if (pssStart != -1)
                    {
                        var pssEnd = html.IndexOf('>', pssStart);
                        if (pssEnd != -1)
                        {
                            var pss = $"&nbsp;<input type=button class=invbut onclick=\"javascript: window.external.StartBulkOldSell('{Name}', '{Price}'); shop_item_sell({SellCall}); \" value=\"Продать все\">";
                            html = html.Insert(pssEnd + 1, pss);                            
                        }
                    }

                    return html;
                }
            }

            return _raw;
        }

        internal void Inc()
        {
            _count++;
        }
    }
}
