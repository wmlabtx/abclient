using System.Xml;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace ABClient.ExtMap
{
    internal static class Map
    {
        internal static SortedDictionary<string, Position> Location { get; private set; }

        internal static SortedDictionary<string, string> InvLocation { get; private set; }

        //private static SortedDictionary<string, Position> InvPosition { get; set; }

        internal static SortedDictionary<string, Cell> Cells { get; private set; }

        internal static SortedDictionary<string, AbcCell> AbcCells { get; private set; }

        internal static SortedDictionary<string, string> MovableCells { get; private set; }

        internal static SortedList<string, string> Teleports { get; private set; }

        static Map()
        {
            Location = new SortedDictionary<string, Position>();
            InvLocation = new SortedDictionary<string, string>();
            //InvPosition = new SortedDictionary<string, Position>();
            Cells = new SortedDictionary<string, Cell>();
            AbcCells = new SortedDictionary<string, AbcCell>();
            MovableCells = new SortedDictionary<string, string>();

            AddRegion("1", 952, 954);
            AddRegion("2", 982, 954);
            AddRegion("3", 1012, 954);
            AddRegion("13", 1042, 954);
            AddRegion("4", 952, 973);
            AddRegion("5", 982, 973);
            AddRegion("6", 1012, 973);
            AddRegion("14", 1042, 973);
            AddRegion("7", 952, 992);
            AddRegion("8", 982, 992);
            AddRegion("9", 1012, 992);
            AddRegion("15", 1042, 992);
            AddRegion("10", 952, 1011);
            AddRegion("11", 982, 1011);
            AddRegion("12", 1012, 1011);
            AddRegion("16", 1042, 1011);
            AddRegion("17", 922, 954);
            AddRegion("18", 922, 973);
            AddRegion("19", 922, 992);
            AddRegion("20", 922, 1011);
            AddRegion("21", 922, 1030);
            AddRegion("22", 922, 1049);
            AddRegion("23", 952, 1030);
            AddRegion("24", 952, 1049);
            AddRegion("25", 982, 1030);
            AddRegion("26", 982, 1049);
            AddRegion("27", 1012, 1030);
            AddRegion("28", 1012, 1049);
            AddRegion("29", 1042, 1030);
            AddRegion("30", 1042, 1049);
            AddRegion("31", 1072, 954);
            AddRegion("32", 1072, 973);
            AddRegion("33", 1072, 992);
            AddRegion("34", 1072, 1011);
            AddRegion("35", 1072, 1030);
            AddRegion("36", 1072, 1049);

            LoadMap();
            LoadAbcMap();
            CompareMaps();
            LoadTeleports();
        }

        private static void LoadAbcMap()
        {
            var pathCellsXml = Path.Combine(Application.StartupPath, "abcells.xml");
            if (!File.Exists(pathCellsXml))
                return;

            string map2;
            try
            {
                map2 = File.ReadAllText(pathCellsXml, Encoding.UTF8);
            }
            catch
            {
                return;
            }

            //var sb = new StringBuilder();

            try
            {
                var xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(map2);
                var cellsNodeList = xmlDocument.GetElementsByTagName(AppConsts.TagAbcMapCell);
                foreach (XmlNode cellNode in cellsNodeList)
                {
                    if (cellNode.Attributes == null)
                        continue;

                    var abcCell = new AbcCell();
                    if (cellNode.Attributes[AppConsts.AttrAbcMapRegNum] == null)
                        continue;

                    abcCell.RegNum = cellNode.Attributes[AppConsts.AttrAbcMapRegNum].Value;
                    if (cellNode.Attributes[AppConsts.AttrAbcMapLabel] != null)
                    {
                        abcCell.Label = cellNode.Attributes[AppConsts.AttrAbcMapLabel].Value;
                        if (Cells.ContainsKey(abcCell.RegNum))
                        {
                            if (
                                !abcCell.Label.Equals(Cells[abcCell.RegNum].Tooltip,
                                    StringComparison.CurrentCultureIgnoreCase))
                            {
                                //sb.Append(
                                //    $"Клетка [{abcCell.RegNum}] помечена как [{Cells[abcCell.RegNum].Tooltip}], должно быть [{abcCell.Label}]");
                                //sb.AppendLine();
                            }
                        }
                        else
                        {
                            //sb.Append($"Клетка [{abcCell.RegNum}] отсутствует");
                            //sb.AppendLine();
                        }
                    }

                    var cost = 0;
                    if (cellNode.Attributes[AppConsts.AttrAbcMapCost] != null)
                    {
                        if (!int.TryParse(cellNode.Attributes[AppConsts.AttrAbcMapCost].Value, out cost))
                        {
                            cost = 0;
                        }
                    }

                    abcCell.Cost = cost;
                    if (abcCell.Cost == 0)
                    {
                        if (Cells.ContainsKey(abcCell.RegNum))
                        {
                            var jcost = Cells[abcCell.RegNum].Cost;
                            if (jcost == 21)
                                abcCell.Cost = 30;
                            else
                            {
                                if (jcost == 28)
                                    abcCell.Cost = 40;
                                else
                                {
                                    if (jcost == 43)
                                        abcCell.Cost = 60;
                                }
                            }
                        }
                    }

                    var visited = DateTime.MinValue;
                    if (cellNode.Attributes[AppConsts.AttrAbcMapVisited] != null)
                    {
                        if (!DateTime.TryParse(cellNode.Attributes[AppConsts.AttrAbcMapVisited].Value, AppVars.EnUsCulture, DateTimeStyles.None, out visited))
                        {
                            visited = DateTime.MinValue;
                        }
                    }

                    abcCell.Visited = visited;

                    var verified = DateTime.MinValue;
                    if (cellNode.Attributes[AppConsts.AttrAbcMapVerified] != null)
                    {
                        if (!DateTime.TryParse(cellNode.Attributes[AppConsts.AttrAbcMapVerified].Value, AppVars.EnUsCulture, DateTimeStyles.None, out verified))
                        {
                            verified = DateTime.MinValue;
                        }
                    }

                    abcCell.Verified = verified;

                    AbcCells.Add(abcCell.RegNum, abcCell);
                }
            }
            catch
            {
            }

            //File.WriteAllText("maps-diff.txt", sb.ToString());
        }

        private static void CompareMaps()
        {
            var todelete = new List<string>();
            foreach (var cell in Cells)
            {
                if (AbcCells.ContainsKey(cell.Value.CellNumber))
                    continue;

                todelete.Add(cell.Value.CellNumber);
            }

            foreach (var regnum in todelete)
            {
                Cells.Remove(regnum);
            }

            foreach (var abcCell in AbcCells)
            {
                if (Cells.ContainsKey(abcCell.Value.RegNum))
                    continue;

                var cell = new Cell
                {
                    CellNumber = abcCell.Value.RegNum,
                    Name = abcCell.Value.Label
                };

                Cells.Add(cell.CellNumber, cell);
            }
        }

        private static void LoadTeleports()
        {
            var pathTeleportsXml = Path.Combine(Application.StartupPath, "abteleports.xml");
            if (!File.Exists(pathTeleportsXml))
                return;

            string teleports2;
            try
            {
                teleports2 = File.ReadAllText(pathTeleportsXml, Encoding.UTF8);
            }
            catch
            {
                return;
            }

            Teleports = new SortedList<string, string>();
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(teleports2);
            var teleportsNodeList = xmlDocument.GetElementsByTagName("teleport");
            foreach (XmlNode teleportNode in teleportsNodeList)
            {
                if (teleportNode.Attributes == null)
                    continue;

                var regnum = teleportNode.Attributes["regnum"].Value;
                var name = teleportNode.Attributes["name"].Value;
                Teleports.Add(regnum, name);
            }
        }

        private static void LoadMap()
        {
            var pathCellsXml = Path.Combine(Application.StartupPath, "map.xml");
            if (!File.Exists(pathCellsXml))
                return;
            
            var xml = new XmlDocument();
            xml.LoadXml(File.ReadAllText(pathCellsXml));
            var ecells = xml.GetElementsByTagName(AppConsts.TagMapCell);
            foreach (XmlNode ecell in ecells)
            {
                if (ecell.Attributes == null)
                    continue;

                var cell = new Cell();
                cell.CellNumber = ecell.Attributes[AppConsts.AttrMapCellNumber].Value.Trim();
                int cost;
                int.TryParse(ecell.Attributes[AppConsts.AttrMapCellCost].Value.Trim(), out cost);
                cell.Cost = cost;
                bool hasFish;
                bool.TryParse(ecell.Attributes[AppConsts.AttrMapCellHasFish].Value.Trim(), out hasFish);
                cell.HasFish = hasFish;
                bool hasWater;
                bool.TryParse(ecell.Attributes[AppConsts.AttrMapCellHasWater].Value.Trim(), out hasWater);
                cell.HasWater = hasWater;
                cell.HerbGroup = ecell.Attributes[AppConsts.AttrMapCellHerbGroup].Value.Trim();
                cell.Name = ecell.Attributes[AppConsts.AttrMapCellName].Value.Trim();
                cell.Tooltip = ecell.Attributes[AppConsts.AttrMapCellTooltip].Value.Trim();
                cell.Updated = ecell.Attributes[AppConsts.AttrMapCellUpdated].Value.Trim();
                cell.NameUpdated = ecell.Attributes[AppConsts.AttrMapCellNameUpdated].Value.Trim();
                cell.CostUpdated = ecell.Attributes[AppConsts.AttrMapCellCostUpdated].Value.Trim();
                Cells.Add(cell.CellNumber, cell);
            }

            var ebots = xml.GetElementsByTagName(AppConsts.TagMapBots);
            foreach (XmlNode ebot in ebots)
            {
                if (ebot.Attributes == null)
                    continue;

                var name = ebot.Attributes[AppConsts.AttrMapBotsName].Value.Trim();
                int minLevel;
                int.TryParse(ebot.Attributes[AppConsts.AttrMapBotsMinLevel].Value, out minLevel);
                int maxLevel;
                int.TryParse(ebot.Attributes[AppConsts.AttrMapBotsMaxLevel].Value, out maxLevel);
                var c = ebot.Attributes[AppConsts.AttrMapBotsC].Value;
                var d = ebot.Attributes[AppConsts.AttrMapBotsD].Value;
                var mapbot = new MapBot(name, minLevel, maxLevel, c, d);

                var ecell = ebot.ParentNode;
                if (ecell != null)
                {
                    var cellNumber = Cell.NormalizeRegNum(ecell.Attributes[AppConsts.AttrMapCellNumber].Value.Trim());
                    if (Cells.ContainsKey(cellNumber))
                    {
                        Cells[cellNumber].AddMapBot(mapbot);
                    }
                    else
                    {
                        throw new Exception(cellNumber);
                    }
                }
            }
        }

        public static void SaveAbcMap()
        {
            var wSettings = new XmlWriterSettings { Indent = true };
            var ms = new MemoryStream();
            var xw = XmlWriter.Create(ms, wSettings);
            xw.WriteStartDocument();
            xw.WriteStartElement("cells");

            foreach (var abcCell in AbcCells)
            {
                xw.WriteStartElement(AppConsts.TagAbcMapCell);

                xw.WriteStartAttribute(AppConsts.AttrAbcMapRegNum);
                xw.WriteString(abcCell.Value.RegNum);
                xw.WriteEndAttribute();

                xw.WriteStartAttribute(AppConsts.AttrAbcMapLabel);
                xw.WriteString(abcCell.Value.Label);
                xw.WriteEndAttribute();

                xw.WriteStartAttribute(AppConsts.AttrAbcMapCost);
                xw.WriteValue(abcCell.Value.Cost);
                xw.WriteEndAttribute();

                xw.WriteStartAttribute(AppConsts.AttrAbcMapVisited);
                xw.WriteString(abcCell.Value.Visited.ToString(AppVars.EnUsCulture));
                xw.WriteEndAttribute();

                xw.WriteStartAttribute(AppConsts.AttrAbcMapVerified);
                xw.WriteString(abcCell.Value.Verified.ToString(AppVars.EnUsCulture));
                xw.WriteEndAttribute();

                xw.WriteEndElement();
            }

            xw.WriteEndElement();

            xw.WriteEndDocument();
            xw.Flush();

            var fileStream = new FileStream("abcells_new.xml", FileMode.Create);
            ms.WriteTo(fileStream);
            fileStream.Close();
            ms.Close();

            try
            {
                File.Delete("abcells.xml");
                File.Move("abcells_new.xml", "abcells.xml");
            }
            catch (IOException)
            {
            }
        }

        internal static string ShowMiniMap(int xmove, int ymove)
        {
            return AppVars.Profile.MapShowMiniMap ? 
                "<br>" +
                ShowMap(
                    InvLocation[AppVars.Profile.MapLocation],
                    null,
                    AppVars.Profile.MapMiniWidth,
                    AppVars.Profile.MapMiniHeight,
                    AppVars.Profile.MapMiniScale,
                    xmove,
                    ymove) +
                @"<br><input type=button class=lbut onclick=""window.external.ShowMiniMap(false)"" value=""Выключить мини-карту"">" : 
                @"<input type=button class=lbut onclick=""window.external.ShowMiniMap(true)"" value=""Включить мини-карту"">";
        }

        private static string ShowMap(string sloc, StringCollection sc, int width, int height, int scale, int xmove, int ymove)
        {
            if (sloc == null)
            {
                throw new ArgumentNullException("sloc");
            }

            if (!Location.ContainsKey(sloc))
            {
                MessageBox.Show(
                    @"Пропущена клетка " + sloc,
                    AppVars.AppVersion.NickProductShortVersion,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return null;
            }

            const int worldxmin = 922;
            const int worldxmax = 1101;
            const int worldymin = 954;
            const int worldymax = 1067;

            //var backcolor = AppVars.Profile.MapShowBackColorWhite ? "white" : "black";
            var coor = Location[sloc];
            var xmin = coor.X - ((width - 1) / 2);
            var xmax = coor.X + ((width - 1) / 2);
            if (xmin < worldxmin)
            {
                var dx = worldxmin - xmin;
                xmin = worldxmin;
                xmax += dx;
            }
            else
            {
                if (xmax > worldxmax)
                {
                    var dx = xmax - worldxmax;
                    xmin -= dx;
                    xmax = worldxmax;
                }
            }

            var ymin = coor.Y - (height - 1) / 2;
            var ymax = coor.Y + (height - 1) / 2;
            if (ymin < worldymin)
            {
                var dy = worldymin - ymin;
                ymin = worldymin;
                ymax += dy;
            }
            else
            {
                if (ymax > worldymax)
                {
                    var dy = ymax - worldymax;
                    ymin -= dy;
                    ymax = worldymax;
                }
            }

            var sb = new StringBuilder(0x10000);

            sb.AppendLine("<TABLE cellSpacing=0 cellPadding=1 width=\"100%\" border=0>");
            sb.AppendLine("<TBODY>");
            sb.AppendLine("<TR>");
            sb.AppendLine("<TD bgColor=#ffffff align=center>");
            sb.AppendLine("<DIV id=world_cont style=\"OVERFLOW: hidden; HEIGHT: 304px; WIDTH: 304px; POSITION: absolute; TEXT-ALIGN: center\">");
            sb.AppendLine("<DIV id=world_map>");
            sb.AppendLine("<TABLE style=\"BORDER-TOP: #000000 1px solid; BORDER-RIGHT: #000000 1px solid; BORDER-COLLAPSE: collapse; BORDER-BOTTOM: #000000 1px solid; BORDER-LEFT: #000000 1px solid; BACKGROUND-COLOR: black\" cellSpacing=0 cellPadding=0>");
            sb.AppendLine("<TBODY>");

            for (var y = ymin; y <= ymax; y++)
            {
                var gy = "&gy=" + y.ToString(CultureInfo.InvariantCulture);
                sb.AppendLine("<TR>");
                for (var x = xmin; x <= xmax; x++)
                {
                    var gx = "&gx=" + x.ToString(CultureInfo.InvariantCulture);
                    string link = null;
                    if (sc != null)
                    {
                        for (var i = 0; i < sc.Count; i++)
                        {
                            if (sc[i].IndexOf(gx, StringComparison.OrdinalIgnoreCase) == -1 ||
                                sc[i].IndexOf(gy, StringComparison.OrdinalIgnoreCase) == -1)
                            {
                                continue;
                            }

                            link = sc[i];
                            sc.RemoveAt(i);
                            break;
                        }
                    }

                    var showmove = (x == xmove) && (y == ymove);
                    var daynight = AppVars.CurrentDayNight ?? "day";
                    var curcoor = MakePosition(x, y);
                    var isframe = string.CompareOrdinal(curcoor, sloc) == 0;
                    AddCell(sb, x, y, scale, daynight, link, showmove, isframe);
                    sb.Append("</td>");
                }

                sb.Append("</tr>");
            }

            sb.AppendLine("</TBODY>");
            sb.AppendLine("</TABLE>");
            sb.AppendLine("</DIV>");
            sb.AppendLine("</DIV>");
            sb.AppendLine("</TD>");
            sb.AppendLine("</TR>");
            sb.AppendLine("</TBODY>");
            sb.AppendLine("</TABLE>");
            return sb.ToString();
        }

        private static void AddCell(StringBuilder sb, int x, int y, int scale, string daynight, string link, bool showmove, bool isframe)
        {
            if (sb == null)
            {
                throw new ArgumentNullException("sb");
            }

            if (daynight == null)
            {
                throw new ArgumentNullException("daynight");
            }

            var coor = MakePosition(x, y);
            if (coor == null)
            {
                return;
            }

            if (!Location.ContainsKey(coor))
            {
                return;
            }
            var regnum = Location[coor].RegNum;
            if (regnum == null)
            {
                return;
            }

            //if (Cells.ContainsKey(regnum))
            /*if (false)
                
            {
                var cell = Cells[regnum];
                sb.Append(@"<div style=""position: relative;""><img src=http://image.neverlands.ru/" + AppVars.PathToMap + "/");
                sb.Append(daynight);
                sb.Append('/');
                sb.Append(coor);
                sb.Append(".jpg width=");
                sb.Append(scale);
                sb.Append(" height=");
                sb.Append(scale);
                sb.Append(@" border=0 onclick=""");
                var script = string.IsNullOrEmpty(link)
                                 ?
                                     "window.external.MoveTo('" + regnum + "')"
                                 :
                                     "location='" + link + "'";

                sb.Append(script);
                 sb.Append(@""" style=""cursor:hand;""");
                
                sb.Append(@" alt = """);
                if (scale < 100 && !string.IsNullOrEmpty(cell.Bots2))
                {
                    sb.Append(MakeAltMini(regnum, cell));
                }
                else
                {
                    sb.Append(MakeAltShort(regnum, cell));
                }

                sb.Append(@"""");
                sb.Append(">");
                sb.Append(@"<div style=""position: absolute; top: 0px; left: 0px;"">");
                GetCellInfo(sb, regnum, cell, script, scale, isframe, showmove);
                sb.Append("</div></div>");
            }
                 
            else*/
            {
                /*
                <TD style="BORDER-TOP: #000000 1px solid; BORDER-RIGHT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid; BORDER-LEFT: #ffffff 1px solid">
                    * <IMG id=img_1001_1000 style="FILTER: alpha(opacity=33)" alt=Недоступно src="http://image.neverlands.ru/map/world/day/1000/1001_1000.jpg" width=100 height=100></TD>
                */

                sb.Append("<TD style=\"BORDER-TOP: #000000 1px solid; BORDER-RIGHT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid; BORDER-LEFT: #ffffff 1px solid\">");
                sb.AppendFormat(
                    "<IMG style=\"-ms-opacity:{0};\" alt=Недоступно src=\"http://image.neverlands.ru/map/world/{1}/{2}.jpg\" width={3} height={4}></TD>",
                    AppVars.Profile.MapBigTransparency, daynight, coor, scale, scale);
            }
        }

        internal static string CellDivText(int x, int y, int scale, string link, bool showmove, bool isframe)
        {
            var coor = MakePosition(x, y);
            if (coor == null)
                return string.Empty;

            Position position;
            if (!Location.TryGetValue(coor, out position))
                return string.Empty;

            var regnum = position.RegNum;
            if (regnum == null)
                return string.Empty;

            Cell cell;
            if (!Cells.TryGetValue(regnum, out cell))
                return string.Empty;

            var border = string.Empty;
            var id = string.Empty;
            if (isframe)
                border = "border:1px solid red;";

            if (showmove)
                border = "border:1px solid red;";

            if (showmove)
                id = "id=\"movingcell\" ";
            else
            {
                if (AppVars.AutoMoving &&
                    AppVars.AutoMovingMapPath != null && 
                    AppVars.AutoMovingMapPath.Path.Length > 0 && 
                    Array.IndexOf(AppVars.AutoMovingMapPath.Path, regnum) >= 0)
                    border = "border:1px solid red;";
            }

            var cellnumcolor = HexColorCost(cell.Cost);

            var shortlabel = cell.Tooltip;
            if (shortlabel.IndexOf(',') != -1)
                shortlabel = shortlabel.Split(',')[1];

            var sb = new StringBuilder();
            sb.Append($"<div {id} style=\"position:relative; left:2px; top:2px; width:90px; height:90px; {border} padding:2px; text-shadow:1px 1px 1px, -1px -1px 1px, -1px 1px 1px, 1px -1px 1px; font-family:Tahoma; font-size:9px; font-weight:bold; text-decoration:none;\">");
            sb.Append($"<span style=\"font-size:11px; color:{cellnumcolor}\">{regnum}</span>");
            sb.Append($"<br><span style=\"color:#C0C0C0\">{shortlabel}</span>");

            if (cell.HasFish)
                sb.Append("<br><span style=\"color:#33CCFF\">Рыба</span>");
            else
            {
                if (cell.HasWater)
                    sb.Append("<br><span style=\"color:#33CCFF\">Вода</span>");
            }

            if (!string.IsNullOrEmpty(cell.BotsTooltip))
            {
                sb.Append($"<br><span style=\"color:#88BBDD\">Боты {cell.MinBotLevel}");
                if (cell.MaxBotLevel > cell.MinBotLevel)
                    sb.Append($"-{cell.MaxBotLevel}");

                sb.Append("</span>");
            }

            if (!string.IsNullOrEmpty(cell.HerbGroup) && !cell.HerbGroup.Equals("0"))
            {
                HerbCell herbCell;
                if (AppVars.Profile.HerbCells.TryGetValue(cell.CellNumber, out herbCell))
                {
                    sb.AppendLine();
                    var diffUpdatedInTicks = DateTime.Now.Subtract(AppVars.Profile.ServDiff).Ticks - herbCell.UpdatedInTicks;
                    var diffUpdated = TimeSpan.FromTicks(diffUpdatedInTicks);
                    sb.Append("<br><span style=\"color:#");
                    if (diffUpdated.TotalHours < 1)
                    {
                        sb.Append("00CC00");
                    }
                    else
                    {
                        if (diffUpdated.TotalHours < 2)
                        {
                            sb.Append("009900");
                        }
                        else
                        {
                            if (diffUpdated.TotalHours < 3)
                            {
                                sb.Append("006600");
                            }
                            else
                            {
                                sb.Append(diffUpdated.TotalHours < 6 ? "003300" : "999999");
                            }
                        }
                    }

                    sb.Append("\">Травы ");
                    sb.Append(cell.HerbGroup);
                    if (diffUpdated.TotalHours < 6)
                    {
                        var timeUpdated = TimeSpan.FromTicks(herbCell.UpdatedInTicks);
                        sb.Append(" (");
                        sb.Append(timeUpdated.Hours);
                        sb.Append(':');
                        sb.AppendFormat("{0:00}", timeUpdated.Minutes);
                        sb.Append(")");
                    }

                    sb.Append("</span>");
                }
                else
                {
                    sb.Append($"<br><span style=\"color:#999999\">Травы {cell.HerbGroup}</span>");
                }
            }

            AbcCell abccell;
            if (AbcCells.TryGetValue(regnum, out abccell))
            {
                var span = DateTime.Now.Subtract(AppVars.Profile.ServDiff).Subtract(abccell.Visited);
                if (span.TotalDays < 1.0)
                {
                    var visitedcolor = HexColorVisited(span.TotalHours);
                    sb.Append($"<br><span style=\"color:{visitedcolor}\">{abccell.Visited.Hour:D2}:{abccell.Visited.Minute:D2}</span>");
                }
            }

            sb.Append("</div>");
            return sb.ToString();
        }

        internal static string CellAltText(int x, int y, int scale)
        {
            var coor = MakePosition(x, y);
            if (coor == null)
                return string.Empty;

            Position position;
            if (!Location.TryGetValue(coor, out position))
                return string.Empty;

            var regnum = position.RegNum;
            Cell cell;
            if (!Cells.TryGetValue(regnum, out cell))
                return string.Empty;

            var sb = new StringBuilder();
            sb.Append(cell.Tooltip);
            if (!string.IsNullOrEmpty(cell.BotsTooltip))
            {
                sb.AppendLine();
                sb.Append(cell.BotsTooltip);
            }

            HerbCell herbCell;
            if (AppVars.Profile.HerbCells.TryGetValue(regnum, out herbCell))
            {
                sb.AppendLine();
                var diffUpdatedInTicks = DateTime.Now.Subtract(AppVars.Profile.ServDiff).Ticks - herbCell.UpdatedInTicks;
                var diffUpdated = TimeSpan.FromTicks(diffUpdatedInTicks);
                sb.Append("(осмотрено ");
                if (diffUpdated.TotalMinutes < 1)
                {
                    sb.Append("только что");
                }
                else
                {
                    if (diffUpdated.Hours > 0)
                    {
                        sb.AppendFormat("{0} час ", diffUpdated.Hours);
                    }

                    sb.AppendFormat("{0} мин", diffUpdated.Minutes);
                    sb.Append(" назад");
                }

                sb.Append(")");
                var par = herbCell.Herbs.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                if (par.Length == 0)
                {
                    sb.AppendLine();
                    sb.Append("Трав нет");
                }
                else
                {
                    for (var index = 0; index < par.Length; index++)
                    {
                        sb.AppendLine();
                        var hpar = par[index].Split(':');
                        if (hpar.Length != 2)
                        {
                            continue;
                        }

                        sb.Append(hpar[0]);
                        if (hpar[1].Equals("0"))
                        {
                            sb.Append(" (срезано)");
                        }
                    }
                }
            }

            if (cell.Cost > 0)
            {
                sb.AppendLine();
                sb.Append($"Время: {cell.Cost}");
            }

            return sb.ToString();
        }

        private static byte ColorInterpolate(byte a, byte b, double p)
        {
            return (byte)((a * (1 - p)) + (b * p));
        }

        private static Color ColorInterpolate(Color a, Color b, double p)
        {
            var c = Color.FromArgb(
                ColorInterpolate(a.R, b.R, p),
                ColorInterpolate(a.G, b.G, p),
                ColorInterpolate(a.B, b.B, p));
            return c;
        }

        private static Color ColorCost(int cost)
        {
            if (cost == 0)
                return Color.DarkGray;

            if (cost <= 30)
                return Color.LightGreen;

            if (cost <= 40)
                return Color.Yellow;

            if (cost >= 60)
                return Color.Red;

            return Color.Black;
        }

        private static Color ColorVisited(double hours)
        {
            if (hours < 0.0)
                return Color.LightGreen;

            if (hours < 1.0)
            {
                var p = hours / 1.0;
                return ColorInterpolate(Color.LightGreen, Color.Yellow, p);
            }

            if (hours < 6.0)
            {
                var p = (hours - 1.0) / 5.0;
                return ColorInterpolate(Color.Yellow, Color.Red, p);
            }

            return Color.Red;
        }

        private static string HexColorCost(int cost)
        {
            return ColorTranslator.ToHtml(ColorCost(cost));
        }

        private static string HexColorVisited(double hours)
        {
            return ColorTranslator.ToHtml(ColorVisited(hours));
        }

        /*
        private static void GetCellInfo(StringBuilder sb, string regnum, Cell cell, string script, int scale, bool isframe, bool showmove)
        {
            if (sb == null) throw new ArgumentNullException("sb");
            if (regnum == null) throw new ArgumentNullException("regnum");
            if (cell == null) throw new ArgumentNullException("cell");
            if (script == null) throw new ArgumentNullException("script");
            var padding = (scale < 100) ? 1 : 4;
            sb.Append(@"<span onclick=""");
            sb.Append(script);
            sb.Append(@"""><span style=""padding: ");
            sb.Append(padding);
            sb.Append("px; width:");
            sb.Append(scale);
            sb.Append(@"px; height:");
            sb.Append(scale);
            sb.Append(@"px; text-decoration: none; cursor:hand; filter:glow(color=black, strength=5)"">");
            var fontsize = (scale < 100) ? 9 : 11;
            if (isframe)
            {
                sb.Append(@"<span style=""padding: ");
                sb.Append(padding);
                sb.Append("px; width:");
                sb.Append(scale - (padding * 2));
                sb.Append(@"px; height:");
                sb.Append(scale - (padding * 2));
                sb.Append(@"px; font-family: Tahoma; font-size: ");
                sb.Append(fontsize);
                sb.Append(@"px; text-decoration: none; color: #FFFF00; font-weight: bold; border:1px red solid;"">");
            }
            else
            {
                if (showmove)
                {
                    sb.Append(@"<span id=""movingcell"" style=""padding: ");
                    sb.Append(padding);
                    sb.Append(@"px; width:");
                    sb.Append(scale - padding * 2);
                    sb.Append(@"px; height:");
                    sb.Append(scale - padding * 2);
                    sb.Append(@"px; font-family: Tahoma; font-size: ");
                    sb.Append(fontsize);
                    sb.Append(@"px; text-decoration: none; color: #FFFF00; font-weight: bold; border:1px red dotted;"">" +
                        @"<script type=""text/javascript"">" +
                        @"window.onload = flash1;" +
                        @"function flash1() {" +
                        @"  movingcell.style.borderColor='white';" +
                        @"  movingcell.className=""white"";" +
                        @"  setTimeout(""flash2()"", 50);" +
                        @"}" +
                        @"function flash2() {" +
                        @"  movingcell.style.borderColor='red';" +
                        @"  setTimeout(""flash1()"", 750);" +
                        @"}" +
                        @"</script>");
                }
                else
                {
                    if (
                        AppVars.AutoMoving &&
                        !string.IsNullOrEmpty(AppVars.AutoMovingPath) &&
                        (AppVars.AutoMovingPath.IndexOf(regnum + "|", StringComparison.Ordinal) != -1))
                    {
                        sb.Append(@"<span style=""padding: ");
                        sb.Append(padding);
                        sb.Append("px; width:");
                        sb.Append(scale - padding * 2);
                        sb.Append(@"px; height:");
                        sb.Append(scale - padding * 2);
                        sb.Append(@"px; font-family: Tahoma; font-size: ");
                        sb.Append(fontsize);
                        sb.Append(@"px; text-decoration: none; color: #FFFF00; font-weight: bold; border:1px #FF00FF dotted;"">");
                    }
                    else
                    {
                        sb.Append(@"<span style=""width:");
                        sb.Append(scale - padding * 2);
                        sb.Append(@"px; height:");
                        sb.Append(scale - padding * 2);
                        sb.Append(@"px; font-family: Tahoma; font-size: ");
                        sb.Append(fontsize);
                        sb.Append(@"px; text-decoration: none; color: #FFFF00; font-weight: bold;"">");
                    }
                }
            }

            string cellnumcolor;
            if (cell.Visited == DateTime.MinValue)
                cellnumcolor = "#666666";
            else
            {
                var elapsed = DateTime.Now.Subtract(cell.Visited).TotalDays;
                if (elapsed <= 1.0)
                {
                    cellnumcolor = "#66ff66";
                }
                else
                {
                    cellnumcolor = elapsed <= 31.0 ? "#ffff00" : "#ff0000";
                }
            }

            sb.Append(@"<span style=""color:");
            sb.Append(cellnumcolor);
            sb.Append(@""">");
            sb.Append(regnum);
            sb.Append(@"</span>");
            sb.Append(@"<span style=""font-family: Tahoma; font-size: 9px; font-weight: bold;""><br>");

            if (scale == 100)
            {
                var b = cell.Bots2;
                if (!string.IsNullOrEmpty(b))
                {
                    sb.Append(@"<span style=""color:#C0C0C0"">");
                    sb.Append(b);
                    sb.Append("</span><br>");
                }
            }

            if (!string.IsNullOrEmpty(cell.Herbs))
            {
                HerbCell herbCell;
                if (AppVars.Profile.HerbCells.TryGetValue(cell.GetRegNum(), out herbCell))
                {
                    sb.AppendLine();
                    var diffUpdatedInTicks = DateTime.Now.Subtract(AppVars.Profile.ServDiff).Ticks - herbCell.UpdatedInTicks;
                    var diffUpdated = TimeSpan.FromTicks(diffUpdatedInTicks);
                    sb.Append(@"<span style=""color:#");
                    if (diffUpdated.TotalHours < 1)
                    {
                        sb.Append("00cc00");
                    }
                    else
                    {
                        if (diffUpdated.TotalHours < 2)
                        {
                            sb.Append("009900");
                        }
                        else
                        {
                            if (diffUpdated.TotalHours < 3)
                            {
                                sb.Append("006600");
                            }
                            else
                            {
                                sb.Append(diffUpdated.TotalHours < 6 ? "003300" : "666666");
                            }
                        }
                    }

                    sb.Append(@""">Тр");
                    sb.Append(cell.Herbs);
                    if (diffUpdated.TotalHours < 6)
                    {
                        var timeUpdated = TimeSpan.FromTicks(herbCell.UpdatedInTicks);
                        sb.Append(" (");
                        sb.Append(timeUpdated.Hours);
                        sb.Append(':');
                        sb.AppendFormat("{0:00}", timeUpdated.Minutes);
                        sb.Append(")");
                    }

                    sb.Append("<br></span>");
                }
                else
                {
                    sb.Append(@"<span style=""color:#666666"">Травы ");
                    sb.Append(cell.Herbs);
                    sb.Append(@"<br></span>");
                }
            }

            switch (cell.Name)
            {
                case "Рыба":
                    sb.Append(@"<span style=""color:#33CCFF"">Рыба<br></span>");
                    break;
                case "Вода":
                    sb.Append(@"<span style=""color:#33CCFF"">Вода<br></span>");
                    break;
                case "Шахта":
                    sb.Append(@"<span style=""color:#FF9933"">Шахта<br></span>");
                    break;
                case "Причал":
                    sb.Append(@"<span style=""color:#33CCFF"">Причал<br></span>");
                    break;
                case "Телепорт":
                    sb.Append(@"<span style=""color:#FF9933"">Телепорт<br></span>");
                    break;
                case "Врата":
                 assessment   sb.Append(@"<span style=""color:#FF9933"">Врата<br></span>");
                    break;
                case "Деревня":
                    sb.Append(@"<span style=""color:#FF9933"">Деревня<br></span>");
                    break;
                case "Биржа":
                    sb.Append(@"<span style=""color:#FFD800"">Биржа<br></span>");
                    break;
                case "Замок":
                    sb.Append(@"<span style=""color:#FF9933"">Замок<br></span>");
                    break;
                case "Башня":
                    sb.Append(@"<span style=""color:#FF9933"">Башня<br></span>");
                    break;
                case "Форт":
                    sb.Append(@"<span style=""color:#FF9933"">Форт<br></span>");
                    break;
                case "Плавильня":
                    sb.Append(@"<span style=""color:#F2F5A9"">Плавильня<br></span>");
                    break;
                case "Лесопилка":
                    sb.Append(@"<span style=""color:#74DF00"">Лесопилка<br></span>");
                    break;
            }

            sb.Append(@"</span></span>");
        }
        */

        /*
    private static string MakeAltShort(string regnum, Cell cell)
    {
        var sb = new StringBuilder();
        sb.Append(regnum);
        sb.Append(' ');
        sb.Append(cell.Label);
        sb.Append(MakeAltHerbs(regnum));
        return sb.ToString();
    }
    */

        /*
    private static string MakeAltMini(string regnum, Cell cell)
    {
        var sb = new StringBuilder();
        sb.Append(regnum);
        sb.Append(' ');
        sb.Append(cell.Label);
        sb.AppendLine();
        sb.Append(cell.Bots2.Replace("<br>", Environment.NewLine));
        sb.Append(MakeAltHerbs(regnum));
        return sb.ToString();
    }
    */

        /*
        private static string MakeAlt(string regnum, Cell cell)
        {
            var sb = new StringBuilder();
            sb.Append(regnum);
            sb.Append(' ');
            sb.Append(cell.Label);
            sb.Append(MakeAltHerbs(regnum));
            return sb.ToString();
        }
         */

            /*
        private static string MakeAltHerbs(string location)
        {
            var sb = new StringBuilder();
            HerbCell herbCell;
            if (AppVars.Profile.HerbCells.TryGetValue(location, out herbCell))
            {
                sb.AppendLine();
                var diffUpdatedInTicks = DateTime.Now.Subtract(AppVars.Profile.ServDiff).Ticks - herbCell.UpdatedInTicks;
                var diffUpdated = TimeSpan.FromTicks(diffUpdatedInTicks);
                sb.Append("(осмотрено ");
                if (diffUpdated.TotalMinutes < 1)
                {
                    sb.Append("только что");
                }
                else
                {
                    if (diffUpdated.Hours > 0)
                    {
                        sb.AppendFormat("{0} час ", diffUpdated.Hours);
                    }

                    sb.AppendFormat("{0} мин", diffUpdated.Minutes);
                    sb.Append(" назад");
                }

                sb.Append(")");
                var par = herbCell.Herbs.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                if (par.Length == 0)
                {
                    sb.AppendLine();
                    sb.Append("Трав нет");
                }
                else
                {
                    for (var index = 0; index < par.Length; index++)
                    {
                        sb.AppendLine();
                        var hpar = par[index].Split(':');
                        if (hpar.Length != 2)
                        {
                            continue;
                        }

                        sb.Append(hpar[0]);
                        if (hpar[1].Equals("0"))
                        {
                            sb.Append(" (срезано)");
                        }
                    }
                }
            }

            return sb.ToString();
        }
        */

        internal static string ConvertToRegNum(int x, int y)
        {
            var sc = MakePosition(x, y);
            return Location.ContainsKey(sc) ? Location[sc].RegNum : string.Empty;
        }

        internal static string Move(string cur, int dx, int dy)
        {
            if (!InvLocation.ContainsKey(cur))
            {
                return null;
            }

            var h = InvLocation[cur];
            var scp = h.Split(new[] { '/', '_' });
            var ynew = Convert.ToInt32(scp[0], CultureInfo.InvariantCulture) + dy;
            var xnew = Convert.ToInt32(scp[1], CultureInfo.InvariantCulture) + dx;
            var hnew = MakePosition(xnew, ynew);
            if (!Location.ContainsKey(hnew))
            {
                return null;
            }

            var curnew = Location[hnew].RegNum;
            return !Cells.ContainsKey(curnew) ? null : curnew;
        }

        private static void AddRegion(string region, int xmin, int ymin)
        {
            var number = 1;
            var xmax = xmin + 29;
            var ymax = ymin + 18;
            for (var y = ymin; y <= ymax; y++)
            {
                for (var x = xmin; x <= xmax; x++)
                {
                    var h = MakePosition(x, y);
                    var l = MakeRegNum(region, number);
                    var p = new Position {RegNum = l, X = x, Y = y};
                    //InvPosition.Add(l, p);
                    Location.Add(h, p);
                    InvLocation.Add(l, h);
                    number++;
                }
            }
        }

        public static string MakeRegNum(string reg, IFormattable k)
        {
            return reg + "-" + k.ToString("000", CultureInfo.InvariantCulture);
        }

        public static string MakePosition(int x, int y)
        {
            return string.Format(CultureInfo.InvariantCulture, "{0}/{1}_{2}", y, x, y);
        }

        internal static void ResetAllVisitedCells()
        {
            foreach (var cell in AbcCells)
            {
                cell.Value.Visited = DateTime.MinValue;
            }
        }

        internal static bool IsCellInPath(int x, int y)
        {
            var coor = MakePosition(x, y);
            if (!Location.ContainsKey(coor))
                return false;

            var regnum = Location[coor].RegNum;
            if (regnum == null)
                return false;

            return
                (AppVars.AutoMoving &&
                 AppVars.AutoMovingMapPath != null &&
                 AppVars.AutoMovingMapPath.Path.Length > 0 &&
                 Array.IndexOf(AppVars.AutoMovingMapPath.Path, regnum) >= 0);
        }
    }
}
