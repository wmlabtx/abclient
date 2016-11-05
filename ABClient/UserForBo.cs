using System;

namespace ABClient
{
    using System.Collections.Generic;
    using ExtMap;
    using System.Text;

    internal class UserForBo : Object, IComparable
    {
        private enum FoePosition
        {
            /// <summary>
            /// Не вычислено, не получилось.
            /// </summary>
            Undefined,

            /// <summary>
            /// Перс в офлайне
            /// </summary>
            Offline,
            
            /// <summary>
            /// Вычислять ничего не надо, противник в городе.
            /// </summary>
            City,

            /// <summary>
            /// Положение точное, координата - m_locationReal.
            /// </summary>
            Defined,

            /// <summary>
            /// Положение неизвестно, ближайшее - m_locatonClosest, возможных - m_numberPossibleLocations
            /// </summary>
            Closest
        };

        private FoePosition m_foePosition;
        private string m_locationReal;
        private string m_locatonClosest;
        private int m_numberPossibleLocations;
        private int m_numSteps;

        internal string Nick { get; set; }

        internal string Level { get; set; }

        internal bool IsOnline { get; set; }

        internal string LocationOne { get; set; }

        internal string LocationTwo { get; set; }

        internal string LocationFromBo { get; set; }

        internal string DescriptionFromBo { get; set; }

        internal string MoveToLocation { get; private set; }

        internal bool IsAvailableToGo()
        {
            return m_foePosition != FoePosition.Undefined && m_foePosition != FoePosition.Offline;
        }

        public int CompareTo(object obj)
        {
            var other = obj as UserForBo;
            var thisRank = 0;
            switch (m_foePosition)
            {
                case FoePosition.Undefined:
                    thisRank = 1;
                    break;
                case FoePosition.Offline:
                    thisRank = 2;
                    break;
                case FoePosition.Closest:
                    thisRank = 3;
                    break;
                case FoePosition.City:
                case FoePosition.Defined:
                    thisRank = 4;
                    break;
            }

            var otherRank = 0;
            if (other != null)
            {
                switch (other.m_foePosition)
                {
                    case FoePosition.Undefined:
                        otherRank = 1;
                        break;
                    case FoePosition.Offline:
                        otherRank = 2;
                        break;
                    case FoePosition.Closest:
                        otherRank = 3;
                        break;
                    case FoePosition.City:
                    case FoePosition.Defined:
                        otherRank = 4;
                        break;
                }
            }

            if (thisRank != otherRank)
            {
                return thisRank > otherRank ? 1 : -1;
            }

            if (other != null)
            {
                switch (m_foePosition)
                {
                    case FoePosition.Undefined:
                    case FoePosition.Offline:
                        return 0;
                    case FoePosition.Closest:
                    case FoePosition.City:
                    case FoePosition.Defined:
                        if (m_numSteps != other.m_numSteps)
                        {
                            return m_numSteps < other.m_numSteps ? 1 : -1;
                        }

                        return 0;
                }
            }

            return 1;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            if (Nick != null)
            {
                sb.Append(Nick);
            }

            if (Level != null)
            {
                sb.Append('[');
                sb.Append(Level);
                sb.Append("]: ");
            }

            if (!IsOnline)
            {
                sb.Append("В офлайне.");
            }
            else
            {
                switch (m_foePosition)
                {
                    case FoePosition.Undefined:
                        sb.Append("Положение не удалось определить.");
                        break;

                    case FoePosition.City:
                        sb.Append(LocationOne);
                        sb.Append(", ");
                        sb.Append(LocationTwo);
                        sb.Append('.');
                        break;

                    case FoePosition.Defined:
                        sb.Append(m_locationReal);
                        if (m_locationReal.Equals(AppVars.LocationReal, StringComparison.Ordinal))
                        {
                            sb.Append(" (рядом на клетке).");
                        }
                        else
                        {
                            sb.Append(". ");
                            sb.Append(LocationTwo);
                            sb.Append('.');
                        }

                        break;

                    case FoePosition.Closest:
                        sb.Append(LocationTwo);
                        sb.Append(". Возможных клеток: ");
                        sb.Append(m_numberPossibleLocations);
                        sb.Append(", ближайшая: ");
                        sb.Append(m_locatonClosest);
                        sb.Append('.');
                        break;
                }

                if (m_numSteps > 0)
                {
                    sb.Append(" Шагов: ");
                    sb.Append(m_numSteps);
                    sb.Append('.');
                }
            }

            return sb.ToString();
        }

        internal void CalculatePath()
        {
            m_foePosition = FoePosition.Undefined;
            m_locationReal = string.Empty;
            m_numSteps = 0;

            if (IsOnline)
            {
                if (!LocationOne.Equals("Форпост", StringComparison.OrdinalIgnoreCase) &&
                    !LocationOne.Equals("Октал", StringComparison.OrdinalIgnoreCase) &&
                    !LocationOne.Equals("Деревня", StringComparison.OrdinalIgnoreCase))
                {
                    if (DescriptionFromBo == null ||
                        (DescriptionFromBo != null && !DescriptionFromBo.Equals(LocationTwo)))
                    {
                        LocationFromBo = null;
                    }

                    if (LocationFromBo != null)
                    {
                        m_locationReal = LocationFromBo;
                        MoveToLocation = m_locationReal;
                        var path = new MapPath(AppVars.LocationReal, new[] { LocationFromBo });
                        m_numSteps = path.Jumps;
                        m_foePosition = FoePosition.Defined;
                    }
                    else
                    {
                        var arrayPossibleLocations = new List<string>();
                        foreach (var cellKey in Map.Cells.Keys)
                        {
                            var cell = Map.Cells[cellKey];
                            if (cell.Tooltip.Equals(LocationTwo, StringComparison.OrdinalIgnoreCase))
                            {
                                arrayPossibleLocations.Add(cellKey);
                            }
                        }

                        if (arrayPossibleLocations.Count > 0)
                        {
                            m_numberPossibleLocations = arrayPossibleLocations.Count;
                            var path = new MapPath(AppVars.LocationReal, arrayPossibleLocations.ToArray());
                            m_numSteps = path.Jumps;
                            if (arrayPossibleLocations.Count == 1)
                            {
                                m_locationReal = arrayPossibleLocations[0];
                                MoveToLocation = m_locationReal;
                                m_foePosition = FoePosition.Defined;
                            }
                            else
                            {
                                m_locatonClosest = path.Destination ?? AppVars.LocationReal;
                                MoveToLocation = m_locatonClosest;
                                m_foePosition = FoePosition.Closest;
                            }
                        }
                    }
                }
                else
                {
                    string[] dest = null;
                    switch (LocationOne)
                    {
                        case "Форпост":
                            dest = new[] { "8-259", "8-294" };
                            break;
                        case "Октал":
                            dest = new[] { "12-428", "12-494" };
                            break;
                        case "Деревня":
                            dest = new[] { "8-197", "8-228", "8-229" };
                            break;
                    }

                    if (dest != null)
                    {
                        var path = new MapPath(AppVars.LocationReal, dest);
                        MoveToLocation = path.Destination;
                        m_numSteps = path.Jumps;
                        m_foePosition = FoePosition.City;
                    }
                }
            }
            else
            {
                m_foePosition = FoePosition.Offline;
            }
        }
    }
}