using System;
using System.Collections.Generic;
using System.Globalization;
using ABClient.MyHelpers;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using ABClient.ABForms;
using Newtonsoft.Json.Linq;

namespace ABClient.Lez
{
    public class LezFight
    {
        public bool IsValid { get; }
        public bool IsBoi { get; private set; }
        public bool IsWaitingForNextTurn { get; private set; }
        public bool DoStop { get; private set; }
        public bool DoExit { get; private set; }
        public bool IsLowHp { get; private set; }
        public bool IsLowMa { get; private set; }
        public string LogBoi { get; private set; }
        public string FoeName { get; private set; }

        private string _html;        
        //private string _fileLog;
        private string[] _fightty;
        private string[] _fexp;
        private int _ftype;
        private int _currentHp, _maxHp;
        private int _currentMa, _maxMa;
        private int _percentHp, _percentMa;
        private string _foeImage, _foeName;
        private int _foeLevel, _foeGroupId;
        public LezBotsGroup FoeGroup;
        private int _magmax, _odmax, _hitval, _bs;
        private int[] _posod;
        private int[] _posma;
        private string[] _bspar;
        private bool _hitByScroll;
        private readonly List<int> _hits = new List<int>();
        private readonly List<bool> _ehits = new List<bool>();
        private readonly List<int> _magblocks = new List<int>();
        private readonly List<int>[] _blocks = { new List<int>(), new List<int>() , new List<int>(), new List<int>() };
        private readonly List<bool>[] _eblocks = { new List<bool>(), new List<bool>(), new List<bool>(), new List<bool>() };
        private readonly List<int> _magics = new List<int>();
        private readonly List<bool> _emagics = new List<bool>();

        private readonly List<LezNode> _lezHits = new List<LezNode> { new LezNode() };
        private readonly List<LezNode> _lezBlocks = new List<LezNode> { new LezNode() };
        private readonly List<LezNode> _lezMagics = new List<LezNode> { new LezNode() };

        public readonly List<LezNode> LezCombinations = new List<LezNode>();
        public LezNode LezCombination;
        public string Result;
        public string Frame;
        
        public LezFight(string html)
        {
            IsValid = Parse(html);
        }

        public void PrintDebug()
        {
            /*
            if (string.IsNullOrEmpty(_fileLog))
                return;

            var sb = new StringBuilder();
            sb.Append(_html);
            sb.AppendLine();
            sb.AppendFormat($"_currentHp = {_currentHp}, _maxHp = {_maxHp}");
            sb.AppendLine();
            sb.AppendFormat($"_currentMa = {_currentMa}, _maxMa = {_maxMa}");
            sb.AppendLine();
            sb.AppendFormat($"_foeImage = {_foeImage}, _foeName = {_foeName}");
            sb.AppendLine();
            sb.AppendFormat($"_foeLevel = {_foeLevel}, _foeGroupId = {_foeGroupId}");
            sb.AppendLine();
            sb.AppendFormat($"_foeGroup = {FoeGroup}");
            sb.AppendLine();
            sb.AppendFormat($"_magmax = {_magmax}, _odmax = {_odmax}, _hitval = {_hitval}, _bs = {_bs}");
            sb.AppendLine();

            sb.AppendFormat($"_hits[{_hits.Count}] = ");
            foreach (var e in _hits)
                sb.AppendFormat($" {e}({LezSpellCollection.PosType[e]},{LezSpellCollection.Spells[e].Name})");

            sb.AppendLine();

            sb.Append("_ehits = ");
            foreach (var e in _ehits)
                sb.AppendFormat($" {e}");

            sb.AppendLine();

            for (var i = 0; i < 4; i++)
            {
                sb.AppendFormat($"_blocks{i+1}[{_blocks[i].Count}] = ");
                foreach (var e in _blocks[i])
                    sb.AppendFormat($" {e}({LezSpellCollection.PosType[e]},{LezSpellCollection.Spells[e].Name})");

                sb.AppendLine();

                sb.AppendFormat($"_eblocks{i + 1} = ");
                foreach (var e in _eblocks[i])
                    sb.AppendFormat($" {e}");

                sb.AppendLine();
            }

            sb.AppendFormat($"_mags[{_magics.Count}] = ");
            foreach (var e in _magics)
                sb.AppendFormat($" {e}({LezSpellCollection.PosType[e]},{LezSpellCollection.Spells[e].Name})");

            sb.AppendLine();

            sb.Append("_emagics = ");
            foreach (var e in _emagics)
                sb.AppendFormat($" {e}");

            sb.AppendLine();

            sb.AppendFormat($"_lezHits.Count = {_lezHits.Count}");
            sb.AppendLine();
            foreach (var hit in _lezHits)
            {
                sb.Append(hit.PrintHit(_posod, _posma));
                sb.AppendLine();
            }

            sb.AppendFormat($"_lezBlocks.Count = {_lezBlocks.Count}");
            sb.AppendLine();
            foreach (var block in _lezBlocks)
            {
                sb.Append(block.PrintBlock(_posod, _posma));
                sb.AppendLine();
            }

            sb.AppendFormat($"_lezMagics.Count = {_lezMagics.Count}");
            sb.AppendLine();
            foreach (var magic in _lezMagics)
            {
                sb.Append(magic.PrintMagic(_posod, _posma));
                sb.AppendLine();
            }

            sb.AppendFormat($"_lezCombinations.Count = {LezCombinations.Count}");
            sb.AppendLine();

            var index = Helpers.Dice.Make(LezCombinations.Count);
            sb.Append(LezCombinations[index].PrintCombination(_posod, _posma));

            File.AppendAllText(_fileLog, sb.ToString());
            */
        }

        private bool Parse(string html)
        {
            _html = html;
            AppVars.FightLink = string.Empty;

            _fightty = ParseString(html, @"var fight_ty = [", 0);
            if (_fightty == null)
                return false;

            if (_fightty.Length <= 8)
                return false;

            LogBoi = Strip(_fightty[8]);
            //if (LogBoi.Length > 0)
            //    _fileLog = Path.Combine(Application.StartupPath, string.Format($"b{LogBoi}.txt"));

            int.TryParse(_fightty[2], out _ftype);

            if (!LogBoi.Equals(AppVars.LastBoiLog, StringComparison.Ordinal))
                ParseFightLog(_html, LogBoi, _fightty[2]);

            IsBoi = (_fightty[3].Length >= 1) && (_fightty[3][0] == '1');

            var paramow = ParseString(html, @"var param_ow = [", 0);
            if (paramow == null)
                return false;

            double d;
            if (!double.TryParse(Strip(paramow[1]), NumberStyles.Any, CultureInfo.InvariantCulture, out d))
                return false;

            _currentHp = (int)d;
            if (_currentHp < 0)
                _currentHp = 0;

            if (!double.TryParse(Strip(paramow[2]), NumberStyles.Any, CultureInfo.InvariantCulture, out d))
                return false;

            _maxHp = (int)d;
            if (_maxHp < 0)
                _maxHp = 0;

            if (!double.TryParse(Strip(paramow[3]), NumberStyles.Any, CultureInfo.InvariantCulture, out d))
                return false;

            _currentMa = (int)d;
            if (_currentMa < 0)
                _currentMa = 0;

            if (!double.TryParse(Strip(paramow[4]), NumberStyles.Any, CultureInfo.InvariantCulture, out d))
                return false;

            _maxMa = (int)d;
            if (_maxMa < 0)
                _maxMa = 0;

            _percentHp = _maxHp > 0 ? (int)((_currentHp * 100.0) / _maxHp) : 0;
            _percentMa = _maxMa > 0 ? (int)((_currentMa * 100.0) / _maxMa) : 0;

            // Проверка на разделку
            ShowRazdMessage();

            var logsStr = HelperStrings.SubString(_html, "var logs = ", ";");
            if (!string.IsNullOrEmpty(logsStr))
            {
                var jnick = string.Format($"\"{AppVars.Profile.UserNick}\",");
                const string p1A = "\"Свиток Удар Ярости\",";
                const string p1B = "ударом ярости\",";
                const string p2A = "\"Снежок\",";
                const string p2B = "снежком\",";

                var jlogs = JObject.Parse("{\"j\":" + logsStr + "}");
                foreach (var js in jlogs.First)
                {
                    foreach (var je in js)
                    {
                        if (je.Type != JTokenType.Array)
                            continue;

                        // [[0,"17:06"],[1,2,"Умник",16,0,"n"],[7,"Свиток Удар Ярости",0],
                        // [[0,"17:11"],[1,2,"Умник",16,0,"n"],[7,"Снежок",0],

                        var sje = je.ToString();
                        if (sje.IndexOf(jnick, StringComparison.CurrentCultureIgnoreCase) == -1)
                            continue;

                        if (sje.IndexOf(p1A, StringComparison.CurrentCultureIgnoreCase) != -1 &&
                            sje.IndexOf(p1B, StringComparison.CurrentCultureIgnoreCase) != -1
                            )
                        {
                            _hitByScroll = true;
                            break;
                        }

                        if (sje.IndexOf(p2A, StringComparison.CurrentCultureIgnoreCase) != -1 &&
                            sje.IndexOf(p2B, StringComparison.CurrentCultureIgnoreCase) != -1
                            )
                        {
                            _hitByScroll = true;
                            break;
                        }
                    }
                }
            }

            if (!IsBoi)
                return ParseNonFight();

            // мы уже ударили свитком?
            if (_hitByScroll)
            {
                _hitByScroll = false;

                try
                {
                    if (AppVars.MainForm != null)
                    {
                        AppVars.MainForm.BeginInvoke(
                            new UpdateFuryOffDelegate(AppVars.MainForm.FuryOff),
                            new object[] { });
                    }
                }
                catch (InvalidOperationException)
                {
                }

                try
                {
                    if (AppVars.MainForm != null)
                    {
                        AppVars.MainForm.BeginInvoke(
                            new UpdateAutoboiOffDelegate(AppVars.MainForm.AutoboiOff),
                            new object[] { });
                    }
                }
                catch (InvalidOperationException)
                {
                }

                if (AppVars.MainForm != null)
                {
                    AppVars.MainForm.WriteChatMsgSafe("<b>Режим свитка осады</b> отключен.");
                }

                // Ждем, когда автобой отключится
                Thread.Sleep(500);
            }

            var standin = ParseString(html, @"var stand_in = [", 0);
            if (standin == null)
                return false;

            var magicin = ParseString(html, @"var magic_in = [", 0);
            if (magicin == null)
                return false;

            var paramen = ParseString(html, @"var param_en = [", 0);
            if (paramen == null)
                return false;

            var slotsen = ParseString(html, @"var slots_en = [", 0);
            if (slotsen == null)
                return false;

            var fightpm = ParseString(html, @"var fight_pm = [", 0);
            if (fightpm == null)
                return false;

            var alchemy = ParseString(html, @"var alchemy = [", 0);

            FoeName = paramen[0].Substring(1, paramen[0].Length - 2);
            if (string.IsNullOrEmpty(FoeName))
                FoeName = paramen[0].Substring(1, paramen[0].Length - 2);

            _foeName = FoeName;
            if (
                (paramen[5].Length < 3) ||
                (!int.TryParse(Strip(paramen[5]), out _foeLevel)))
                _foeLevel = 33;

            _foeImage = Strip(slotsen[0]);
            if (!_foeImage.StartsWith("bot", StringComparison.OrdinalIgnoreCase) &&
                !_foeImage.StartsWith("_xneto", StringComparison.OrdinalIgnoreCase) &&
                !_foeImage.StartsWith("_xsilf", StringComparison.OrdinalIgnoreCase))
                _foeName = "Человек";

            _foeGroupId = 0;
            for (var i = 0; i < AppVars.Profile.LezGroups.Count; i++)
            {
                var group = AppVars.Profile.LezGroups[i];
                switch (group.Id)
                {
                    case 001:
                        _foeGroupId = group.Id;
                        break;

                    case 010:
                        if (_foeName.Equals("Человек", StringComparison.CurrentCultureIgnoreCase) && _foeLevel >= group.MinimalLevel)
                            _foeGroupId = group.Id;

                        break;

                    case 020:
                        if (!_foeName.Equals("Человек", StringComparison.CurrentCultureIgnoreCase) && _foeLevel >= group.MinimalLevel)
                            _foeGroupId = group.Id;

                        break;

                    case 021:
                        if (IsBossName(_foeName))
                            _foeGroupId = group.Id;

                        break;

                    default:
                        if (_foeName.Equals(LezBotsClassCollection.GetClass(group.Id).Name, StringComparison.CurrentCultureIgnoreCase) && _foeLevel >= group.MinimalLevel)
                            _foeGroupId = group.Id;

                        break;
                }

                if (_foeGroupId != 0)
                {
                    FoeGroup = (LezBotsGroup)group.Clone();
                    break;
                }
            }

            if (!int.TryParse(fightpm[0], out _magmax))
                return false;

            if (!int.TryParse(fightpm[1], out _odmax))
                return false;

            if (!int.TryParse(fightpm[2], out _hitval))
                return false;

            _posod = new int[LezSpellCollection.Od.Length];
            Array.Copy(LezSpellCollection.Od, _posod, LezSpellCollection.Od.Length);
            _posod[0] = _hitval;
            _posod[1] = _hitval + 20;

            _posma = new int[LezSpellCollection.PosMana.Length];
            Array.Copy(LezSpellCollection.PosMana, _posma, LezSpellCollection.PosMana.Length);
            _posma[2] = FoeGroup.MagHits;
            _posma[3] = FoeGroup.MagHits;

            var lstandin = new List<int> {0, 1};
            foreach (var e in standin)
            {
                int p;
                if (int.TryParse(e, out p))
                    lstandin.Add(p);
            }

            Selpl(0, lstandin);

            var lmagicin = new List<int>();
            foreach (var e in magicin)
            {
                int p;
                if (int.TryParse(e, out p))
                    lmagicin.Add(p);
            }

            if (lmagicin.Count > 0)
                Selpl(1, lmagicin);

            switch (fightpm[3])
            {
                case "0":
                    _bs = 0;
                    break;

                case "40":
                    _bs = 1;
                    break;

                case "70":
                    _bs = 2;
                    break;

                case "90":
                    _bs = 3;
                    break;

                default:
                    _bs = 0;
                    break;
            }

            string[] tshowbl = { "4:5:6@7:8:9@10:11@12:13", "14:15@16:17@18:19@20:21", "22:23@24@25@26", "27@28" };
            _bspar = tshowbl[_bs].Split('@');
            for (var ee = 0; ee < 4; ee++)
            {
                if (ee >= _bspar.Length)
                    break;

                var blks = _bspar[ee].Split(':');
                var phblocks = new List<int>();
                for (var i = 0; i < blks.Length; i++)
                {
                    var val = Convert.ToInt32(blks[i]);
                    phblocks.Add(val);
                }

                _blocks[ee].AddRange(phblocks);
                _blocks[ee].AddRange(_magblocks);
                for (var i = 0; i < _blocks[ee].Count; i++)
                {
                    _eblocks[ee].Add(IsBlockAllowed(_blocks[ee][i]));
                }
            }

            for (var i = 0; i < _hits.Count; i++)
            {
                _ehits.Add(IsHitAllowed(_hits[i]));
            }

            for (var combo = 0; combo < 4; combo++)
            {
                for (var op = 1; op <= _hits.Count; op++)
                {                    
                    if (!_ehits[op - 1])
                        continue;

                    var hit = new LezNode();
                    var code = _hits[op - 1];
                    hit.AddHit(combo, op, code);
                    if (hit.Od(_posod) > _odmax || hit.Mana(_posma) > _currentMa)
                        continue;

                    _lezHits.Add(hit);
                }
            }

            for (var combo1 = 0; combo1 < 3; combo1++)
            {
                for (var op1 = 1; op1 <= _hits.Count; op1++)
                {                    
                    if (!_ehits[op1 - 1])
                        continue;

                    var hit = new LezNode();
                    var code1 = _hits[op1 - 1];
                    hit.AddHit(combo1, op1, code1);
                    if (hit.Od(_posod) > _odmax || hit.Mana(_posma) > _currentMa)
                        continue;

                    for (var combo2 = combo1 + 1; combo2 < 4; combo2++)
                    {
                        if (combo2 - combo1 == 3)
                            continue;

                        for (var op2 = 1; op2 <= _hits.Count; op2++)
                        {                            
                            if (!_ehits[op2 - 1])
                                continue;

                            var hit2 = (LezNode) hit.Clone();
                            var code2 = _hits[op2 - 1];
                            hit2.AddHit(combo2, op2, code2);
                            if (hit2.Od(_posod) > _odmax || hit2.Mana(_posma) > _currentMa)
                                continue;

                            _lezHits.Add(hit2);
                        }
                    }
                }
            }

            for (var combo1 = 0; combo1 < 2; combo1++)
            {
                for (var op1 = 1; op1 <= _hits.Count; op1++)
                {                   
                    if (!_ehits[op1 - 1])
                        continue;

                    var hit = new LezNode();
                    var code1 = _hits[op1 - 1];
                    hit.AddHit(combo1, op1, code1);
                    if (hit.Od(_posod) > _odmax || hit.Mana(_posma) > _currentMa)
                        continue;

                    var combo2 = combo1 + 1;
                    for (var op2 = 1; op2 <= _hits.Count; op2++)
                    {                        
                        if (!_ehits[op2 - 1])
                            continue;

                        var hit2 = (LezNode) hit.Clone();
                        var code2 = _hits[op2 - 1];
                        hit2.AddHit(combo2, op2, code2);
                        if (hit2.Od(_posod) > _odmax || hit2.Mana(_posma) > _currentMa)
                            continue;

                        var combo3 = combo2 + 1;
                        for (var op3 = 1; op3 <= _hits.Count; op3++)
                        {
                            
                            if (!_ehits[op3 - 1])
                                continue;

                            var hit3 = (LezNode) hit2.Clone();
                            var code3 = _hits[op3 - 1];
                            hit3.AddHit(combo3, op3, code3);
                            if (hit3.Od(_posod) > _odmax || hit3.Mana(_posma) > _currentMa)
                                continue;

                            _lezHits.Add(hit3);
                        }
                    }
                }
            }

            for (var combo = 0; combo < 4; combo++)
            {
                for (var op = 1; op <= _blocks[combo].Count; op++)
                {
                    if (!_eblocks[combo][op - 1])
                        continue;

                    var block = new LezNode();
                    var code = _blocks[combo][op - 1];
                    if (combo > 0 && !LezSpell.IsPhBlock(code))
                        continue;

                    block.AddBlock(combo, op, code);
                    if (block.Od(_posod) > _odmax || block.Mana(_posma) > _currentMa)
                        continue;

                    _lezBlocks.Add(block);
                }
            }

            var magicClickablesCount = MagicClickablesCount();
            if (magicClickablesCount > 0)
            {
                for (var flag = 0; flag < _magics.Count; flag++)
                {
                    if (_emagics[flag])
                    {
                        var code = _magics[flag];
                        var magic = new LezNode();
                        magic.AddMagic(flag, code, ZMag(FoeGroup, code), ZRestore(FoeGroup, code), ZScroll(code));
                        if (magic.Od(_posod) > _odmax || magic.Mana(_posma) > _currentMa)
                            continue;

                        _lezMagics.Add(magic);
                    }
                }
            }
            
            if (magicClickablesCount > 1)
            {
                for (var flag1 = 0; flag1 < _magics.Count - 1; flag1++)
                {
                    if (_emagics[flag1])
                    {
                        var code1 = _magics[flag1];
                        var magic = new LezNode();
                        magic.AddMagic(flag1, code1, ZMag(FoeGroup, code1), ZRestore(FoeGroup, code1), ZScroll(code1));
                        if (magic.Od(_posod) > _odmax || magic.Mana(_posma) > _currentMa)
                            continue;

                        for (var flag2 = flag1 + 1; flag2 < _magics.Count; flag2++)
                        {
                            if (_emagics[flag2])
                            {
                                var code2 = _magics[flag2];
                                if (
                                    (code1 == 388 && Array.IndexOf(FoeGroup.SpellsRestoreHp, code2) >= 0) ||
                                    (code2 == 388 && Array.IndexOf(FoeGroup.SpellsRestoreHp, code1) >= 0)
                                    )
                                    continue;

                                if (
                                    (Array.IndexOf(FoeGroup.SpellsBlocks, code1) >= 0) && (Array.IndexOf(FoeGroup.SpellsBlocks, code2) >= 0)
                                    )
                                    continue;

                                var magic2 = (LezNode) magic.Clone();
                                magic2.AddMagic(flag2, code2, ZMag(FoeGroup, code2), ZRestore(FoeGroup, code2), ZScroll(code2));
                                if (magic2.Od(_posod) > _odmax || magic2.Mana(_posma) > _currentMa)
                                    continue;

                                _lezMagics.Add(magic2);
                            }
                        }
                    }
                }
            }

            if (magicClickablesCount > 2)
            {
                for (var flag1 = 0; flag1 < _magics.Count - 2; flag1++)
                {
                    if (_emagics[flag1])
                    {
                        var code1 = _magics[flag1];
                        var magic = new LezNode();
                        magic.AddMagic(flag1, code1, ZMag(FoeGroup, code1), ZRestore(FoeGroup, code1), ZScroll(code1));
                        if (magic.Od(_posod) > _odmax || magic.Mana(_posma) > _currentMa)
                            continue;

                        for (var flag2 = flag1 + 1; flag2 < _magics.Count - 1; flag2++)
                        {
                            if (_emagics[flag2])
                            {
                                var code2 = _magics[flag2];
                                if (
                                    (code1 == 388 && Array.IndexOf(FoeGroup.SpellsRestoreHp, code2) >= 0) ||
                                    (code2 == 388 && Array.IndexOf(FoeGroup.SpellsRestoreHp, code1) >= 0)
                                    )
                                    continue;

                                if (
                                    (Array.IndexOf(FoeGroup.SpellsBlocks, code1) >= 0) && (Array.IndexOf(FoeGroup.SpellsBlocks, code2) >= 0)
                                    )
                                    continue;


                                var magic2 = (LezNode) magic.Clone();
                                magic2.AddMagic(flag2, code2, ZMag(FoeGroup, code2), ZRestore(FoeGroup, code2), ZScroll(code2));
                                if (magic2.Od(_posod) > _odmax || magic2.Mana(_posma) > _currentMa)
                                    continue;

                                for (var flag3 = flag2 + 1; flag3 < _magics.Count; flag3++)
                                {
                                    if (_emagics[flag3])
                                    {
                                        var code3 = _magics[flag3];
                                        if (
                                            (code1 == 388 && Array.IndexOf(FoeGroup.SpellsRestoreHp, code3) >= 0) ||
                                            (code2 == 388 && Array.IndexOf(FoeGroup.SpellsRestoreHp, code3) >= 0) ||
                                            (code3 == 388 && Array.IndexOf(FoeGroup.SpellsRestoreHp, code1) >= 0) ||
                                            (code3 == 388 && Array.IndexOf(FoeGroup.SpellsRestoreHp, code2) >= 0)
                                            )
                                            continue;

                                        if (
                                            ((Array.IndexOf(FoeGroup.SpellsBlocks, code1) >= 0) && (Array.IndexOf(FoeGroup.SpellsBlocks, code3) >= 0)) ||
                                            ((Array.IndexOf(FoeGroup.SpellsBlocks, code2) >= 0) && (Array.IndexOf(FoeGroup.SpellsBlocks, code3) >= 0))
                                            )
                                            continue;

                                        var magic3 = (LezNode) magic2.Clone();
                                        magic3.AddMagic(flag3, code3, ZMag(FoeGroup, code3), ZRestore(FoeGroup, code3), ZScroll(code3));
                                        if (magic3.Od(_posod) > _odmax || magic3.Mana(_posma) > _currentMa)
                                            continue;

                                        _lezMagics.Add(magic3);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            
            for (var ihits = 0; ihits < _lezHits.Count; ihits++)
            {
                var combination = new LezNode();
                combination.AddCombination(_lezHits[ihits]);
                for (var iblocks = 0; iblocks < _lezBlocks.Count; iblocks++)
                {
                    var hasNonPhBlock2 = _lezBlocks[iblocks].HasNonPhBlock(FoeGroup);

                    var combination2 = (LezNode) combination.Clone();
                    combination2.AddCombination(_lezBlocks[iblocks]);
                    if (combination2.Od(_posod) > _odmax || combination2.Mana(_posma) > _currentMa)
                        continue;                    

                    for (var imagic = 0; imagic < _lezMagics.Count; imagic++)
                    {
                        if (hasNonPhBlock2)
                        {
                            var hasNonPhBlock3 = _lezMagics[imagic].HasNonPhBlock(FoeGroup);
                            if (hasNonPhBlock3)
                                continue;
                        }

                        var combination3 = (LezNode)combination2.Clone();
                        combination3.AddCombination(_lezMagics[imagic]);
                        if (combination3.Od(_posod) > _odmax || combination3.Mana(_posma) > _currentMa)
                            continue;

                        if (!combination3.IsValid())
                            continue;                        

                        if (LezCombinations.Count == 0)
                            LezCombinations.Add(combination3);
                        else
                        {
                            var compare = combination3.CompareTo(LezCombinations[0]);
                            if (compare < 0)
                                continue;

                            if (compare > 0)
                                LezCombinations.Clear();

                            LezCombinations.Add(combination3);
                        }
                    }
                }
            }

            DoStop = FoeGroup.DoStopNow;
            IsLowHp = FoeGroup.DoStopLowHp && (_percentHp <= FoeGroup.StopLowHp);
            IsLowMa = FoeGroup.DoStopLowMa && (_percentMa <= FoeGroup.StopLowMa);
            DoExit = FoeGroup.DoExitRisky && _ftype >= 80 && _foeName.Equals("Человек");
            
            if (DoStop || IsLowHp || IsLowMa || DoExit)
            {
                if (UnderAttack.IsHuman && UnderAttack.IsMe)
                {
                    DoStop = false;
                    IsLowHp = false;
                    IsLowMa = false;
                    DoExit = false;
                }
            }

            if (LezCombinations.Count > 0)
            {
                var index = Helpers.Dice.Make(LezCombinations.Count);
                LezCombination = LezCombinations[index];

                // Удары

                var inputu = new StringBuilder();
                for (var i = 0; i < 4; i++)
                {
                    if (LezCombination.HitOps[i] > 0)
                    {
                        var code = LezCombination.HitCodes[i];

                        inputu.Append(i);
                        inputu.Append('_');
                        inputu.Append(code);
                        inputu.Append('_');
                        inputu.Append(_posma[code]); // mbu...
                        inputu.Append('@');
                    }
                }

                // Блоки

                var inputb = new StringBuilder();
                if (LezCombination.BlockOp > 0)
                {
                    inputb.Append(LezCombination.BlockCombo);
                    inputb.Append('_');
                    inputb.Append(LezCombination.BlockCode);
                    inputb.Append('_');
                    inputb.Append(_posma[LezCombination.BlockCode]); // mbb...
                    inputb.Append('@');
                }

                // Магия

                var inputa = new StringBuilder();
                for (var i = 0; i < LezCombination.MagicFlags.Length; i++)
                {
                    if (LezCombination.MagicFlags[i])
                    {
                        var code = LezCombination.MagicCodes[i];
                        var posType = LezSpellCollection.PosType[code];
                        if (posType > 2)
                        {
                            inputa.Append(code);
                            if (posType > 3)
                            {
                                inputa.Append('_');
                                inputa.Append(alchemy[i]);
                            }

                            inputa.Append('@');
                        }
                    }
                }

                // Определение противника
                var nameprot = paramen[0].Substring(1, paramen[0].Length - 2);
                int levelprot;
                if (
                    (paramen[5].Length < 3) ||
                    (!int.TryParse(Strip(paramen[5]), out levelprot)))
                {
                    levelprot = -1;
                }

                // Сообщение для всплывающей подсказки

                if (AppVars.Profile.ShowTrayBaloons)
                {
                    var sbm = new StringBuilder();
                    if (levelprot == -1)
                    {
                        sbm.Append("Невидимка");
                    }
                    else
                    {
                        sbm.Append(Strip(paramen[0]));
                        sbm.Append(" [");
                        sbm.Append(Strip(paramen[5]));
                        sbm.Append("] [");
                        sbm.Append(Strip(paramen[1]));
                        sbm.Append('/');
                        sbm.Append(Strip(paramen[2]));
                        sbm.Append(" | ");
                        sbm.Append(Strip(paramen[3]));
                        sbm.Append('/');
                        sbm.Append(Strip(paramen[4]));
                        sbm.Append(']');
                    }

                    try
                    {
                        if (AppVars.MainForm != null)
                        {
                            AppVars.MainForm.BeginInvoke(
                                new UpdateTrayBaloonDelegate(AppVars.MainForm.UpdateTrayBaloon), sbm.ToString());
                        }
                    }
                    catch (InvalidOperationException)
                    {
                    }
                }

                // Построение Result

                var res = new StringBuilder();
                var vcode = Strip(fightpm[4]);
                var levbot = Strip(paramen[5]);

                res.Append(vcode);
                res.Append('|');
                res.Append(fightpm[5]);
                res.Append('|');
                res.Append(fightpm[6]);
                res.Append('|');
                res.Append(fightpm[7]);
                res.Append('|');
                res.Append(levbot);
                res.Append('|');
                res.Append(_fightty[2]);
                res.Append('|');
                res.Append(inputu);
                res.Append('|');
                res.Append(inputb);
                res.Append('|');
                res.Append(inputa);
                Result = res.ToString();

                // Построение Frame

                var sb = new StringBuilder();
                sb.Append(HelperErrors.Head());
                sb.Append("<b>");
                sb.Append(paramow[0].Substring(1, paramow[0].Length - 2));
                sb.Append("</b> [");
                sb.Append(paramow[5].Substring(1, paramow[5].Length - 2));
                sb.Append("] [<font color=#bb0000><b>");
                sb.Append(paramow[1].Substring(1, paramow[1].Length - 2));
                sb.Append("</b>/<b>");
                sb.Append(paramow[2].Substring(1, paramow[2].Length - 2));
                sb.Append("</b></font> | <font color=#336699><b>");
                sb.Append(paramow[3].Substring(1, paramow[3].Length - 2));
                sb.Append("</b>/<b>");
                sb.Append(paramow[4].Substring(1, paramow[4].Length - 2));
                sb.Append("</b></font>] : <b>");
                if (levelprot == -1)
                {
                    sb.Append("Невидимка</b>");
                }
                else
                {
                    sb.Append(nameprot);
                    sb.Append("</b> [");
                    sb.Append(levelprot);
                    sb.Append("] [<font color=#bb0000><b>");
                    sb.Append(Strip(paramen[1]));
                    sb.Append("</b>/<b>");
                    sb.Append(Strip(paramen[2]));
                    sb.Append("</b></font> | <font color=#336699><b>");
                    sb.Append(Strip(paramen[3]));
                    sb.Append("</b>/<b>");
                    sb.Append(Strip(paramen[4]));
                    sb.Append("</b></font>]");
                }

                // var form_node = d.getElementById('form_main');
                sb.Append(@"<form action=""main.php"" method=POST name=ff id=form_main>");

                // form_node.appendChild(AddElement('post_id','7'));
                sb.Append(@"<input name=post_id type=hidden value=""");
                sb.Append(7);
                sb.Append(@""">");

                // form_node.appendChild(AddElement('vcode',fight_pm[4]));
                sb.Append(@"<input name=vcode type=hidden value=""");
                sb.Append(vcode);
                sb.Append(@""">");

                // form_node.appendChild(AddElement('enemy',fight_pm[5]));
                sb.Append(@"<input name=enemy type=hidden value=""");
                sb.Append(fightpm[5]);
                sb.Append(@""">");

                // form_node.appendChild(AddElement('group',fight_pm[6]));
                sb.Append(@"<input name=group type=hidden value=""");
                sb.Append(fightpm[6]);
                sb.Append(@""">");

                // form_node.appendChild(AddElement('inf_bot',fight_pm[7]));
                sb.Append(@"<input name=inf_bot type=hidden value=""");
                sb.Append(fightpm[7]);
                sb.Append(@""">");

                // form_node.appendChild(AddElement('inf_zb',fight_pm[10]));
                sb.Append(@"<input name=inf_zb type=hidden value=""");
                sb.Append(fightpm[10]);
                sb.Append(@""">");

                // form_node.appendChild(AddElement('lev_bot',param_en[5]));
                sb.Append(@"<input name=lev_bot type=hidden value=""");
                sb.Append(levbot);
                sb.Append(@""">");

                // form_node.appendChild(AddElement('ftr',fight_ty[2]));
                sb.Append(@"<input name=ftr type=hidden value=""");
                sb.Append(_fightty[2]);
                sb.Append(@""">");

                // form_node.appendChild(AddElement('inu',input_u));
                sb.Append(@"<input name=inu type=hidden value=""");
                sb.Append(inputu);
                sb.Append(@""">");

                // form_node.appendChild(AddElement('inb',input_b));
                sb.Append(@"<input name=inb type=hidden value=""");
                sb.Append(inputb);
                sb.Append(@""">");

                // form_node.appendChild(AddElement('ina',input_a));
                sb.Append(@"<input name=ina type=hidden value=""");
                sb.Append(inputa);
                sb.Append(@""">");

                sb.Append(@"</form>" +
                          @"<script language=""JavaScript"">" +
                          @"document.ff.submit();" +
                          @"</script></body></html>");

                Frame = sb.ToString();
            }
            else
            {
                if (AppVars.Profile.LezDoAutoboi)
                {
                    try
                    {
                        if (AppVars.MainForm != null)
                        {
                            AppVars.MainForm.BeginInvoke(
                                new UpdateWriteChatMsgDelegate(AppVars.MainForm.WriteChatMsg),
                                "Настройки автобоя противоречивы. Автобой остановлен.");
                        }
                    }
                    catch (InvalidOperationException)
                    {
                    }

                    try
                    {
                        if (AppVars.MainForm != null)
                        {
                            AppVars.MainForm.BeginInvoke(
                                new UpdateAutoboiOffDelegate(AppVars.MainForm.AutoboiOff),
                                new object[] {});
                        }
                    }
                    catch (InvalidOperationException)
                    {
                    }

                    // Ждем, когда автобой отключится
                    Thread.Sleep(500);
                }

                return true;
            }

            return true;
        }

        private void Selpl(int mode, IEnumerable<int> input)
        {
            foreach (var e in input)
            {
                var posType = LezSpellCollection.PosType[e];
                if (posType == 1)
                {
                    _hits.Add(e);
                    if (mode == 1)
                    {
                        _magics.Add(e);
                        _emagics.Add(false);
                    }
                }
                else
                {
                    if (posType == 2)
                    {
                        _magblocks.Add(e);
                        if (mode == 1)
                        {
                            _magics.Add(e);
                            _emagics.Add(false);
                        }
                    }
                    else
                    {
                        if (posType == 3 || posType == 4)
                        {
                            _magics.Add(e);
                            _emagics.Add(IsMagicAllowed(e));
                        }
                    }
                }
            }
        }

        private int MagicClickablesCount()
        {
            var count = 0;
            foreach (var c in _emagics)
            {
                if (c)
                    count++;
            }

            return count;
        }

        private bool IsHitAllowed(int code)
        {
            if (LezSpell.IsPhHit(code) && FoeGroup.DoHits)
                return true;

            if (LezSpell.IsMagHit(code) && FoeGroup.DoMagHits)
                return true;

            if (Array.IndexOf(FoeGroup.SpellsHits, code) >= 0 && FoeGroup.DoAbilHits)
                return true;

            return false;
        }

        private bool IsBlockAllowed(int code)
        {
            if (LezSpell.IsPhBlock(code) && FoeGroup.DoBlocks)
                return true;

            if (LezSpell.IsMagBlock(code) && FoeGroup.DoMagBlocks)
                return true;

            if (Array.IndexOf(FoeGroup.SpellsBlocks, code) >= 0 && FoeGroup.DoAbilBlocks)
                return true;

            return false;
        }

        private bool IsMagicAllowed(int code)
        {
            if (Array.IndexOf(FoeGroup.SpellsRestoreHp, code) >= 0)
            {
                if (FoeGroup.DoRestoreHp)
                {
                    var php = (int) (_currentHp*100.0/_maxHp);
                    if (php <= FoeGroup.RestoreHp)
                        return true;
                }

                return false;
            }

            if (Array.IndexOf(FoeGroup.SpellsRestoreMa, code) >= 0)
            {
                if (FoeGroup.DoRestoreMa)
                {
                    var php = (int)(_currentMa * 100.0 / _maxMa);
                    if (php <= FoeGroup.RestoreMa)
                        return true;
                }

                return false;
            }

            if (Array.IndexOf(FoeGroup.SpellsBlocks, code) >= 0)
            {
                if (FoeGroup.DoAbilBlocks)
                    return true;

                return false;
            }

            if (Array.IndexOf(FoeGroup.SpellsHits, code) >= 0)
            {
                if (FoeGroup.DoAbilHits)
                    return true;

                return false;
            }

            if (Array.IndexOf(FoeGroup.SpellsMisc, code) >= 0)
            {
                if (FoeGroup.DoMiscAbils)
                    return true;

                return false;
            }

            if (LezSpell.IsScrollHit(code))
            {
                if (AppVars.DoFury && IsBossName(_foeName))
                    return true;

                return false;
            }

            if (code == 328) // "Зелье Ярость Берсерка"
            {
                if (IsBossInLog())
                    return true;

                return false;
            }

            return false;
        }
        
        private static int ZMag(LezBotsGroup group, int code)
        {
            if (Array.IndexOf(group.SpellsBlocks, code) >= 0)
                return 4;

            if (Array.IndexOf(group.SpellsHits, code) >= 0)
                return 2;

            if (Array.IndexOf(group.SpellsMisc, code) >= 0)
                return 1;

            return 0;
        }

        private static int ZRestore(LezBotsGroup group, int code)
        {
            if (code == 388) // "Исцеление"
                return 3;

            if (Array.IndexOf(group.SpellsRestoreHp, code) >= 0)
                return 2;

            if (Array.IndexOf(group.SpellsRestoreMa, code) >= 0)
                return 1;

            return 0;
        }

        private static int ZScroll(int code)
        {
            if (code == 328) // "Зелье Ярость Берсерка"
                return 3;

            if (code == 338) // "Снежок"
                return 2;

            if (code == 277) // "Свиток Удар Ярости"
                return 1;

            return 0;
        }

        private static bool IsBossName(string name)
        {
            return (
                name.Equals("Королева Змей", StringComparison.CurrentCultureIgnoreCase) ||
                name.Equals("Хранитель Леса", StringComparison.CurrentCultureIgnoreCase) ||
                name.Equals("Громлех Синезубый", StringComparison.CurrentCultureIgnoreCase) ||
                name.Equals("Выползень", StringComparison.CurrentCultureIgnoreCase)
                );
        }

        private bool IsBossInLog()
        {
            return (
                _html.IndexOf("\"Королева Змей\"", StringComparison.CurrentCultureIgnoreCase) != -1 ||
                _html.IndexOf("\"Хранитель Леса\"", StringComparison.CurrentCultureIgnoreCase) != -1 ||
                _html.IndexOf("\"Громлех Синезубый\"", StringComparison.CurrentCultureIgnoreCase) != -1 ||
                _html.IndexOf("\"Выползень\"", StringComparison.CurrentCultureIgnoreCase) != -1
                );
        }

        private bool ParseNonFight()
        {
            switch (_fightty[4])
            {
                case "2":
                    _fexp = ParseString(_html, @"var fexp = [", 0);
                    if (_fexp == null)
                        return false;

                    if (_fexp[4].Length > 2)
                    {
                        // Завершение боя с капчей
                        if (_fexp[6].Equals("0", StringComparison.Ordinal))
                        {
                            AppVars.CodeAddress =
                                "http://www.neverlands.ru/modules/code/code.php?" +
                                Strip(_fexp[4]);

                            AppVars.FightLink =
                                "http://www.neverlands.ru/main.php?code=????&get_id=61&act=7&fexp=" +
                                Strip(_fexp[0]) +
                                "&fres=" + Strip(_fexp[1]) +
                                "&vcode=" + Strip(_fexp[3]) +
                                "&min1=" + Strip(_fexp[8]) +
                                "&max1=" + Strip(_fexp[9]) +
                                "&min2=" + Strip(_fexp[10]) +
                                "&max2=" + Strip(_fexp[11]) +
                                "&sum1=" + Strip(_fexp[12]) +
                                "&sum2=" + Strip(_fexp[13]) +
                                "&ftype=" + Strip(_fexp[5]);

                            if (!AppVars.Profile.DoGuamod)
                            {
                                if (AppVars.MainForm != null && AppVars.MainForm.TrayIsDigitsWaitTooLong())
                                {
                                    try
                                    {
                                        if (AppVars.MainForm != null)
                                        {
                                            AppVars.MainForm.BeginInvoke(
                                                new UpdateGuamodTurnOnDelegate(
                                                    AppVars.MainForm.UpdateGuamodTurnOn),
                                                new object[] { });
                                        }
                                    }
                                    catch (InvalidOperationException)
                                    {
                                    }
                                }
                                else
                                {
                                    MySounds.EventSounds.PlayDigits();
                                    try
                                    {
                                        if (AppVars.MainForm != null)
                                        {
                                            AppVars.MainForm.BeginInvoke(
                                                new UpdateTrayFlashDelegate(AppVars.MainForm.UpdateTrayFlash), "Ввод цифр");
                                        }
                                    }
                                    catch (InvalidOperationException)
                                    {
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        // Завершение боя без капчи
                        if (AppVars.Profile.ShowTrayBaloons)
                        {
                            try
                            {
                                if (AppVars.MainForm != null)
                                    AppVars.MainForm.BeginInvoke(
                                        new UpdateTrayBaloonDelegate(AppVars.MainForm.UpdateTrayBaloon), "Завершаем бой");
                            }
                            catch (InvalidOperationException)
                            {
                            }
                        }

                        AppVars.FightLink =
                            "http://www.neverlands.ru/main.php?get_id=61&act=7&fexp=" +
                            Strip(_fexp[0]) +
                            "&fres=" +
                            Strip(_fexp[1]) +
                            "&vcode=" +
                            Strip(_fexp[3]) +
                            "&ftype=" +
                            Strip(_fexp[5]) +
                            "&min1=" +
                            Strip(_fexp[8]) +
                            "&max1=" +
                            Strip(_fexp[9]) +
                            "&min2=" +
                            Strip(_fexp[10]) +
                            "&max2=" +
                            Strip(_fexp[11]) +
                            "&sum1=" +
                            Strip(_fexp[12]) +
                            "&sum2=" +
                            Strip(_fexp[13]);
                    }

                    break;
                case "3":
                    if (_fightty[6].Length > 2)
                    {
                        if (AppVars.Profile.LezDoWinTimeout)
                        {
                            // Победа по таймауту

                            if (AppVars.Profile.ShowTrayBaloons)
                            {
                                try
                                {
                                    if (AppVars.MainForm != null)
                                    {
                                        AppVars.MainForm.BeginInvoke(
                                            new UpdateTrayBaloonDelegate(AppVars.MainForm.UpdateTrayBaloon), "Победа по таймауту");
                                    }
                                }
                                catch (InvalidOperationException)
                                {
                                }
                            }

                            AppVars.FightLink =
                                "http://www.neverlands.ru/main.php?get_id=61&act=6&mode=1&gr=" +
                                Strip(_fightty[7]) +
                                "&vcode=" +
                                Strip(_fightty[6]);
                        }
                        else
                        {
                            // Ничья по таймауту

                            if (AppVars.Profile.ShowTrayBaloons)
                            {
                                try
                                {
                                    if (AppVars.MainForm != null)
                                    {
                                        AppVars.MainForm.BeginInvoke(
                                            new UpdateTrayBaloonDelegate(AppVars.MainForm.UpdateTrayBaloon), "Ничья по таймауту");
                                    }
                                }
                                catch (InvalidOperationException)
                                {
                                }
                            }

                            AppVars.FightLink =
                                "http://www.neverlands.ru/main.php?get_id=61&act=6&mode=0&gr=" +
                                Strip(_fightty[7]) +
                                "&vcode=" +
                                Strip(_fightty[6]);
                        }
                    }
                    else
                    {
                        IsWaitingForNextTurn = true;
                        AppVars.AccountError = string.Empty;

                        if (AppVars.AutoRefresh)
                        {
                            if (AppVars.MainForm != null)
                                AppVars.MainForm.WaitForTurnSafe();

                            var sb = new StringBuilder(HelperErrors.Head());
                            sb.AppendLine("Ожидаем хода противника... <span id='wt'>00:00:00</span>");
                            sb.AppendLine(@"<script language=""JavaScript"">");
                            sb.AppendLine(@"var block = document.getElementById('wt');");
                            sb.AppendLine(@"wtimer(0, block);");
                            sb.AppendLine(@"function wtimer(sec, block){");
                            sb.AppendLine(@"  var time = sec;");
                            sb.AppendLine(@"  var minutes = parseInt(time / 60);");
                            sb.AppendLine(@"  if (minutes < 1) minutes = 0;");
                            sb.AppendLine(@"  time = parseInt(time - minutes * 60);");
                            sb.AppendLine(@"  if (minutes < 10) minutes = '0' + minutes;");
                            sb.AppendLine(@"  var seconds = time;");
                            sb.AppendLine(@"  if (seconds < 10) seconds = '0' + seconds;");
                            sb.AppendLine(@"  block.innerHTML = minutes + ':' + seconds;");
                            sb.AppendLine(@"  sec++;");
                            sb.AppendLine(@"  setTimeout(function(){ wtimer(sec, block); }, 1000);");
                            sb.AppendLine(@"}");
                            sb.Append(@"</script></body></html>");
                            Frame = sb.ToString();
                        }
                    }

                    break;
                case "4":
                    if (AppVars.Profile.ShowTrayBaloons)
                    {
                        try
                        {
                            if (AppVars.MainForm != null)
                            {
                                AppVars.MainForm.BeginInvoke(
                                    new UpdateTrayBaloonDelegate(AppVars.MainForm.UpdateTrayBaloon), "Ждем окончания боя");
                            }
                        }
                        catch (InvalidOperationException)
                        {
                        }
                    }

                    break;
                case "5":
                    if (AppVars.Profile.ShowTrayBaloons)
                    {
                        try
                        {
                            if (AppVars.MainForm != null)
                            {
                                AppVars.MainForm.BeginInvoke(
                                    new UpdateTrayBaloonDelegate(AppVars.MainForm.UpdateTrayBaloon), "Завершить");
                            }
                        }
                        catch (InvalidOperationException)
                        {
                        }
                    }

                    AppVars.FightLink = "http://www.neverlands.ru/main.php?get_id=61&act=5&vcode=" +
                                        Strip(_fightty[5]);

                    break;

                case "7":
                    if (AppVars.Profile.ShowTrayBaloons)
                    {
                        try
                        {
                            if (AppVars.MainForm != null)
                            {
                                AppVars.MainForm.BeginInvoke(
                                    new UpdateTrayBaloonDelegate(AppVars.MainForm.UpdateTrayBaloon), "Завершить");
                            }
                        }
                        catch (InvalidOperationException)
                        {
                        }
                    }

                    AppVars.FightLink = "http://www.neverlands.ru/main.php?get_id=61&act=5&st=" + 
                        Strip(_fightty[4]) +
                        "&vcode=" + 
                        Strip(_fightty[5]);
                    break;
            }

            if (!LogBoi.Equals(AppVars.LastBoiEndLog, StringComparison.Ordinal))
            {
                var list = ParseString(_html, @"var list = [[", 0);

                if (list != null)
                {
                    var uron = 0;
                    for (var u = 6; u <= 10; u++)
                    {
                        uron += Convert.ToInt32(list[u]);
                    }

                    AppVars.LastBoiUron = uron.ToString(CultureInfo.InvariantCulture);
                    AppVars.LastBoiEndLog = LogBoi;
                }
            }


            return true;
        }

        private static string[] ParseString(string html, string sarg, int mina)
        {
            if (html == null || sarg == null)
                return null;

            if (html.IndexOf(sarg, StringComparison.OrdinalIgnoreCase) == -1)
                return null;

            var args = HelperStrings.SubString(html, sarg, "]");
            if (args == null)
                return null;

            var pars = args.Split(',');
            return pars.Length < mina ? null : pars;
        }

        private static string Strip(string arg)
        {
            return arg.Trim('"');
        }

        private static void ParseFightLog(string htmlContent, string fightty8, string fighttty2)
        {
            AppVars.LastBoiLog = fightty8;
            AppVars.LastBoiSostav = string.Empty;
            AppVars.LastBoiTimer = DateTime.Now;
            AppVars.LastBoiTravm = string.Empty;
            var ftmppic = string.Empty;
            var ftmp = string.Empty;
            switch (fighttty2)
            {
                case "10":
                    ftmppic = "4";
                    ftmp = "низкий";
                    break;
                case "30":
                    ftmppic = "3";
                    ftmp = "средний";
                    break;
                case "50":
                    ftmppic = "2";
                    ftmp = "высокий";
                    break;
                case "80":
                case "100":
                    ftmppic = "1";
                    ftmp = "оч. высокий";
                    break;
                case "110":
                    ftmppic = "0";
                    ftmp = "травма";
                    break;
            }

            if (!string.IsNullOrEmpty(ftmppic))
            {
                AppVars.LastBoiTravm =
                    "<img src=http://image.neverlands.ru/gameplay/injury" +
                    ftmppic +
                    @".gif alt=""% травматичности: " +
                    ftmp +
                    @""" width=17 height=17 align=absmiddle>";
            }

            var log1 = HelperStrings.SubString(htmlContent, "var logs = ", ";");
            if (log1 == null)
            {
                return;
            }

            var slog1 = log1.Split(new[] { @"""Бой между""", @""" начался" }, StringSplitOptions.None);
            if (slog1.Length != 3)
            {
                return;
            }

            var slog2 = slog1[1].Split(new[] { @""" и""" }, StringSplitOptions.None);
            if (slog2.Length != 2)
            {
                return;
            }

            var i = 0;
            var sbs = new StringBuilder();

            otherteam:
            sbs.Length = 0;

            int poss;
            var pose = -1;
            do
            {
                poss = slog2[i].IndexOf('[', pose + 1);
                if (poss == -1)
                {
                    continue;
                }

                pose = slog2[i].IndexOf(']', poss + 1);
                if (pose == -1)
                {
                    break;
                }

                var log2 = slog2[i].Substring(poss + 1, pose - poss - 1);
                var slog3 = log2.Split(new[] { ',' }, StringSplitOptions.None);
                string sf;
                var sfl = string.Empty;
                if (slog3.Length == 6)
                {
                    sfl = slog3[2].Substring(1, slog3[2].Length - 2);
                    sf = sfl + "[" + slog3[3] + "]";
                }
                else
                {
                    sf = "невидимки";
                }

                if (sfl.Equals(AppVars.Profile.UserNick, StringComparison.OrdinalIgnoreCase))
                {
                    i = 1;
                    goto otherteam;
                }

                if (sbs.Length != 0)
                {
                    sbs.Append(", ");
                }

                sbs.Append(sf);
            }
            while (poss != -1);
            AppVars.LastBoiSostav = sbs.ToString();
            try
            {
                if (AppVars.MainForm != null)
                {
                    AppVars.MainForm.BeginInvoke(
                        new UpdateTexLogDelegate(AppVars.MainForm.UpdateTexLog), "(" + AppVars.MainForm.GetServerTime() + ") Начало боя " + AppVars.LastBoiLog);
                }
            }
            catch (InvalidOperationException)
            {
            }
        }

        public long CalcRestoreAfterBoi()
        {
            AppVars.Profile.Pers.LogReady = LogBoi;
            double sec = 0;

            if (AppVars.Profile.LezDoWaitHp)
            {
                if (_percentHp < AppVars.Profile.LezWaitHp)
                {
                    var goalHp = (int)(AppVars.Profile.LezWaitHp * _maxHp / 100.0);
                    sec = ((goalHp - _currentHp) * AppVars.Profile.Pers.IntHP) / _maxHp;
                }
            }

            if (AppVars.Profile.LezDoWaitMa)
            {
                if (_percentMa < AppVars.Profile.LezWaitMa)
                {
                    var goalMa = (int)(AppVars.Profile.LezWaitMa * _maxMa / 100.0);
                    var secma = ((goalMa - _currentMa) * AppVars.Profile.Pers.IntHP) / _maxMa;
                    if (secma > sec)
                    {
                        sec = secma;
                    }
                }
            }

            return sec < 1 ? 0 : DateTime.Now.AddSeconds(sec).Ticks;
        }

        private void ShowRazdMessage()
        {
            var logs = HelperStrings.SubString(_html, @"var logs = [", "];");
            if (!string.IsNullOrEmpty(logs))
            {
                var posone = logs.IndexOf(']');
                if (posone != -1)
                {
                    posone = logs.IndexOf(']', posone + 1);
                    if (posone != -1)
                    {
                        var posta = logs.LastIndexOf('[', posone);
                        if (posta != -1)
                        {
                            var rezras = logs.Substring(posta + 1, posone - posta - 1);
                            var spraz = rezras.Split(',');
                            if (spraz.Length == 4)
                            {
                                if (spraz[0].Equals("8", StringComparison.Ordinal))
                                {
                                    var sb = new StringBuilder();
                                    sb.Append("Результат разделки: <font color=#E34242><b>«");
                                    sb.Append(Strip(spraz[2]));
                                    sb.Append("»</b></font>.");
                                    AppVars.RazdelkaResultList.Add(Strip(spraz[2]));

                                    AppVars.AutoSkinCheckRes = true;

                                    if (!spraz[3].Equals("0", StringComparison.Ordinal))
                                    {
                                        AppVars.AutoSkinCheckUm = true;
                                        sb.Append(" Умение <b>«Охота»</b> повысилось на 1!");
                                        AppVars.RazdelkaLevelUp++;
                                    }

                                    /*
                                    try
                                    {
                                        if (AppVars.MainForm != null)
                                        {
                                            AppVars.MainForm.BeginInvoke(
                                                new UpdateWriteChatMsgDelegate(AppVars.MainForm.WriteChatMsg), sb.ToString());
                                        }
                                    }
                                    catch (InvalidOperationException)
                                    {
                                    }
                                    */

                                    AppVars.RazdelkaTime = DateTime.Now.AddSeconds(5);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
