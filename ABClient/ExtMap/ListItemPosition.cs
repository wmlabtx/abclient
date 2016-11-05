namespace ABClient.ExtMap
{
    public class ListItemPosition
    {
        public string Code { get; private set; }
        public string Name { get; private set; }

        public ListItemPosition(string name, string code)
        {
            Name = name;
            Code = code;
        }
    }
}
