namespace ABClient.Lez
{
    public class LezSpell
    {
        private int _id;
        public readonly string Name;

        public LezSpell(int id, string name)
        {
            _id = id;
            Name = name;
        }

        public static bool IsPhBlock(int code)
        {
            return code >= 4 && code <= 28;
        }

        public static bool IsMagBlock(int code)
        {
            return code >= 29 && code <= 31;
        }

        public static bool IsPhHit(int code)
        {
            return code >= 0 && code <= 1;
        }

        public static bool IsMagHit(int code)
        {
            return code >= 2 && code <= 3;
        }

        public static bool IsScrollHit(int code)
        {
            return code == 277 || code == 338;
        }
    }
}
