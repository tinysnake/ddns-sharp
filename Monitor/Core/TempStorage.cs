using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDnsSharp.Monitor.Core
{
    public class TempStorage
    {
        private Dictionary<string, object> storage = new Dictionary<string, object>();

        public T GetAndRemove<T>(string key)
        {
            if (storage.ContainsKey(key))
            {
                var result = storage[key];
                storage.Remove(key);
                if (result is T)
                    return (T)result;
                return default(T);
            }
            return default(T);
        }

        public T Get<T>(string key)
        {
            if (storage.ContainsKey(key))
            {
                var result = storage[key];
                if (result is T)
                    return (T)result;
                return default(T);
            }
            return default(T);
        }

        public void Set(string key, object value)
        {
            if (storage.ContainsKey(key))
            {
                storage[key] = value;
            }
            else
            {
                storage.Add(key, value);
            }
        }

        public void Remove(string key)
        {
            if (storage.ContainsKey(key))
                storage.Remove(key);
        }

        public void Clear()
        {
            storage.Clear();
        }
    }
}
