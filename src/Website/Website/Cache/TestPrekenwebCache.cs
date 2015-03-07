using System;

namespace Prekenweb.Website.Cache
{
    public class TestPrekenwebCache : IPrekenwebCache
    {
        public T GetCached<T>(string cacheKey, int taalId, string userName, Func<T> getItemCallback) where T : class
        {
            return getItemCallback();
        }

        public void RemoveCached(string searchKey)
        {

        }
    }
}