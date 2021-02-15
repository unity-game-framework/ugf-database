using System.Threading.Tasks;

namespace UGF.Database.Runtime
{
    public interface IDatabase<TKey, TValue> : IDatabase
    {
        new event DatabaseValueHandler<TKey, TValue> Changed;

        void Set(TKey key, TValue value);
        bool TrySet(TKey key, TValue value);
        Task SetAsync(TKey key, TValue value);
        Task<bool> TrySetAsync(TKey key, TValue value);
        TValue Get(TKey key);
        bool TryGet(TKey key, out TValue value);
        Task<TValue> GetAsync(TKey key);
        Task<DatabaseGetAsyncResult<TValue>> TryGetAsync(TKey key);
    }
}
