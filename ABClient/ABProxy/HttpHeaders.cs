namespace ABClient.ABProxy
{
    using System;
    using System.Collections;

    internal class HttpHeaders
    {
        internal HttpHeaders()
        {
            HttpVersion = "HTTP/1.1";
            Storage = new ArrayList();
        }

        internal string HttpVersion { get; set; }

        internal ArrayList Storage { get; set; }

        internal HttpHeaderItem this[int intHeaderNumber]
        {
            get
            {
                return (HttpHeaderItem)Storage[intHeaderNumber];
            }
        }

        internal string this[string strHeaderName]
        {
            get
            {
                for (var i = 0; i < Storage.Count; i++)
                {
                    if (string.Compare(((HttpHeaderItem)Storage[i]).Name, strHeaderName, StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        return ((HttpHeaderItem)Storage[i]).Value;
                    }
                }

                return string.Empty;
            }

            set
            {
                var flag = false;
                for (var i = 0; i < Storage.Count; i++)
                {
                    if (string.Compare(((HttpHeaderItem)Storage[i]).Name, strHeaderName, StringComparison.OrdinalIgnoreCase) != 0)
                    {
                        continue;
                    }

                    ((HttpHeaderItem)Storage[i]).Value = value;
                    flag = true;
                    break;
                }

                if (!flag)
                {
                    Add(strHeaderName, value);
                }
            }
        }

        internal void Add(string strHeaderName, string strValue)
        {
            var item = new HttpHeaderItem
                           {
                               Name = strHeaderName, 
                               Value = strValue
                           };

            Storage.Add(item);
        }

        internal int Count()
        {
            return Storage.Count;
        }

        internal bool Exists(string strHeaderName)
        {
            for (var i = 0; i < Storage.Count; i++)
            {
                if (string.Compare(((HttpHeaderItem)Storage[i]).Name, strHeaderName, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    return true;
                }
            }

            return false;
        }

        internal bool ExistsAndContains(string strHeaderName, string strHeaderValue)
        {
            for (var i = 0; i < Storage.Count; i++)
            {
                if ((string.Compare(((HttpHeaderItem)Storage[i]).Name, strHeaderName, StringComparison.OrdinalIgnoreCase) == 0) &&
                    (((HttpHeaderItem)Storage[i]).Value.IndexOf(strHeaderValue, StringComparison.OrdinalIgnoreCase) > -1))
                {
                    return true;
                }
            }

            return false;
        }

        internal bool ExistsAndEquals(string strHeaderName, string strHeaderValue)
        {
            for (var i = 0; i < Storage.Count; i++)
            {
                if ((string.Compare(((HttpHeaderItem)Storage[i]).Name, strHeaderName, StringComparison.OrdinalIgnoreCase) == 0) &&
                    (string.Compare(((HttpHeaderItem)Storage[i]).Value, strHeaderValue, StringComparison.OrdinalIgnoreCase) == 0))
                {
                    return true;
                }
            }

            return false;
        }

        internal void Remove(string strHeaderName)
        {
            for (var i = Storage.Count - 1; i >= 0; i--)
            {
                if (string.Compare(((HttpHeaderItem)Storage[i]).Name, strHeaderName, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    Storage.RemoveAt(i);
                }
            }
        }

        internal void RenameHeaderItems(string oldHeaderName, string newHeaderName)
        {
            for (int i = 0; i < Storage.Count; i++)
            {
                if (!string.Equals(((HttpHeaderItem) Storage[i]).Name, oldHeaderName, StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                ((HttpHeaderItem)Storage[i]).Name = newHeaderName;
            }
        }
    }
}