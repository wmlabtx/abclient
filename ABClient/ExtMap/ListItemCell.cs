namespace ABClient.ExtMap
{
    public class ListItemCell
    {
        public string Description { get; private set; }
        public string RegNum { get; private set; }

        public ListItemCell(string adescription, string aregnum)
        {
            Description = adescription;
            RegNum = aregnum;
        }
    }
}