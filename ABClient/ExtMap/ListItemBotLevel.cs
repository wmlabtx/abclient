namespace ABClient.ExtMap
{
    public class ListItemBotLevel
    {
        public int BotLevelValue { get; private set; }
        public string BotLevel { get; private set; }

        public ListItemBotLevel(string level, int levelvalue)
        {
            BotLevel = level;
            BotLevelValue = levelvalue;
        }
    }
}
