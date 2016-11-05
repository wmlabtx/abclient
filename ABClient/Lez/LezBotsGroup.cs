using System;

namespace ABClient.Lez
{
    [Serializable]
    public class LezBotsGroup : object, ICloneable, IComparable
    {
        public int Id;
        public int MinimalLevel;

        public bool DoRestoreHp;
        public bool DoRestoreMa;
        public int RestoreHp;
        public int RestoreMa;
        public bool DoAbilBlocks;
        public bool DoAbilHits;
        public bool DoMagHits;
        public int MagHits;
        public bool DoMagBlocks;
        public bool DoHits;
        public bool DoBlocks;
        public bool DoMiscAbils;

        public bool DoStopNow;
        public bool DoStopLowHp;
        public bool DoStopLowMa;
        public int StopLowHp;
        public int StopLowMa;
        public bool DoExit;
        public bool DoExitRisky;

        public int[] SpellsHits;
        public int[] SpellsBlocks;
        public int[] SpellsRestoreHp;
        public int[] SpellsRestoreMa;
        public int[] SpellsMisc;

        public LezBotsGroup(int id, int minimalLevel)
        {
            Change(id, minimalLevel);

            DoRestoreHp = true;
            DoRestoreMa = true;
            RestoreHp = 50;
            RestoreMa = 50;
            DoAbilBlocks = true;
            DoAbilHits = true;
            DoMagHits = true;
            MagHits = 5;
            DoMagBlocks = false;
            DoHits = true;
            DoBlocks = true;
            DoMiscAbils = true;

            DoStopNow = false;
            DoStopLowHp = false;
            DoStopLowMa = false;
            StopLowHp = 25;
            StopLowMa = 25;
            DoExit = false;
            DoExitRisky = true;

            SpellsHits = LezSpellCollection.Hits;
            SpellsBlocks = LezSpellCollection.Blocks;
            SpellsRestoreHp = LezSpellCollection.RestoreHp;
            SpellsRestoreMa = LezSpellCollection.RestoreMa;
            SpellsMisc = LezSpellCollection.Misc;
        }

        public void Change(int id, int minimalLevel)
        {
            Id = id;
            MinimalLevel = minimalLevel;
        }

        public override string ToString()
        {
            var plural = LezBotsClassCollection.GetClass(Id).Plural;
            return string.Format($"{plural} {MinimalLevel}+");
        }

        public int CompareTo(object obj)
        {
            var other = (LezBotsGroup) obj;
            var result = other.Id.CompareTo(Id);
            if (result != 0)
                return result;

            result = other.MinimalLevel.CompareTo(MinimalLevel);
            return result;
        }

        public object Clone()
        {
            return Helpers.Misc.DeepClone(this);
        }
    }
}
