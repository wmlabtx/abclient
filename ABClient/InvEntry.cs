namespace ABClient
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;
    using MyHelpers;

    internal sealed class InvEntry : IComparable, ICloneable
    {
        internal string Name { get; private set; }
        internal string WearLink { get; private set; }

        internal string DropThing { get; private set; }
        internal string DropLink { get; private set; }
        internal string DropPrice { get; private set; }

        internal string PssThing { get; private set; }
        internal string PssLink { get; private set; }
        internal int PssPrice { get; private set; }

        private readonly string _img;
        private readonly int _level;
        
        private readonly string _dolg;
        private readonly string _properties = string.Empty;
        private readonly int _dolgOne;
        private readonly int _dolgTwo;
        private readonly int _countButton;
        private readonly bool _expired;
        
        private readonly bool _expirible;
        private string _raw;
        private int _count;

        internal InvEntry(string html)
        {
            // <input type=button class=invbut onclick="location='main.php?get_id=57&uid=85896535&s=1&vcode=33a9b5f8916ae068f30d9efcb458934e'" value="Надеть">
            WearLink = HelperStrings.SubString(html, "<input type=button class=invbut onclick=\"location='", "'\" value=\"Надеть\">") ?? string.Empty;


            // <input type=button class=invbut onclick="javascript: if(confirm('Вы точно хотите продать < Кристальное Кольцо (ап) > за 300 NV?')) { location='main.php?get_id=8&uid=16964485&wpr=500&wmas=2&sum=300&vcode=61a2a21c37f65190952e0eda5c426a2c&wcs=70&wms=70&wn=%CA%F0%E8%F1%F2%E0%EB%FC%ED%EE%E5+%CA%EE%EB%FC%F6%EE+%28%E0%EF%29' }" value="Продать за 300 NV">

            var possl = html.IndexOf("Продать за", StringComparison.CurrentCultureIgnoreCase);
            if (possl != -1)
            {
                var pssStart = html.LastIndexOf("<input", possl, StringComparison.CurrentCultureIgnoreCase);
                if (pssStart != -1)
                {
                    var pssEnd = html.IndexOf('>', possl);
                    if (pssEnd != -1)
                    {
                        var pssSubHtml = html.Substring(pssStart, pssEnd - pssStart + 1);
                        var pssThing = HelperStrings.SubString(pssSubHtml, "продать < ", " >");
                        if (pssThing != null)
                        {
                            var pssPriceString = HelperStrings.SubString(pssSubHtml, "> за ", " NV");
                            if (pssPriceString != null)
                            {
                                var pssLink = HelperStrings.SubString(pssSubHtml, "location='", "' ");
                                if (pssLink != null)
                                {
                                    int pssPrice;
                                    if (int.TryParse(pssPriceString, out pssPrice))
                                    {
                                        PssThing = pssThing;
                                        PssPrice = pssPrice;
                                        PssLink = pssLink;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            var dropThing = HelperStrings.SubString(html, "if(top.DeleteTrue('", "'))");
            if (!string.IsNullOrEmpty(dropThing))
            {
                var str = $"if(top.DeleteTrue('{dropThing}')) {{ location='";
                var dropLink = HelperStrings.SubString(html, str, "'");
                if (!string.IsNullOrEmpty(dropLink))
                {
                    var dropPrice = HelperStrings.SubString(html, "Цена: <b>", " NV</b>");
                    if (!string.IsNullOrEmpty(dropLink))
                    {
                        DropThing = dropThing;
                        DropLink = dropLink;
                        DropPrice = dropPrice;
                    }
                }
            }

            _raw = html;
            _count = 1;
            Name = HelperStrings.SubString(html, "<font class=nickname><b> ", "</b>") ?? string.Empty;

            // 25.06.2010 19:02
            var sg = HelperStrings.SubString(html, "<font color=#cc0000>Срок годности: ", "</font>");
            if (!string.IsNullOrEmpty(sg))
            {
                _expirible = true;
                var sp = sg.Split((new[] { '.', ' ', ':' }));
                if (sp.Length > 4)
                {
                    int day;
                    if (int.TryParse(sp[0], out day))
                    {
                        int month;
                        if (int.TryParse(sp[1], out month))
                        {
                            int year;
                            if (int.TryParse(sp[2], out year))
                            {
                                int hour;
                                if (int.TryParse(sp[3], out hour))
                                {
                                    int minute;
                                    if (int.TryParse(sp[4], out minute))
                                    {
                                        var exptime = new DateTime(year, month, day, hour, minute, 0);
                                        if (AppVars.ServerDateTime > exptime.AddDays(1))
                                        {
                                            _expired = true;
                                            /*
                                            <tr><td bgcolor=#F5F5F5><div align=center><input type=image src=http://image.neverlands.ru/weapon/Bi_w27_52.gif border=0 width=40 height=40 onclick="location='main.php?get_id=57&uid=39873192&s=1&vcode=f8594529b3435d142a75407c540bca30'"><br><img src=http://image.neverlands.ru/1x1.gif width=62 height=1><br><img src=http://image.neverlands.ru/solidst.gif width=62 height=2 border=0><img src=http://image.neverlands.ru/nosolidst.gif width=0 height=2 border=0></div></td><td width=100% bgcolor=#FFFFFF valign=top><table cellpadding=0 cellspacing=0 border=0 width=100%><tr><td bgcolor=#FFFFFF width=100%><input type=button class=invbut onclick="location='main.php?get_id=57&uid=39873192&s=1&vcode=f8594529b3435d142a75407c540bca30'" value="Надеть"> <input type=button class=invbut onclick="magicreform('39873192','Черный','Зелье Сильной Спины','af1ef85a503d2628f62581410d2c050f')" value="Использовать"> <input type=button class=invbut onclick="transferform('39873192','2','Зелье Сильной Спины','5e8ef288f10315cdb07898e6040ac88a','50','1','7','7')" value="Передать"> <input type=button class=invbut onclick="presentform('39873192','Зелье Сильной Спины','c995cd0ce9cc98c03a30ba71de878eff','1','50','7','7')" value="Подарить"> <input type=button class=invbut onclick="sellingform('39873192','Зелье Сильной Спины','079d292ef558c5f9c4f8dde534c9cbb9','50','1','0')" value="Продать"><br><img src=http://image.neverlands.ru/1x1.gif width=1 height=5></td><td valign=center> <input type=image src=http://image.neverlands.ru/del.gif width=14 height=14 border=0 onclick="javascript: if(top.DeleteTrue('Зелье Сильной Спины')) { location='main.php?get_id=50&uid=39873192&wpr=50&wmas=1&wcs=7&wms=7&vcode=a32f77183c3eb0dd3539d76838611768&wn=%C7%E5%EB%FC%E5+%D1%E8%EB%FC%ED%EE%E9+%D1%EF%E8%ED%FB' }" value="x" ><br><img src=http://image.neverlands.ru/1x1.gif width=1 height=5></td></tr><tr><td colspan=2 width=100%><table cellpadding=0 cellspacing=0 border=0 width=100%><tr><td bgcolor=#D8CDAF width=50% colspan=3><div align=center><font class=invtitle><font color=#000000>свойства</font></div></td><td bgcolor=#B9A05C><img src=http://image.neverlands.ru/1x1.gif width=1 height=16></td><td bgcolor=#D8CDAF width=50% colspan=3><div align=center><font class=invtitle><font color=#000000>требования</font></div></td></tr><tr><td bgcolor=#FCFAF3><img src=http://image.neverlands.ru/1x1.gif width=5 height=1></td><td bgcolor=#FCFAF3 width=50%><font class=nickname><b> Зелье Сильной Спины</b><br><font class=weaponch><b><font color=#cc0000>Срок годности: 25.06.2010 19:02</font></b><br>Цена: <b>50 NV</b><br>Долговечность: <b>7/7</b><br></td><td bgcolor=#FCFAF3><img src=http://image.neverlands.ru/1x1.gif width=5 height=1></td><td bgcolor=#B9A05C><img src=http://image.neverlands.ru/1x1.gif width=1 height=1></td><td bgcolor=#FCFAF3><img src=http://image.neverlands.ru/1x1.gif width=5 height=1></td><td bgcolor=#FCFAF3 width=50%><font class=weaponch>Масса: <b>1</b><br>Уровень: <b>8</b><br></td><td bgcolor=#FCFAF3><img src=http://image.neverlands.ru/1x1.gif width=5 height=1></td></tr></table></td></tr></table></td></tr>
                                            */
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            var prefix = string.Format(
                CultureInfo.InvariantCulture,
                "<font color=#000000>требования</font></div></td></tr><tr><td bgcolor=#FCFAF3><img src=http://image.neverlands.ru/1x1.gif width=5 height=1></td><td bgcolor=#FCFAF3 width=50%><font class=nickname><b> {0}</b><br><font class=weaponch>",
                Name);
            var prop = HelperStrings.SubString(
                html,
                prefix,
                "<br></td><td bgcolor=#FCFAF3><img src=http://image.neverlands.ru/1x1.gif width=5 height=1></td></tr></table>");
            if (!string.IsNullOrEmpty(prop))
            {
                var par =
                    prop.Split(
                        new[]
                            {
                                "<br>",
                                "</td><td bgcolor=#FCFAF3><img src=http://image.neverlands.ru/1x1.gif width=5 height=1></td><td bgcolor=#B9A05C><img src=http://image.neverlands.ru/1x1.gif width=1 height=1></td><td bgcolor=#FCFAF3><img src=http://image.neverlands.ru/1x1.gif width=5 height=1></td><td bgcolor=#FCFAF3 width=50%><font class=weaponch>"
                            },
                        StringSplitOptions.RemoveEmptyEntries);
                if (par.Length > 1)
                {
                    var sb = new StringBuilder();
                    for (var index = 1; index < par.Length; index++)
                    {
                        if (par[index].IndexOf("Цена: <b>", StringComparison.CurrentCultureIgnoreCase) != -1)
                        {
                            continue;
                        }

                        if (par[index].IndexOf("Материал: <b>", StringComparison.CurrentCultureIgnoreCase) != -1)
                        {
                            continue;
                        }

                        if (AppVars.Profile.DoInvPackDolg && string.IsNullOrEmpty(_dolg))
                        {
                            _dolg = HelperStrings.SubString(par[index], "Долговечность: <b>", "</b>") ?? string.Empty;
                            if (!string.IsNullOrEmpty(_dolg))
                            {
                                var parD = _dolg.Split('/');
                                if (parD.Length == 2)
                                {
                                    if (!int.TryParse(parD[0], out _dolgOne))
                                    {
                                        _dolgOne = 0;
                                    }

                                    if (!int.TryParse(parD[1], out _dolgTwo))
                                    {
                                        _dolgTwo = 0;
                                    }
                                }
                            }
                            else
                            {
                                if (sb.Length > 0)
                                {
                                    sb.Append('|');
                                }

                                sb.Append(par[index]);
                            }
                        }
                        else
                        {
                            if (sb.Length > 0)
                            {
                                sb.Append('|');
                            }

                            sb.Append(par[index]);
                        }
                    }

                    _properties = sb.ToString();
                }
            }

            _img = HelperStrings.SubString(html, " src=http://", " ") ?? string.Empty;
            var level = HelperStrings.SubString(html, "<br>Уровень: <b>", "</b>") ?? string.Empty;
            if (!string.IsNullOrEmpty(level))
            {
                if (!int.TryParse(level, out _level))
                {
                    _level = 0;
                }
            }

            var pos = 0;
            while (pos != -1)
            {
                pos = html.IndexOf("<input type=button", pos, StringComparison.CurrentCultureIgnoreCase);
                if (pos != -1)
                {
                    _countButton++;
                    pos += "<input type=button".Length;
                }
            }
        }

        public int CompareTo(object obj)
        {
            if (!(obj is InvEntry))
            {
                return 1;
            }

            var other = (InvEntry)obj;
            var result = string.Compare(Name, other.Name, StringComparison.Ordinal);
            if (result != 0)
            {
                return result;
            }

            result = string.Compare(_img, other._img, StringComparison.Ordinal);
            if (result != 0)
            {
                return result;
            }

            if ((!_expirible && other._expirible) || (_expirible && !other._expirible))
            {
                return _expirible.CompareTo(other._expirible);
            }

            if (_expirible && other._expirible)
            {
                if ((!_expired && other._expired) || (_expired && !other._expired))
                {
                    return _expired.CompareTo(other._expired);
                }

                // return _exptime.CompareTo(other._exptime);
            }

            result = _level.CompareTo(other._level);
            if (result != 0)
            {
                return result;
            }

            result = _countButton.CompareTo(other._countButton);
            if (result != 0)
            {
                return result;
            }

            result = string.Compare(_properties, other._properties, StringComparison.Ordinal);
            if (result != 0)
            {
                return result;
            }

            /*
            if (_name.IndexOf("(ап)", StringComparison.OrdinalIgnoreCase) != -1)
            {
                return 1;
            }
             */

            /*
            if (!AppVars.Profile.DoInvPackDolg)
            {
                result = _dolg.CompareTo(other._dolg);
                if (result != 0)
                {
                    return result;
                }
            }
             */ 

            return 0;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }

        internal void Inc()
        {
            _count++;
        }

        internal bool IsExpired()
        {
            return _expirible && _expired;
        }

        internal string Build()
        {
            var work = _raw;
            if (IsExpired())
            {
                work = work.Replace("bgcolor=#F5F5F5", "bgcolor=#F5E5E5");
                work = work.Replace("bgcolor=#FFFFFF", "bgcolor=#F5E5E5");
                work = work.Replace("bgcolor=#FCFAF3", "bgcolor=#F5E5E5");
                work = work.Replace("bgcolor=#D8CDAF", "bgcolor=#F5D5D5");
                int ps = work.IndexOf("<font class=nickname><b> ", StringComparison.Ordinal);
                if (ps != -1)
                {
                    work = work.Insert(ps, "<font class=nickname><font color=#cc0000><b>ПРОСРОЧЕНО!</b></font></font> ");
                }
            }

            if (_count == 1)
            {
                return work;
            }

            int pos = work.IndexOf("<font class=nickname><b> ", StringComparison.CurrentCultureIgnoreCase);
            if (pos != -1)
            {
                pos = work.IndexOf("</b>", pos, StringComparison.CurrentCultureIgnoreCase);
                if (pos != -1)
                {
                    string count = string.Format(CultureInfo.InvariantCulture, " ({0} шт.)", _count);
                    return work.Insert(pos, count);
                }
            }

            return work;
        }

        internal void AddBulkSell()
        {
            if (_count == 1)
            {
                return;
            }

            if (string.IsNullOrEmpty(PssThing))
            {
                return;
            }

            var possl = _raw.IndexOf("Продать за", StringComparison.CurrentCultureIgnoreCase);
            if (possl != -1)
            {
                var pssStart = _raw.LastIndexOf("<input", possl, StringComparison.CurrentCultureIgnoreCase);
                if (pssStart != -1)
                {
                    var pssEnd = _raw.IndexOf('>', possl);
                    if (pssEnd != -1)
                    {
                        var pss = string.Format(
                                @" <input type=button class=invbut onclick=""javascript: if(confirm('Вы точно хотите продать все предметы < {0} > по {1} NV?')) {{ window.external.StartBulkSell('{0}', '{1}', '{2}'); }}"" value=""Продать пачку за {3} NV"">",
                                PssThing,
                                PssPrice,
                                PssLink,
                                PssPrice * _count);
                        _raw = _raw.Insert(pssEnd + 1, pss);
                    }
                }
            }
        }

        internal void AddBulkDelete()
        {
            if (_count == 1)
                return;

            if (string.IsNullOrEmpty(DropThing) || string.IsNullOrEmpty(DropPrice))
                return;

            const string patx = "<input type=image src=http://image.neverlands.ru/del.gif width=14 height=14 border=0 onclick=\"javascript: if(top.DeleteTrue('"; 
            var possl = _raw.IndexOf(patx, StringComparison.CurrentCultureIgnoreCase);
            if (possl != -1)
            {
                var dropButton =
                    $" <input type=image src=http://image.neverlands.ru/del.gif width=14 height=14 border=0 title=\"Выбросить всю пачку\" onclick=\"javascript: if(top.DeleteTrue('Пачку')) {{ window.external.StartBulkDrop('{DropThing}', '{DropPrice}'); }}\" value=\"X\">&nbsp;";
                _raw = _raw.Insert(possl, dropButton);
            }
        }

        internal int CompareDolg(InvEntry other)
        {
            if (other == null)
            {
                return 0;
            }

            var isFull = _dolgOne == _dolgTwo;
            var isFullOther = other._dolgOne == other._dolgTwo;
            int result = isFull.CompareTo(isFullOther);
            if (result != 0)
            {
                return result;
            }

            result = _dolgOne.CompareTo(other._dolgOne);
            if (result != 0)
            {
                return result;
            }

            result = _dolgTwo.CompareTo(other._dolgTwo);
            return result;
        }
    }

    internal sealed class InvComparer : IComparer<InvEntry>
    {
        public int Compare(InvEntry x, InvEntry y)
        {
            return x.CompareTo(y);
        }
    }
}
