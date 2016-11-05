namespace ABClient
{
    using System;

    internal class Foe : IComparable, ICloneable
    {
        internal bool IsValid { get; private set; }

        private readonly string _triba;
        private readonly int _level;
        private readonly bool _nevid;
        private readonly string _link;

        internal Foe()
        {
            _nevid = true;
            IsValid = true;
            _link = string.Empty;
        }

        internal Foe(string triba, int level, string link)
        {
            _triba = triba;
            _level = level;
            _link = link;

            CheckValid();
        }

        internal Foe(string source)
        {
            _link = string.Empty;
            var p1 = source.IndexOf('[');
            if (p1 == -1)
            {
                _nevid = true;
                IsValid = true;
                return;
            }

            var p2 = source.IndexOf(']', p1 + 1);
            if (p2 == -1)
            {
                _nevid = true;
                IsValid = true;
                return;
            }

            _triba = source.Substring(0, p1);
            var slevel = source.Substring(p1 + 1, p2 - p1 - 1);
            if (int.TryParse(slevel, out _level))
            {
                CheckValid();
                return;
            }

            _nevid = true;
            IsValid = true;
        }

        internal int Level => _level;

        internal bool IsHuman()
        {
            return _nevid || _triba.Equals("Человек", StringComparison.OrdinalIgnoreCase);
        }

        public override string ToString()
        {
            return _nevid ? "Невидимка" : _triba + "[" + _level + "]";
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Foe))
            {
                return false;    
            }

            var other = (Foe)obj;
            if (_nevid && other._nevid)
            {
                return true;
            }

            if (!_nevid && !other._nevid)
            {
                if (
                    (string.CompareOrdinal(_triba, other._triba) == 0) &&
                    (_level == other._level) &&
                    (string.CompareOrdinal(_link, other._link) == 0))
                    return true;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return _triba.GetHashCode() + _level.GetHashCode() + _nevid.GetHashCode();
        }

        private void CheckValid()
        {
            IsValid = false;
            switch (_triba)
            {
                case "Невидимка":
                    IsValid = true;
                    break;
                case "Человек":
                    if (_level == 33 || (_level >= 0 && _level <= 26))
                    {
                        IsValid = true;
                    }

                    break;
                case "Огр":
                    if (_level >= 16 && _level <= 24)
                    {
                        IsValid = true;
                    }

                    break;
                case "Орк":
                    if (_level >= 1 && _level <= 15)
                    {
                        IsValid = true;
                    }
                    break;
                case "Гоблин":
                    if (_level >= 1 && _level <= 14)
                    {
                        IsValid = true;
                    }
                    break;
                case "Кабан":
                    if (_level >= 8 && _level <= 12)
                    {
                        IsValid = true;
                    }
                    break;
                case "Крыса":
                    if (_level >= 0 && _level <= 10)
                    {
                        IsValid = true;
                    }
                    break;
                case "Паук":
                    if (_level >= 1 && _level <= 5)
                    {
                        IsValid = true;
                    }
                    break;
                case "Ядовитый паук":
                    if (_level >= 6 && _level <= 10)
                    {
                        IsValid = true;
                    }
                    break;

                case "Зомби":
                    if (_level >= 10 && _level <= 15)
                    {
                        IsValid = true;
                    }
                    break;

                case "Скелет":
                    if (_level >= 7 && _level <= 10)
                    {
                        IsValid = true;
                    }
                    break;

                case "Скелет-Воин":
                    if (_level >= 11 && _level <= 15)
                    {
                        IsValid = true;
                    }
                    break;

                case "Разбойник":
                    if (_level >= 5 && _level <= 17)
                    {
                        IsValid = true;
                    }
                    break;

                case "Грабитель":
                    if (_level >= 6 && _level <= 16)
                    {
                        IsValid = true;
                    }
                    break;

                case "Сильф":
                    IsValid = true;
                    break;

                case "Нетопырь":
                    IsValid = true;
                    break;

                case "Королева Змей":
                    if (_level == 25)
                    {
                        IsValid = true;
                    }

                    break;

                case "Хранитель Леса":
                    if (_level == 25)
                    {
                        IsValid = true;
                    }

                    break;

                case "Громлех Синезубый":
                    if (_level == 25)
                    {
                        IsValid = true;
                    }

                    break;

                case "Выползень":
                    if (_level == 25)
                    {
                        IsValid = true;
                    }

                    break;
            }
        }

        #region IComparable Members

        public int CompareTo(object obj)
        {
            if (!(obj is Foe))
            {
                return 1;
            }

            var other = (Foe) obj;
            if (_nevid && other._nevid)
            {
                return 0;
            }

            if (!_nevid && !other._nevid)
            {
                var c1 = string.CompareOrdinal(_triba, other._triba);
                return c1 == 0 ? _level.CompareTo(other._level) : c1;
            }

            if (!_nevid && other._nevid)
            {
                return 1;
            }

            return -1;
        }

        #endregion

        #region ICloneable Members

        public object Clone()
        {
            return MemberwiseClone();
        }

        #endregion
    }
}
