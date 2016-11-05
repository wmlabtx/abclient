namespace ABClient.Things
{
    using System.Collections.Generic;
    using System;
    using System.IO;
    using System.Text;
    using System.Windows.Forms;
    using System.Xml;

    internal static class ThingsDb
    {
        private static readonly string filedb = Path.Combine(Application.StartupPath, "abthings.xml");
        private static readonly List<Thing> list = new List<Thing>();
        private static string temp;

        internal static string StrReq, StrBon;

        internal static void Load()
        {
            if (!File.Exists(filedb))
            {
                return;
            }

            var settings = new XmlReaderSettings { IgnoreComments = true, IgnoreWhitespace = true, ConformanceLevel = ConformanceLevel.Auto };
            using (var reader = XmlReader.Create(filedb, settings))
            {
                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            ReadElement(reader);
                            break;

                        default:
                            break;
                    }
                }
            }
        }

        /*
        internal static void Add(Thing th)
        {
            list.Add(th);
        }
         */ 

        internal static bool FindPart(string img, string req)
        {
            if (img == null)
            {
                throw new ArgumentNullException("img");
            }

            if (req == null)
            {
                throw new ArgumentNullException("req");
            }

            var lt = Find(img);
            if (lt.Count == 0)
            {
                return false;
            }

            Thing th;
            string[] bonkeys = null;
            string[] bonvals = null;
            if (string.IsNullOrEmpty(req))
            {
                th = lt[lt.Count - 1];
            }
            else
            {
                var sp = req.Split(new[] { "<br>" }, StringSplitOptions.RemoveEmptyEntries);
                var sk = new List<string>();
                var sv = new List<string>();
                for (var i = 0; i < sp.Length; i++)
                {
                    var spp = sp[i].Split(new[] { ": " }, StringSplitOptions.None);
                    if (spp.Length != 2)
                    {
                        continue;
                    }

                    sk.Add(spp[0]);
                    sv.Add(spp[1].TrimEnd(new[] { '%' }));
                }

                bonkeys = sk.ToArray();
                bonvals = sv.ToArray();

                int t;
                for (t = 0; t < lt.Count; t++)
                {
                    if (IsEq(bonkeys, bonvals, lt[t]))
                    {
                        break;
                    }
                }

                if (t < lt.Count)
                {
                    th = lt[t];
                }
                else
                {
                    var maxindex = 0;
                    var maxcmp = -1;
                    for (t = 0; t < lt.Count; t++)
                    {
                        var cmp = CompareThing(bonkeys, bonvals, lt[t]);
                        if (cmp == -1)
                        {
                            continue;
                        }

                        if (cmp <= maxcmp)
                        {
                            continue;
                        }

                        maxcmp = cmp;
                        maxindex = t;
                    }

                    th = lt[maxindex];
                }
            }

            var sb = new StringBuilder();
            for (var i = 0; i < th.reqkeys.Length; i++)
            {
                sb.Append(th.reqkeys[i]);
                sb.Append(": <b>");
                sb.Append(th.reqvals[i]);
                sb.Append("</b><br>");
            }

            StrReq = sb.ToString();
            sb.Length = 0;
            if (!string.IsNullOrEmpty(th.Description))
            {
                sb.Append("<span class=description>");
                sb.Append(th.Description);
                sb.Append("</span><br>");
            }

            for (var i = 0; i < th.bonkeys.Length; i++)
            {
                var gray = string.Empty;
                if (!th.bonkeys[i].Equals("Удар", StringComparison.OrdinalIgnoreCase) &&
                    !th.bonkeys[i].Equals("Класс брони", StringComparison.OrdinalIgnoreCase) &&
                    !th.bonkeys[i].Equals("Пробой брони", StringComparison.OrdinalIgnoreCase) &&
                    !th.bonkeys[i].Equals("HP", StringComparison.OrdinalIgnoreCase) &&
                    !th.bonkeys[i].Equals("Мана", StringComparison.OrdinalIgnoreCase) &&
                    !th.bonkeys[i].Equals("Долговечность", StringComparison.OrdinalIgnoreCase))
                {
                    gray = "<font color=#999999>";
                }

                sb.Append(gray);
                sb.Append(th.bonkeys[i]);
                sb.Append(": <b>");
                if (bonkeys != null)
                {
                    int k;
                    for (k = 0; k < bonkeys.Length; k++)
                    {
                        if (!th.bonkeys[i].Equals(bonkeys[k], StringComparison.OrdinalIgnoreCase))
                        {
                            continue;
                        }

                        if (th.bonvals[i].Equals(bonvals[k], StringComparison.OrdinalIgnoreCase))
                        {
                            sb.Append(th.bonvals[i]);
                            sb.Append("</b>");
                            if (!string.IsNullOrEmpty(gray))
                            {
                                sb.Append("</font>");
                            }

                            sb.Append("<br>");
                        }
                        else
                        {
                            sb.Append(bonvals[k]);
                            sb.Append("</b> (было <span class=up>");
                            sb.Append(th.bonvals[i]);
                            sb.Append("</span>)<br>");
                        }

                        break;
                    }

                    if (k == bonkeys.Length)
                    {
                        sb.Append(th.bonvals[i]);
                        sb.Append("</b>");
                        if (!string.IsNullOrEmpty(gray))
                        {
                            sb.Append("</font>");
                        }

                        sb.Append("<br>");
                    }
                }
                else
                {
                    sb.Append(th.bonvals[i]);
                    sb.Append("</b>");
                    if (!string.IsNullOrEmpty(gray))
                    {
                        sb.Append("</font>");
                    }

                    sb.Append("<br>");
                }
            }

            StrBon = sb.ToString();
            return true;
        }

        private static bool IsEq(string[] keys, string[] vals, Thing thing)
        {
            for (var i = 0; i < keys.Length; i++)
            {
                var keyR = keys[i];
                var j = 0;
                while (j < thing.bonkeys.Length)
                {
                    var keyX = thing.bonkeys[j];
                    if (keyR.Equals(keyX, StringComparison.OrdinalIgnoreCase))
                    {
                        break;
                    }

                    j++;
                }

                if (j == thing.bonkeys.Length)
                {
                    return false;
                }

                var valR = vals[i];
                var valX = thing.bonvals[j];
                if (!valR.Equals(valX, StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }
            }

            return true;
        }

        private static int CompareThing(string[] keys, string[] vals, Thing thing)
        {
            var cmp = 0;
            for (var i = 0; i < keys.Length; i++)
            {
                var keyR = keys[i];
                var j = 0;
                while (j < thing.bonkeys.Length)
                {
                    var keyX = thing.bonkeys[j];
                    if (keyR.Equals(keyX, StringComparison.OrdinalIgnoreCase))
                    {
                        break;
                    }

                    j++;
                }

                if (j == thing.bonkeys.Length)
                {
                    return -1;
                }

                var valR = vals[i];
                var valX = thing.bonvals[j];
                if (valR.Equals(valX, StringComparison.OrdinalIgnoreCase))
                {
                    cmp++;
                }
            }

            return cmp;
        }

        private static bool EqName(Thing th)
        {
            return th.Img.Equals(temp, StringComparison.OrdinalIgnoreCase);
        }

        internal static List<Thing> Find(string img)
        {
            temp = img;
            return list.FindAll(EqName);
        }

        private static void ReadElement(XmlReader reader)
        {
            switch (reader.Name)
            {
                case "t":
                    var thing = new Thing
                                    {
                                        Img = (reader["i"] ?? string.Empty),
                                        Name = (reader["n"] ?? string.Empty),
                                        Description = (reader["d"] ?? string.Empty)
                                    };
                    var req = reader["r"] ?? string.Empty;
                    thing.SetReq(req);
                    var bon = reader["b"] ?? string.Empty;
                    thing.SetBon(bon);
                    if (!string.IsNullOrEmpty(thing.Name))
                    {
                        list.Add(thing);
                    }

                    break;
            }
        }
    }
}