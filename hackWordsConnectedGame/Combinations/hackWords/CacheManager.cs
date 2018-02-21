

using System.Collections;

namespace hackWords
{
    public sealed class CacheManager
    {
        private static readonly CacheManager cachManager = new CacheManager();
        private static readonly Hashtable CacheTable = Hashtable.Synchronized(new Hashtable());

        public static CacheManager GetCacheManager()
        {
            return cachManager;
        }

        public void Add(string key, object keyData)
        {
            lock (this)
            {
                if (CacheTable.ContainsKey(key))
                    CacheTable[key] = keyData;
                else
                    CacheTable.Add(key, keyData);
            }
        }

        public void Remove(string key)
        {
            lock (this)
            {
                CacheTable.Remove(key);
            }
        }

        public void Flush()
        {
            lock (this)
            {
                CacheTable.Clear();
            }
        }

        public object this[string key] => CacheTable[key];
    }
}