using System;
using System.Text;
using ABClient.Helpers;

namespace ABClient.Lez
{
    [Serializable]
    public class LezNode : ICloneable, IComparable
    {
        public readonly int[] HitOps = new int[4];
        public readonly int[] HitCodes = new int[4];
        public int BlockCombo;
        public int BlockOp;
        public int BlockCode;
        public readonly bool[] MagicFlags = new bool[18];
        public readonly int[] MagicCodes = new int[18];

        private int _zScroll; // +2 snowball, +1 fury
        private int _zRestore; // +2 hp, +1 ma
        private int _zMag;  // +4 block, +2 hit, +1 other
        private int _zHit; // 0-32 (0 +3, 1 +4, 2 +6, 3 +8)
        private int _zBlock; // 0-4

        private int HitCounts()
        {
            var count = 0;
            foreach (var hitOp in HitOps)
            {
                if (hitOp > 0)
                    count++;
            }

            return count;
        }

        private int BlockCounts()
        {
            return (BlockOp > 0)? 1 : 0;
        }

        private int MagicCounts()
        {
            var count = 0;
            foreach (var magicFlags in MagicFlags)
            {
                if (magicFlags)
                    count++;
            }

            return count;
        }

        public bool IsValid()
        {
            var hitCounts = HitCounts();
            var blockCounts = BlockCounts();
            var magicCounts = MagicCounts();
            return ((hitCounts > 0 && magicCounts > 0) || (blockCounts > 0 && magicCounts > 0) || (hitCounts > 0 && blockCounts > 0) || hitCounts > 1);
        }

        public int Od(int[] posod)
        {
            var od = 0;

            var hitCounts = HitCounts();
            if (hitCounts > 0)
            {
                for (var i = 0; i < 4; i++)
                {
                    if (HitOps[i] > 0)
                        od += posod[HitCodes[i]];
                }

                var shtraud = new[] { 0, 0, 25, 75, 150, 250 };
                od += shtraud[hitCounts];
            }

            var blockCounts = BlockCounts();
            if (blockCounts > 0)
                od += posod[BlockCode];

            var magicCounts = MagicCounts();
            if (magicCounts > 0)
            {
                for (var i = 0; i < MagicFlags.Length; i++)
                {
                    if (MagicFlags[i])
                        od += posod[MagicCodes[i]];
                }
            }

            return od;
        }

        public int Mana(int[] posma)
        {
            var mana = 0;

            var hitCounts = HitCounts();
            if (hitCounts > 0)
            {
                for (var i = 0; i < 4; i++)
                {
                    if (HitOps[i] > 0)
                        mana += posma[HitCodes[i]];
                }
            }

            var blockCounts = BlockCounts();
            if (blockCounts > 0)
                mana += posma[BlockCode];

            var magicCounts = MagicCounts();
            if (magicCounts > 0)
            {
                for (var i = 0; i < MagicFlags.Length; i++)
                {
                    if (MagicFlags[i])
                        mana += posma[MagicCodes[i]];
                }
            }

            return mana;
        }

        private string Z()
        {
            return string.Format($"{_zScroll}.{_zRestore}.{_zMag:D2}.{_zBlock}.{_zHit:D2}");
        }

        public void AddHit(int combo, int op, int code)
        {
            HitOps[combo] = op;
            HitCodes[combo] = code;

            if (LezSpell.IsPhHit(code))
            {
                _zHit += code == 0 ? 3 : 4;
            }
            else
            {
                if (LezSpell.IsMagHit(code))
                {
                    _zHit += code == 2 ? 10 : 12;
                }
                else
                {
                    _zHit += 25;
                }
            }
        }

        public string PrintHit(int[] posod, int[] posma)
        {
            var sb = new StringBuilder();
            for (var i = 0; i < 4; i++)
            {
                if (HitOps[i] == 0)
                    sb.Append("[.]");
                else
                    sb.AppendFormat($"[{LezSpellCollection.Spells[HitCodes[i]].Name}]");
            }

            return string.Format($"Hit = \'{sb}\' Od = {Od(posod)} Mana = {Mana(posma)} Z = {Z()}");
        }

        public void AddBlock(int combo, int op, int code)
        {
            BlockCombo = combo;
            BlockOp = op;
            BlockCode = code;

            if (LezSpell.IsPhBlock(code))
            {
                _zBlock = LezSpellCollection.Spells[code].Name.Split('+').Length;
            }
            else
            {
                if (LezSpell.IsMagBlock(code))
                {
                    if (code == 29)
                        _zBlock = 1;
                    else
                    {
                        if (code == 30)
                            _zBlock = 2;
                        else
                        {
                            if (code == 31)
                                _zBlock = 3;
                        }
                    }
                }
                else
                {
                    _zMag += 4;
                }
            }
        }

        public string PrintBlock(int[] posod, int[] posma)
        {
            return string.Format($"Block = \'{LezSpellCollection.Spells[BlockCode].Name}\' BlockCombo = {BlockCombo} BlockOp = {BlockOp} BlockCode = {BlockCode} Od = {Od(posod)} Mana = {Mana(posma)} Z = {Z()}");
        }

        public void AddMagic(int op, int code, int zmag, int zrestore, int zscroll)
        {
            MagicFlags[op] = true;
            MagicCodes[op] = code;

            _zScroll += zscroll;
            _zRestore += zrestore;
            _zMag += zmag;
        }

        public string PrintMagic(int[] posod, int[] posma)
        {
            var sb = new StringBuilder();
            for (var i = 0; i < MagicFlags.Length; i++)
            {
                if (!MagicFlags[i])
                    sb.Append("[.]");
                else
                    sb.AppendFormat($"[{LezSpellCollection.Spells[MagicCodes[i]].Name}]");
            }

            return string.Format($"Magic = \'{sb}\' Od = {Od(posod)} Mana = {Mana(posma)} Z = {Z()}");
        }

        public void AddCombination(LezNode other)
        {
            for (var i = 0; i < 4; i++)
            {
                if (other.HitOps[i] > 0)
                {
                    HitOps[i] = other.HitOps[i];
                    HitCodes[i] = other.HitCodes[i];
                }
            }

            if (other.BlockOp > 0)
            {
                BlockCombo = other.BlockCombo;
                BlockOp = other.BlockOp;
                BlockCode = other.BlockCode;
            }

            for (var i = 0; i < other.MagicFlags.Length; i++)
            {
                if (other.MagicFlags[i])
                {
                    MagicFlags[i] = other.MagicFlags[i];
                    MagicCodes[i] = other.MagicCodes[i];
                }
            }

            _zScroll += other._zScroll;
            _zRestore += other._zRestore;
            _zMag += other._zMag;
            _zHit += other._zHit;
            _zBlock += other._zBlock;
        }

        public string PrintCombination(int[] posod, int[] posma)
        {
            var sb = new StringBuilder();
            sb.Append(PrintHit(posod, posma));
            sb.AppendLine();
            sb.Append(PrintBlock(posod, posma));
            sb.AppendLine();
            sb.Append(PrintMagic(posod, posma));
            return sb.ToString();
        }

        public bool HasNonPhBlock(LezBotsGroup foeGroup)
        {
            if (BlockOp > 0)
            {
                if (
                    LezSpell.IsMagBlock(BlockCode) ||
                    Array.IndexOf(foeGroup.SpellsBlocks, BlockCode) >= 0
                    )
                    return true;
            }

            for (var i = 0; i < MagicFlags.Length; i++)
            {
                if (MagicFlags[i])
                {
                    if (Array.IndexOf(foeGroup.SpellsBlocks, MagicCodes[i]) >= 0)
                        return true;
                }
            }

            return false;
        }

        public object Clone()
        {
            return (LezNode) Misc.DeepClone(this);
        }

        public int CompareTo(object obj)
        {
            var other = (LezNode) obj;
            var compare = string.Compare(Z(), other.Z(), StringComparison.Ordinal);
            return compare;
        }
    }
}
