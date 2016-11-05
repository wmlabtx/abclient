using System;
using System.Collections.Generic;
using System.Text;

namespace ABClient.ExtMap
{
    public class Cell
    {
        private int _region;
        private int _num;
        public readonly List<MapBot> MapBots = new List<MapBot>();

        private int _cost;
        private string _tooltip;
        private int _minBotsLevel, _maxBotsLevel;
        private string _botsTooltip;

        public string CellNumber
        {
            set
            {
                if (string.IsNullOrEmpty(value))
                    return;

                var cellNumberSplitted = value.Split('-');
                if (cellNumberSplitted.Length != 2)
                    return;

                int region;
                if (!int.TryParse(cellNumberSplitted[0], out region))
                    return;

                int num;
                if (!int.TryParse(cellNumberSplitted[1], out num))
                    return;

                _region = region;
                _num = num;
            }

            get
            {
                return string.Format($"{_region}-{_num:D3}");
            }
        }

        public bool HasFish { set; get; }
        public bool HasWater { set; get; }
        public string HerbGroup { set; get; }
        public string Name { set; get; }        
        public string Updated { set; get; }
        public string NameUpdated { set; get; }
        public string CostUpdated { set; get; }

        public string Tooltip
        {
            set { _tooltip = value; }
            get
            {
                if (Map.AbcCells.ContainsKey(CellNumber))
                {
                    return Map.AbcCells[CellNumber].Label;
                }

                return _tooltip;
            }
        }

        public int Cost
        {
            set
            {
                _cost = value;
            }
            get
            {
                if (Map.AbcCells.ContainsKey(CellNumber))
                {
                    return Map.AbcCells[CellNumber].Cost;
                }

                return _cost;
            }
        }

        public string BotsTooltip => _botsTooltip;

        public int MinBotLevel => _minBotsLevel;

        public int MaxBotLevel => _maxBotsLevel;

        public override string ToString()
        {
            return CellNumber;
        }

        public void AddMapBot(MapBot mapBot)
        {
            MapBots.Add(mapBot);
            var sb = new StringBuilder();
            _minBotsLevel = 0;
            _maxBotsLevel = 0;
            foreach (var bot in MapBots)
            {
                if (sb.Length > 0)
                {
                    sb.AppendLine();
                    _minBotsLevel = Math.Min(_minBotsLevel, bot.MinLevel);
                    _maxBotsLevel = Math.Max(_maxBotsLevel, bot.MaxLevel);
                }
                else
                {
                    _minBotsLevel = bot.MinLevel;
                    _maxBotsLevel = bot.MaxLevel;
                }

                sb.AppendFormat("{0} {1}-{2}", bot.Name, bot.MinLevel, bot.MaxLevel);
            }

            _botsTooltip = sb.ToString();
        }

        public bool IsBot(string pattern)
        {
            foreach (var mapBot in MapBots)
            {
                if (mapBot.Name.IndexOf(pattern, StringComparison.CurrentCultureIgnoreCase) >= 0)
                    return true;
            }

            return false;
        }

        public static string NormalizeRegNum(string regnum)
        {
            if (string.IsNullOrEmpty(regnum))
                return null;

            var cellNumberSplitted = regnum.Split('-');
            if (cellNumberSplitted.Length != 2)
                return null;

            int region;
            if (!int.TryParse(cellNumberSplitted[0], out region))
                return null;

            int num;
            if (!int.TryParse(cellNumberSplitted[1], out num))
                return null;

            return string.Format($"{region}-{num:D3}");
        }
    }
}
