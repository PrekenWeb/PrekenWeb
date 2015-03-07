using System;

namespace Prekenweb.Website
{
    public interface IPrekenwebCache
    {
        T GetCached<T>(string cacheKey, int taalId, string userName, Func<T> getItemCallback) where T : class;
        void RemoveCached(string searchKey);
    }
}