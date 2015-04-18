using System;
using System.Collections.Generic;
using System.Web;

namespace Prekenweb.Website.Lib.Cache
{
    public class PrekenwebHttpCache : IPrekenwebCache
    {  
        public T GetCached<T>(string cacheKey, int taalId, string userName, Func<T> getItemCallback) where T : class
        {
            var mergedCacheId = string.Format("{0}.{1}.{2}", cacheKey, taalId, userName);
            var item = HttpRuntime.Cache.Get(mergedCacheId) as T;
            if (item == null)
            {
                item = getItemCallback();
                HttpRuntime.Cache.Insert(mergedCacheId, item, null, DateTime.Now.AddHours(1), TimeSpan.Zero);
            }
            return item;
        }

        public void RemoveCached(string searchKey)
        { 
            var keys = new List<string>();

            var enumerator = HttpRuntime.Cache.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (enumerator.Key.ToString().Contains(searchKey))
                    keys.Add(enumerator.Key.ToString());
            }
            foreach (string cacheKey in keys)
                HttpRuntime.Cache.Remove(cacheKey);
        }

    }
}