namespace ABClient
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Globalization;

    internal static class TorgList
    {
        private static TorgPair[] Table;

        internal static bool Trigger { get; set; }

        internal static bool TriggerBuy { get; set; }

        internal static string MessageThanks { get; set; }

        internal static string MessageNoMoney { get; set; }

        internal static string UidThing { get; set; }

        internal static bool Parse(string torgString)
        {
            if (string.IsNullOrEmpty(torgString))
            {
                return false;
            }

            var newTorgList = new List<TorgPair>();

            var work = torgString.Replace(Environment.NewLine, string.Empty).Replace(" ", string.Empty).Replace("(0)", "(*0)").Replace("(-", "(*");
            var sp = work.Split(new[] { '-', '(', ')', '*', ',' });
            var i = 0;
            while (i < sp.Length)
            {
                int lowValue;
                if (!int.TryParse(sp[i], out lowValue))
                {
                    return false;
                }

                if (++i >= sp.Length)
                {
                    return false;
                }

                int highValue;
                if (!int.TryParse(sp[i], out highValue))
                {
                    return false;
                }

                if (lowValue > highValue)
                {
                    return false;
                }

                if (++i >= sp.Length)
                {
                    return false;
                }

                if (!string.IsNullOrEmpty(sp[i]))
                {
                    return false;
                }

                if (++i >= sp.Length)
                {
                    return false;
                }

                int price;
                if (!int.TryParse(sp[i], out price))
                {
                    return false;
                }

                if (price < 0)
                {
                    return false;
                }

                var torgPair = new TorgPair(lowValue, highValue, -price);
                newTorgList.Add(torgPair);

                if (++i >= sp.Length)
                {
                    break;
                }

                ++i;
            }

            if (newTorgList.Count == 0)
            {
                return false;
            }

            Table = newTorgList.ToArray();
            return true;
        }

        internal static int Calculate(int price)
        {
            for (var i = 0; i < Table.Length; i++)
            {
                if (price >= Table[i].PriceLow && price <= Table[i].PriceHi)
                {
                    return price + Table[i].Bonus;
                }
            }

            var bonus = 0;
            var diffmin = int.MaxValue;

            for (var i = 0; i < Table.Length; i++)
            {
                if (price < Table[i].PriceLow)
                {
                    continue;
                }

                var diff = price - Table[i].PriceLow;
                if (diff >= diffmin)
                {
                    continue;
                }

                diffmin = diff;
                bonus = price + Table[i].Bonus;
            }

            return bonus;
        }

        internal static string DoFilter(string message, string thing, string thingLevel, int price, int tableprice, int thingRealDolg, int thingFullDolg, int price90)
        {
            var sb = new StringBuilder(message);
            sb.Replace("{таблица}", AppVars.Profile.TorgTabl);
            sb.Replace("{вещь}", thing);
            sb.Replace("{вещьур}", thingLevel);
            sb.Replace("{вещьдолг}", thingRealDolg.ToString(CultureInfo.InvariantCulture) + '/' + thingFullDolg.ToString(CultureInfo.InvariantCulture));
            sb.Replace("{цена}", price.ToString(CultureInfo.InvariantCulture));
            sb.Replace("{минцена}", tableprice.ToString(CultureInfo.InvariantCulture));
            sb.Replace("{цена90}", price90.ToString(CultureInfo.InvariantCulture));
            return sb.ToString();
        }
    }
}
