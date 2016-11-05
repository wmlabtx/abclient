namespace ABClient
{
    using System.Globalization;

    public class ListItemBotLevelEx
    {
        public ListItemBotLevelEx(int levelvalue)
        {
            BotLevel = string.Format(CultureInfo.InvariantCulture, "[{0}] и слабее", levelvalue);
            BotLevelValue = levelvalue;
        }

        public int BotLevelValue { get; private set; }

        public string BotLevel { get; private set; }
    }
}