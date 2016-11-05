using ABClient.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using ABClient.ExtMap;

namespace ABClient.MyForms
{
    public partial class FormNavigator : Form
    {
        private readonly AutoCompleteStringCollection _scAutoComplete = new AutoCompleteStringCollection();
        private string _destination;
        private readonly SortedDictionary<string, GroupCell> _herbs;
        private readonly SortedDictionary<string, GroupCell> _bots;

        private readonly string[] _compas;
        private readonly string _compasnick;

        internal FormNavigator(string destination, string loc, string loc2, string nick)
        {
            InitializeComponent();

            _herbs = new SortedDictionary<string, GroupCell>();
            _bots = new SortedDictionary<string, GroupCell>();
            foreach (var cellNumber in Map.Cells.Keys)
            {
                _scAutoComplete.Add(cellNumber);

                Cell cell;
                if (!Map.Cells.TryGetValue(cellNumber, out cell))
                    continue;

                if (!string.IsNullOrEmpty(cell.HerbGroup) && !cell.HerbGroup.Trim().Equals("0"))
                {
                    var sp = cell.HerbGroup.Split(',');
                    foreach (var herbString in sp)
                    {
                        int level;
                        if (int.TryParse(herbString.Trim(), out level))
                        {
                            var group = new GroupCell("Травы", level);
                            var key = group.ToString();
                            if (!_herbs.ContainsKey(key))
                            {
                                _herbs.Add(key, group);
                            }

                            _herbs[key].AddCell(cellNumber);
                        }
                    }
                }

                if (cell.MapBots != null && cell.MapBots.Count > 0)
                {
                    foreach (var mapBot in cell.MapBots)
                    {
                        var level = mapBot.MaxLevel;
                        var group = new GroupCell(mapBot.Name, level);
                        var key = group.ToString();
                        if (!_bots.ContainsKey(key))
                        {
                            _bots.Add(key, group);
                        }

                        _bots[key].AddCell(cellNumber);
                    }
                }
            }

            textDest.AutoCompleteCustomSource = _scAutoComplete;

            _destination = destination;
            if (string.IsNullOrEmpty(_destination))
                _destination = AppVars.Profile.MapLocation;

            textDest.Text = _destination;

            PopulateStandardList();

            var navscriptmanager = new NavScriptManager(this);
            browserMap.Name = "browserMap";
            browserMap.ScriptErrorsSuppressed = true;
            browserMap.ObjectForScripting = navscriptmanager;

            PointToDest(new []{ _destination });

            _compas = null;
            _compasnick = null;
            if (loc == null)
            {
                return;
            }

            _compasnick = nick;
            if (loc == "Природа")
            {
                var tlist = new List<string>();
                foreach (var pos in Map.Cells.Keys)
                {
                    var c = Map.Cells[pos];
                    if (c.Tooltip.Equals(loc2, StringComparison.OrdinalIgnoreCase))
                    {
                        tlist.Add(pos);
                    }
                }

                _compas = tlist.ToArray();
            }
        }

        private static void AddLocation(TreeNode parentNode, string cellNumber)
        {
            var name = $"{cellNumber}. {Map.Cells[cellNumber].Tooltip}";
            MakeNode(parentNode, name, cellNumber);
        }

        private static void AddGroupLocation(TreeNode parentNode, GroupCell groupCell)
        {
            if (groupCell.Cells.Count == 0)
                return;

            var tooltip = groupCell.ToString();
            var tag = groupCell.GetCells();
            var cellNumber = groupCell.Cells.Count == 1 ? tag : groupCell.GetCells();
            var name = $"({groupCell.Cells.Count}шт) {tooltip}";
            var tgroup = MakeNode(parentNode, name, cellNumber);
            foreach (var cellNumver in groupCell.Cells.Keys)
            {
                AddLocation(tgroup, cellNumver);
            }
        }

        private void UpdateMap(string destination)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<HTML>");
            sb.AppendLine("<HEAD>");
            sb.AppendLine("<META Http-Equiv=\"Content-Type\" Content=\"text/html; charset=windows-1251\">");
            sb.AppendLine("<LINK href=\"http://www.neverlands.ru/css/main.css\" rel=\"STYLESHEET\" type=\"text/css\">");
            sb.AppendLine("</HEAD>");
            sb.AppendLine("<BODY leftmargin=0 topmargin=0 bottommargin=0 rightmargin=0>");
            sb.AppendLine("<SCRIPT language=\"JavaScript\">");
            sb.AppendLine(Resources.mapnav);
            var sloc = Map.InvLocation[destination];
            var coor = Map.Location[sloc];
            sb.AppendFormat("showMap({0}, {1});", coor.X, coor.Y);
            sb.AppendLine();
            sb.AppendLine("</SCRIPT>");
            sb.AppendLine("</BODY>");
            sb.AppendLine("</HTML>");

            browserMap.DocumentText = sb.ToString();
            while (browserMap.ReadyState != WebBrowserReadyState.Complete)
                Application.DoEvents();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            AppVars.AutoMovingDestinaton = _destination;
            AppVars.AutoMoving = true;
        }

        private void FormNavigator_Load(object sender, EventArgs e)
        {
            if (_compasnick != null)
            {
                Text = @"Возможное местонахождение " + _compasnick;
            }

            PointToDest(_compas ?? new []{_destination });
        }

        public void PointToDest(IList<string> dest)
        {
            try
            {
                var start = AppVars.Profile.MapLocation;
                var path = new MapPath(start, dest);
                if (path.PathExists)
                {
                    Text = $"Маршрут до {path.Destination}";
                    textDest.Text = path.Destination;
                    _destination = path.Destination;
                    labelJumps.Text = path.Jumps.ToString();
                    labelBotLevel.Text = path.BotLevel.ToString();
                    labelTied.Text = $"~{AppVars.Tied + (path.Jumps*2)}%";
                    UpdateMap(_destination);
                    buttonOk.Enabled = true;
                }
                else
                {
                    Text = @"Навигатор";
                    labelJumps.Text = @"-";
                    labelBotLevel.Text = @"-";
                    labelTied.Text = @"-";
                    UpdateMap(AppVars.Profile.MapLocation);
                    buttonOk.Enabled = false;
                }
            }
            catch (NullReferenceException)
            {
            }
        }

        private TreeNode MakeGroupNode(string name)
        {
            var tn = new TreeNode
            {
                Text = name,
                
                ForeColor = Color.DarkCyan,
                Tag = null
            };

            treeDest.Nodes.Add(tn);
            return tn;
        }

        private static TreeNode MakeNode(TreeNode parent, string name, string tag)
        {
            var tn = new TreeNode
            {
                Text = name,
                ForeColor = Color.Black,
                Tag = tag
            };

            parent.Nodes.Add(tn);
            return tn;
        }

        private void PopulateStandardList()
        {
            treeDest.BeginUpdate();
            treeDest.Nodes.Clear();

            TreeNode treeNode;
            if (AppVars.Profile.FavLocations != null && AppVars.Profile.FavLocations.Length > 0)
            {
                treeNode = MakeGroupNode("Запомненные локации");
                foreach (var cellNumber in AppVars.Profile.FavLocations)
                {
                    Cell cell;
                    if (!Map.Cells.TryGetValue(cellNumber, out cell))
                        continue; // + 2.15.1

                    AddLocation(treeNode, cellNumber);
                }
            }

            treeNode = MakeGroupNode("Форпост");
            AddLocation(treeNode, "8-259");
            AddLocation(treeNode, "8-294");

            treeNode = MakeGroupNode("Деревня");
            AddLocation(treeNode, "8-197");

            treeNode = MakeGroupNode("Октал");
            AddLocation(treeNode, "12-428");
            AddLocation(treeNode, "12-494");
            AddLocation(treeNode, "12-521");

            treeNode = MakeGroupNode("Замки");
            foreach (var cell in Map.Cells)
            {
                if (cell.Value.Tooltip.IndexOf("замок", StringComparison.CurrentCultureIgnoreCase) >= 0)
                    AddLocation(treeNode, cell.Key);
            }

            treeNode = MakeGroupNode("Форты группы 1");
            foreach (var cell in Map.Cells)
            {
                if (cell.Value.Name.IndexOf("ФортGA", StringComparison.CurrentCultureIgnoreCase) >= 0)
                    AddLocation(treeNode, cell.Key);
            }

            treeNode = MakeGroupNode("Форты группы 2");
            foreach (var cell in Map.Cells)
            {
                if (cell.Value.Name.IndexOf("ФортGB", StringComparison.CurrentCultureIgnoreCase) >= 0)
                    AddLocation(treeNode, cell.Key);
            }

            treeNode = MakeGroupNode("Форты группы 3");
            foreach (var cell in Map.Cells)
            {
                if (cell.Value.Name.IndexOf("ФортGC", StringComparison.CurrentCultureIgnoreCase) >= 0)
                    AddLocation(treeNode, cell.Key);
            }

            if (_bots.Count > 0)
            {
                treeNode = MakeGroupNode("Боты");
                foreach (var groupCell in _bots.Values)
                    AddGroupLocation(treeNode, groupCell);
            }

            if (_herbs.Count > 0)
            {
                treeNode = MakeGroupNode("Травы");
                foreach (var groupCell in _herbs.Values)
                    AddGroupLocation(treeNode, groupCell);
            }

            treeNode = MakeGroupNode("Шахты");
            foreach (var cell in Map.Cells)
            {
                if ((cell.Value.Tooltip.IndexOf("шахта", StringComparison.CurrentCultureIgnoreCase) >= 0) ||
                    (cell.Value.Tooltip.IndexOf("рудник провал", StringComparison.CurrentCultureIgnoreCase) >= 0))
                    AddLocation(treeNode, cell.Key);
            }

            treeNode = MakeGroupNode("Рыба");
            foreach (var cell in Map.Cells)
            {
                if (cell.Value.HasFish)
                    AddLocation(treeNode, cell.Key);
            }

            treeNode = MakeGroupNode("Причалы");
            foreach (var cell in Map.Cells)
            {
                if (cell.Value.Name.IndexOf("причал", StringComparison.CurrentCultureIgnoreCase) >= 0)
                    AddLocation(treeNode, cell.Key);
            }

            treeNode = MakeGroupNode("Лесопилки");
            foreach (var cell in Map.Cells)
            {
                if (cell.Value.Name.IndexOf("лесопилка", StringComparison.CurrentCultureIgnoreCase) >= 0)
                    AddLocation(treeNode, cell.Key);
            }

            treeNode = MakeGroupNode("Телепорты");
            foreach (var cell in Map.Teleports)
                AddLocation(treeNode, cell.Key);

            treeNode = MakeGroupNode("Объекты");
            AddLocation(treeNode, "8-227");
            AddLocation(treeNode, "2-482");
            AddLocation(treeNode, "9-494");
            AddLocation(treeNode, "26-430");

            treeNode = MakeGroupNode("Ресурсы охотников");

            PopulateSkinRes(treeNode, "Крысиные хвосты (0+)", "крысы");
            PopulateSkinRes(treeNode, "Крысиные лапы (15+)", "крысы");

            PopulateSkinRes(treeNode, "Мясо кабана (50+)", "кабаны");
            PopulateSkinRes(treeNode, "Шкуры кабана (65+)", "кабаны");
            PopulateSkinRes(treeNode, "Клык кабана (80+)", "кабаны");
            PopulateSkinRes(treeNode, "Копыта (100+)", "кабаны");

            PopulateSkinRes(treeNode, "Кости скелетов (120+)", "скелеты");
            PopulateSkinRes(treeNode, "Черепа (130+)", "скелеты");
            PopulateSkinRes(treeNode, "Зубы (150+)", "скелеты");

            PopulateSkinRes(treeNode, "Трупный яд (180+)", "зомби");
            PopulateSkinRes(treeNode, "Костный мозг (200+)", "зомби");
            PopulateSkinRes(treeNode, "Гниль (220+)", "зомби");

            PopulateSkinRes(treeNode, "Паучьи лапы (235+)", "пауки");
            PopulateSkinRes(treeNode, "Хитиновые панцири (250+)", "пауки");
            PopulateSkinRes(treeNode, "Паутина (260+)", "пауки");
            PopulateSkinRes(treeNode, "Жвала (280+)", "пауки");
            PopulateSkinRes(treeNode, "Паучьи яйца (300+)", "пауки");
            PopulateSkinRes(treeNode, "Ядовитые железы (320+)", "пауки");

            PopulateSkinRes(treeNode, "Крысиные глаза (350+)", "крысы");

            PopulateSkinRes(treeNode, "Медвежье мясо (400+)", "медведи");
            PopulateSkinRes(treeNode, "Шкуры медведей (500+)", "медведи");
            PopulateSkinRes(treeNode, "Медвежий жир (600+)", "медведи");
            PopulateSkinRes(treeNode, "Клыки (700+)", "медведи");

            /*
            | Перья | Шкура медведя | Медвежье мясо | Медвежий жир | Клок волос | Клыки Варга | Клык | Кожа | Шкура кабана | Мясо кабана | Крысиный глаз | 
            | Паучья лапа | Кость скелета | Трупный яд | Гниль | Зуб | Копыто | Паутина | Череп | Паучьи яйца | Хитиновый панцирь | Жвала | Клык кабана | Ядовитые железы | 
            | Костный мозг | Крысиная лапа | Крысиный хвост | 
            */

            treeDest.EndUpdate();

            if (treeDest.Nodes.Count > 0)
                treeDest.Nodes[0].Expand();
        }

        private void PopulateSkinRes(TreeNode parent, string title, string pattern)
        {
            var list = new List<string>();
            foreach (var cellNumber in Map.Cells.Keys)
            {
                var cell = Map.Cells[cellNumber];
                if (cell.IsBot(pattern))
                    list.Add(cell.CellNumber);
            }

            var groupCell = new GroupCell(title);
            foreach (var cellNumber in list)
            {
                groupCell.AddCell(cellNumber);
            }

            AddGroupLocation(parent, groupCell);

            //var rootNode = MakeNode(parent, title, cellNumber);
        }

        private void PopulateFavoriteList(string pattern)
        {
            treeDest.BeginUpdate();
            treeDest.Nodes.Clear();

            var sc = new SortedDictionary<string, List<string>>();
            foreach (var cellNumber in Map.Cells.Keys)
            {
                var cell = Map.Cells[cellNumber];
                if (cell.Tooltip.IndexOf(pattern, StringComparison.CurrentCultureIgnoreCase) >= 0)
                {
                    if (!sc.ContainsKey(cell.Tooltip))
                        sc.Add(cell.Tooltip, new List<string> { cell.CellNumber });
                    else
                    {
                        sc[cell.Tooltip].Add(cell.CellNumber);
                    }
                }
            }

            if (sc.Count > 0)
            {
                var treeNode = MakeGroupNode("Подходящие названия");
                foreach (var gc in sc)
                {
                    if (gc.Value.Count == 1)
                        AddLocation(treeNode, gc.Value[0]);
                    else
                    {
                        var groupCell = new GroupCell(gc.Key);
                        foreach (var cellNumber in gc.Value)
                        {
                            groupCell.AddCell(cellNumber);
                        }

                        AddGroupLocation(treeNode, groupCell);
                    }
                }
            }

            treeDest.EndUpdate();

            if (treeDest.Nodes.Count > 0)
                treeDest.Nodes[0].Expand();
        }

        private void textCell_TextChanged(object sender, EventArgs e)
        {
            bool isValid;
            try
            {
                var cellRegEx = new Regex(@"\d{1,2}-\d{3}");
                var cellMatch = cellRegEx.Match(textDest.Text);
                isValid = cellMatch.Success && Map.Cells.ContainsKey(textDest.Text);
            }
            catch
            {
                isValid = false;
            }

            textDest.BackColor = isValid ? SystemColors.Window : Color.Pink;
            buttonCalc.Enabled = isValid;
        }

        private void textTitleSuggest_TextChanged(object sender, EventArgs e)
        {
            if (textTooltipSuggest.Text.Length < 1)
            {
                PopulateStandardList();
                return;
            }

            PopulateFavoriteList(textTooltipSuggest.Text);
        }

        private void buttonSaveInFavorites_Click(object sender, EventArgs e)
        {
            AppVars.Profile.AddFavLocation(_destination);
            PopulateStandardList();
        }

        private void buttonClearFavorites_Click(object sender, EventArgs e)
        {
            AppVars.Profile.ClearFavLocations();
            PopulateStandardList();
        }

        private void buttonCalc_Click(object sender, EventArgs e)
        {
            PointToDest(new[] { textDest.Text });
        }

        private void treeDest_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeDest.Nodes.Count == 0)
                return;

            var tag = treeDest.SelectedNode.Tag;
            if (tag == null)
                return;

            var destination = ((string)tag).Split('|');
            PointToDest(destination);
        }
    }
}