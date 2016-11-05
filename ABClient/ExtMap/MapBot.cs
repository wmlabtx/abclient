namespace ABClient.ExtMap
{
    public class MapBot
    {
        public string Name { get; private set; }
        public int MinLevel { get; private set; }
        public int MaxLevel { get; private set; }
        public string C { get; private set; }
        public string D { get; private set; }

        public MapBot (string name, int minLevel, int maxLevel, string c, string d)
        {
            Name = name;
            MinLevel = minLevel;
            MaxLevel = maxLevel;
            C = c;
            D = d;
        }
    }
}
