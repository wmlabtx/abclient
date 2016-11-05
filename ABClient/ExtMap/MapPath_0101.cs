using System;
using System.Collections.Generic;
using System.Globalization;
using ABClient.Helpers;

namespace ABClient.ExtMap
{
    public enum CityGateType
    {
        None,
        ForpostRightToLeftGate,
        ForpostLeftToRightGate,
        OktalLeftToRightGate,
        OktalLeftToBottomGate,
        OktalRightToLeftGate,
        OktalRightToBottomGate,
        OktalBottomToLeftGate,
        OktalBottomToRightGate
    }

    public class MapPath
    {
        public bool PathExists { get; private set; }
        public string[] Path { get; private set; }
        public int Cost { get; private set; }
        public bool HasTeleport { get; private set; }
        public int BotLevel { get; private set; }
        public string HtmlResult { get; private set; }

        public string Destination { get; private set; }
        public int Jumps { get; private set; }

        public string NextJump { get; private set; }
        public bool IsNextTeleport { get; private set; }
        public bool IsNextCity { get; private set; }
        public CityGateType CityGate { get; private set; }

        private readonly List<MapPathNode> _treeNext;

        public MapPath(string sourceCellNumber, IList<string> destinationCellNumberList)
        {
            HtmlResult = string.Empty;
            PathExists = false;

            if (destinationCellNumberList == null)
                return;

            if (destinationCellNumberList.Count == 1 && sourceCellNumber.Equals(destinationCellNumberList[0]))
                return;

            var bestPathes = new List<MapPathNode>();
            var treeCurrent = new List<MapPathNode> { new MapPathNode(sourceCellNumber) };
            _treeNext = new List<MapPathNode>();
            

            var destinations = new SortedDictionary<string, object>();
            foreach (var destinationCellNumber in destinationCellNumberList)
                destinations.Add(destinationCellNumber, null);

            while (treeCurrent.Count > 0)
            {
                foreach (var node in treeCurrent)
                {
                    string h;
                    if (!Map.InvLocation.TryGetValue(node.CellNumber, out h))
                        continue;

                    var scp = h.Split('/', '_');
                    var y = Convert.ToInt32(scp[0], CultureInfo.InvariantCulture);
                    var x = Convert.ToInt32(scp[1], CultureInfo.InvariantCulture);

                    var idx = new[] {0, 0, -1, 1, -1, 1, -1, 1};
                    var idy = new[] {-1, 1, 0, 0, -1, -1, 1, 1};

                    for (var i = 0; i < idx.Length; i++)
                    {
                        var xnew = x + idx[i];
                        var ynew = y + idy[i];
                        var hnew = Map.MakePosition(xnew, ynew);
                        if (!Map.Location.ContainsKey(hnew))
                            continue;

                        var nearCellNumber = Map.Location[hnew].RegNum;
                        if (!Map.Cells.ContainsKey(nearCellNumber))
                            continue;

                        var nextNode = node.AddCell(nearCellNumber, false, false);
                        if (nextNode != null)
                            AddNextNode(nextNode);
                    }

                    string[] newLocArray;
                    switch (node.CellNumber)
                    {
                        case "8-259":
                            newLocArray = new[] {"8-294"};
                            break;
                        case "8-294":
                            newLocArray = new[] {"8-259"};
                            break;
                        case "12-428":
                            newLocArray = new[] {"12-494", "12-521"};
                            break;
                        case "12-494":
                            newLocArray = new[] {"12-428", "12-521"};
                            break;
                        case "12-521":
                            newLocArray = new[] {"12-428", "12-494"};
                            break;
                        default:
                            newLocArray = new string[0];
                            break;
                    }

                    foreach (var gateCellNumber in newLocArray)
                    {
                        if (!Map.Cells.ContainsKey(gateCellNumber))
                            continue;

                        var nextNode = node.AddCell(gateCellNumber, true, false);
                        if (nextNode != null)
                            AddNextNode(nextNode);
                    }

                    if (!node.HasTeleport)
                    {
                        if (Map.Teleports.ContainsKey(node.CellNumber))
                        {
                            foreach (var teleportCellNumber in Map.Teleports.Keys)
                            {
                                if (string.CompareOrdinal(teleportCellNumber, node.CellNumber) == 0)
                                    continue;

                                if (!Map.Cells.ContainsKey(teleportCellNumber))
                                    continue;

                                var nextNode = node.AddCell(teleportCellNumber, false, true);
                                if (nextNode != null)
                                    AddNextNode(nextNode);
                            }
                        }
                    }
                }

                treeCurrent.Clear();
                foreach (var nextNode in _treeNext)
                {
                    if (destinations.ContainsKey(nextNode.CellNumber))
                    {
                        if (bestPathes.Count == 0)
                            bestPathes.Add(nextNode);
                        else
                        {
                            var result = nextNode.CompareTo(bestPathes[0]);
                            if (result <= 0)
                            {
                                if (result < 0)
                                    bestPathes.Clear();

                                bestPathes.Add(nextNode);
                            }
                        }

                        continue;
                    }

                    //if (nextNode.CellNumbers.Length > 100)
                    //    continue;

                    if (bestPathes.Count > 0 && nextNode.Cost > bestPathes[0].Cost)
                        continue;

                    treeCurrent.Add(nextNode);
                }

                _treeNext.Clear();
            }

            if (bestPathes.Count <= 0)
                return;

            var index = Dice.Make(bestPathes.Count);
            PathExists = true;
            Path = bestPathes[index].CellNumbers;
            Destination = bestPathes[index].CellNumber;
            Cost = bestPathes[index].Cost;            
            HasTeleport = bestPathes[index].HasTeleport;
            BotLevel = bestPathes[index].BotLevel;

            CanUseExistingPath(sourceCellNumber, Destination);
        }

        private void AddNextNode(MapPathNode nextNode)
        {
            for (var i = 0; i < _treeNext.Count; i++)
            {
                var index = Array.IndexOf(_treeNext[i].CellNumbers, nextNode.CellNumber);
                if (index >= 0)
                {
                    if (nextNode.Cost < _treeNext[i].Costs[index])
                    {
                        _treeNext[i] = nextNode;
                    }
                    else
                    {
                        return;
                    }
                }
            }

            _treeNext.Add(nextNode);
        }

        public bool CanUseExistingPath(string source, string destination)
        {
            if (string.IsNullOrEmpty(source) ||
                string.IsNullOrEmpty(destination) ||
                !PathExists ||
                Path.Length < 2)
                return false;

            if (source.Equals(destination))
                return false;

            if (!Destination.Equals(destination))
                return false;

            var index = Array.IndexOf(Path, source);
            if (index < 0)
                return false;

            Jumps = Path.Length - 1 - index;
            var currentCell = Path[index];
            NextJump = Path[index + 1];
            IsNextTeleport = false;
            IsNextCity = false;
            CityGate = CityGateType.None;

            if (Map.Teleports.ContainsKey(currentCell) &&
                Map.Teleports.ContainsKey(NextJump))
            {
                IsNextTeleport = true;
                return true;
            }

            if (currentCell.Equals("8-259") && NextJump.Equals("8-294"))
            {
                IsNextCity = true;
                CityGate = CityGateType.ForpostLeftToRightGate;
                return true;
            }

            if (currentCell.Equals("8-294") && NextJump.Equals("8-259"))
            {
                IsNextCity = true;
                CityGate = CityGateType.ForpostRightToLeftGate;
                return true;
            }

            if (currentCell.Equals("12-428") && NextJump.Equals("12-494"))
            {
                IsNextCity = true;
                CityGate = CityGateType.OktalLeftToRightGate;
                return true;
            }

            if (currentCell.Equals("12-494") && NextJump.Equals("12-428"))
            {
                IsNextCity = true;
                CityGate = CityGateType.OktalLeftToRightGate;
                return true;
            }

            if (currentCell.Equals("12-428") && NextJump.Equals("12-521"))
            {
                IsNextCity = true;
                CityGate = CityGateType.OktalLeftToBottomGate;
                return true;
            }

            if (currentCell.Equals("12-494") && NextJump.Equals("12-521"))
            {
                IsNextCity = true;
                CityGate = CityGateType.OktalRightToBottomGate;
                return true;
            }

            if (currentCell.Equals("12-521") && NextJump.Equals("12-428"))
            {
                IsNextCity = true;
                CityGate = CityGateType.OktalBottomToLeftGate;
                return true;
            }

            if (currentCell.Equals("12-521") && NextJump.Equals("12-494"))
            {
                IsNextCity = true;
                CityGate = CityGateType.OktalBottomToRightGate;
                return true;
            }

            return true;
        }
    }
}
