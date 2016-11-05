using System;

namespace ABClient
{
    public class ChatUser
    {
        public ChatUser(string nick, string level, string sign, string status)
        {
            Nick = nick;
            Sign = sign.Equals("none", StringComparison.OrdinalIgnoreCase) ? string.Empty : sign;
            Status = status;
            Level = level;
            LastUpdated = DateTime.Now;
        }

        public string Nick { get; private set; }

        public string Sign { get; private set; }

        public string Status { get; private set; }

        public string Level { get; private set; }

        public DateTime LastUpdated { get; private set; }
    }
}
