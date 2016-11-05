namespace ABClient.ABProxy
{
    using System.Collections;
    using System.Text;

    internal sealed class CookiePack
    {
        private readonly ArrayList _storage = new ArrayList();

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (var i = 0; i < _storage.Count; i++)
            {
                if (sb.Length > 0)
                {
                    sb.Append("; ");
                }

                sb.Append(((CookiePackItem)_storage[i]).Name);
                sb.Append('=');
                sb.Append(((CookiePackItem)_storage[i]).Value);
            }

            return sb.ToString();
        }

        internal void Add(string strHeaderName, string strValue)
        {
            var item = new CookiePackItem
                           {
                               Name = strHeaderName, 
                               Value = strValue
                           };

            _storage.Add(item);
        }
    }
}