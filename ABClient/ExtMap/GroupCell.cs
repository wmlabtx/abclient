using System;
using System.Collections.Generic;
using System.Text;

namespace ABClient.ExtMap
{
    public class GroupCell : object, IComparable
    {
        public readonly string Name;
        public readonly int Level;
        public readonly SortedList<string, object> Cells;

        public GroupCell(string name)
        {
            Name = name;
            Level = -1;
            Cells = new SortedList<string, object>();
        }

        public GroupCell(string name, int level)
        {
            Name = name;
            Level = level;
            Cells = new SortedList<string, object>();
        }

        public override string ToString()
        {
            return Level < 0 ? Name : $"{Name} {Level:D2}";
        }

        public int CompareTo(object obj)
        {
            var other = (GroupCell)obj;

            var result = string.Compare(Name, other.Name, StringComparison.CurrentCultureIgnoreCase);
            if (result != 0)
                return result;

            result = Level.CompareTo(other.Level);
            return result;
        }

        public void AddCell(string cellNumber)
        {
            if (!Cells.ContainsKey(cellNumber))
                Cells.Add(cellNumber, null);
        }

        public string GetCells()
        {
            var sb = new StringBuilder();
            foreach (var cellNumber in Cells.Keys)
            {
                if (sb.Length > 0)
                    sb.Append('|');

                sb.Append(cellNumber);
            }

            return sb.ToString();
        }
    }
}
