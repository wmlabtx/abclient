namespace ABClient
{
    internal static class Tips
    {
        private static int _lastTipIndex;
        private static int[] _arrayTipIndexes;
        private static readonly string[] Replics =
        {
            "Связаться с автором <b>ABClient</b> очень просто - Skype: <b>wmlab.home</b>, сайт: <b><a href='https://github.com/wmlabtx/abclient/wiki/' onclick='window.open(this.href);'>https://github.com/wmlabtx/abclient/wiki/</a></b>, E-mail: <b>wmlab@hotmail.com</b>", 
            "<b>ABClient</b> может попытаться вычислить клетки, где находится перс и его соклановцы. Надо открыть инфу перса и нажать на тулбаре кнопку [Компас]",               
            "При достижении определенной усталости клиент попытается найти в инвентаре блаж и выпить его. Настройки автопитья блажа находятся в первой закладке настроек.", 
            "<b>ABClient</b> может добавить в контакты сразу весь клан. Открываем инфу любого перса в клане и нажимаем на тулбаре кнопку [Весь клан].", 
            /*"<b>ABClient</b> может сдать пачку вещей в лавку одной кнопкой. Кнопка находится на панели вещи в инвентаре.", */
            "<b>ABClient</b> сам будет разделывать ботов, если надет нож и разделка возможна.", 
            "Серые цифры около маны - это число уникальных пользователей, запустивших ABClient 2.18 и более поздней версии за последние сутки. Число считается в момент Вашего захода в игру или перезахода в игру и не обновляется в процессе игры.",  
            "Вы можете объявить контакт или клан другом или врагом через правую кнопку мыши на контакте или группе в панели контактов.",
            "Магобой настраивать нет надобности, он сам подбирает оптимальные заклинания, удары и блоки."
        };

        internal static string GetNextAnswer()
        {
            if (_arrayTipIndexes == null || (_arrayTipIndexes.Length != Replics.Length))
            {
                _arrayTipIndexes = new int[Replics.Length];
                for (var i = 0; i < _arrayTipIndexes.Length; i++)
                {
                    _arrayTipIndexes[i] = i;
                }

                for (var i = 0; i < _arrayTipIndexes.Length; i++)
                {
                    var j = Helpers.Dice.Make(_arrayTipIndexes.Length);
                    var t = _arrayTipIndexes[i];
                    _arrayTipIndexes[i] = _arrayTipIndexes[j];
                    _arrayTipIndexes[j] = t;
                }

                _lastTipIndex = -1;
            }

            _lastTipIndex++;
            if (_lastTipIndex == Replics.Length)
            {
                _lastTipIndex = 0;
            }

            return Replics[_arrayTipIndexes[_lastTipIndex]];
        }
    }
}
