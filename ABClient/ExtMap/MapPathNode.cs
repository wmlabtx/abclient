using System;

namespace ABClient.ExtMap
{
    [Serializable]
    public class MapPathNode : IComparable
    {
        public string[] CellNumbers { get; private set; }
        public int[] Costs { get; private set; }
        public bool HasTeleport { get; private set; }
        public int BotLevel { get; private set; }
        public int Jumps { get; private set; }

        public string CellNumber => CellNumbers.Length == 0 ? null : CellNumbers[CellNumbers.Length - 1];
        public int Cost => Costs.Length == 0 ? 0 : Costs[Costs.Length - 1];

        private MapPathNode()
        {
        }

        public MapPathNode(string sourceCellNumber)
        {
            CellNumbers = new[] {sourceCellNumber};            
            HasTeleport = false;
            BotLevel = 0;
            Jumps = 0;

            Cell cell;
            if (!Map.Cells.TryGetValue(sourceCellNumber, out cell))
                return;

            Costs = new[] { 0 };
            BotLevel = cell.MaxBotLevel;
        }

        public MapPathNode AddCell(string cellNumber, bool isGate, bool isTeleport)
        {
            if (Array.IndexOf(CellNumbers, cellNumber) >= 0)
                return null;

            var cost = Cost;
            var jumps = Jumps;

            Cell cell;
            if (!isGate && !isTeleport)
            {
                if (!Map.Cells.TryGetValue(CellNumber, out cell))
                    return null;

                cost += cell.Cost;
                jumps++;
            }

            var hasTeleport = HasTeleport || isTeleport;

            if (!Map.Cells.TryGetValue(cellNumber, out cell))
                return null;

            var maxBotLevel = Math.Max(BotLevel, cell.MaxBotLevel);
            var node = new MapPathNode
            {
                CellNumbers = new string[CellNumbers.Length + 1],
                Costs = new int[Costs.Length + 1],
            };

            Array.Copy(CellNumbers, node.CellNumbers, CellNumbers.Length);
            Array.Copy(Costs, node.Costs, Costs.Length);

            node.CellNumbers[CellNumbers.Length] = cellNumber;
            node.Costs[Costs.Length] = cost;
            node.HasTeleport = hasTeleport;
            node.BotLevel = maxBotLevel;
            node.Jumps = jumps;
            return node;
        }

        public int CompareTo(object obj)
        {
            var other = (MapPathNode) obj;
            var result = Cost.CompareTo(other.Cost);
            if (result != 0)
                return result;

            result = CellNumbers.Length.CompareTo(other.CellNumbers.Length);
            if (result != 0)
                return result;

            result = BotLevel.CompareTo(other.BotLevel);
            if (result != 0)
                return result;

            result = HasTeleport.CompareTo(other.HasTeleport);
            return result;
        }
    }
}
