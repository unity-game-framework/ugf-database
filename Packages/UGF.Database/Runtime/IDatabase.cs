using System.Threading.Tasks;

namespace UGF.Database.Runtime
{
    public interface IDatabase
    {
        event DatabaseValueHandler Changed;

        void Add(object key, object value);
        Task AddAsync(object key, object value);
        bool Remove(object key);
        Task<bool> RemoveAsync(object key);
        void Set(object key, object value);
        bool TrySet(object key, object value);
        Task SetAsync(object key, object value);
        Task<bool> TrySetAsync(object key, object value);
        object Get(object key);
        bool TryGet(object key, out object value);
        Task<object> GetAsync(object key);
        Task<DatabaseGetAsyncResult> TryGetAsync(object key);
    }
}
