namespace ABClient.Lez
{
    public class LezBotsClass
    {
        public int Id { get; }
        public string Name { get; }
        public string Plural { get; }

        public LezBotsClass(int id, string name, string plural)
        {
            Id = id;
            Name = name;
            Plural = plural;
        }
    }
}
