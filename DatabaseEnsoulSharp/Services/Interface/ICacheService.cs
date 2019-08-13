namespace DatabaseEnsoulSharp.Services.Interface
{
    public interface ICacheService
    {
        T GetCache<T>(string key);

        void SetCache<T>(string key, T value);

        void ClearCacheAll();

        void ClearCacheByKey(string key);
    }
}