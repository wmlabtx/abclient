using System.Collections.Generic;

namespace ABClient.Lez
{
    public static class LezBotsClassCollection
    {
        private static readonly SortedDictionary<int, LezBotsClass> Classes = new SortedDictionary<int, LezBotsClass>();

        private static void AddClass(LezBotsClass lezBotsClass)
        {
            if (!Classes.ContainsKey(lezBotsClass.Id))
            {
                Classes.Add(lezBotsClass.Id, lezBotsClass);
            }
        }

        static LezBotsClassCollection()
        {
            AddClass(new LezBotsClass(001, "Все", "Все"));
            AddClass(new LezBotsClass(010, "Человек", "Люди"));
            AddClass(new LezBotsClass(020, "Бот", "Боты"));
            AddClass(new LezBotsClass(021, "Босс", "Боссы"));
            AddClass(new LezBotsClass(101, "Орк", "Орки"));
            AddClass(new LezBotsClass(102, "Гоблин", "Гоблины"));
            AddClass(new LezBotsClass(103, "Крыса", "Крысы"));
            AddClass(new LezBotsClass(104, "Кабан", "Кабаны"));
            AddClass(new LezBotsClass(105, "Ядовитый паук", "Ядовитые пауки"));
            AddClass(new LezBotsClass(106, "Скелет", "Скелеты"));
            AddClass(new LezBotsClass(107, "Скелет-мечник", "Скелеты-мечники"));
            AddClass(new LezBotsClass(108, "Зомби", "Зомби"));
            AddClass(new LezBotsClass(109, "Тролль", "Тролли"));
            AddClass(new LezBotsClass(110, "Огр", "Огры"));
            AddClass(new LezBotsClass(111, "Огр-берсеркер", "Огры-берсеркеры"));
            AddClass(new LezBotsClass(112, "Сильф", "Сильфы"));
            AddClass(new LezBotsClass(113, "Нетопырь", "Нетопыри"));
            AddClass(new LezBotsClass(114, "Разбойник", "Разбойники"));
            AddClass(new LezBotsClass(115, "Грабитель", "Грабители"));
            AddClass(new LezBotsClass(116, "Призрак", "Призраки"));
            AddClass(new LezBotsClass(117, "Некромант", "Некроманты"));
            AddClass(new LezBotsClass(118, "Элементаль", "Элементали"));
            AddClass(new LezBotsClass(119, "Дварф", "Дварфы"));
            AddClass(new LezBotsClass(120, "Медведь", "Медведи"));
            AddClass(new LezBotsClass(121, "Воин Таэров", "Воины Таэров"));
        }

        public static LezBotsClass GetClass(int id)
        {
            return Classes.ContainsKey(id) ? Classes[id] : new LezBotsClass(0, id.ToString(), id.ToString());
        }

        public static List<LezBotsClass> ListForComboBox()
        {
            return new List<LezBotsClass>(Classes.Values);
        }
    }
}
