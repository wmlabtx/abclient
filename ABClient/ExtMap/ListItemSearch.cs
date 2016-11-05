namespace ABClient.ExtMap
{
    public class ListItemSearch
    {
        public string RegNum { get; private set; }
        public int Jumps { get; private set; }
        public string Html { get; private set; }

        public ListItemSearch(string regnum, int jumps, string html)
        {
            RegNum = regnum;
            Jumps = jumps;
            Html = html;
        }

        public override string ToString()
        {
            return RegNum + " (шагов: " + Jumps + ")";
        }
    }
}