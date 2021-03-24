namespace Core.CrossCuttingConcerns.Caching
{
    public interface ICacheManager
    {
        public void AddDataToCache(string key, object value, int duration);
        public object GetDataFromCache(string key);
        public T GetDataFromCache<T>(string key);
        public bool IsDataInCache(string key);
        public void RemoveDataFromCache(string key);
        public void RemoveDataFromCacheByPattern(string pattern);
    }
}