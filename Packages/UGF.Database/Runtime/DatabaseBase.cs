using System;
using System.Threading.Tasks;

namespace UGF.Database.Runtime
{
    public abstract class DatabaseBase : IDatabase
    {
        public void Set(object key, object value)
        {
            if (!OnTrySet(key, value))
            {
                throw new ArgumentException($"Value can not be set by the specified key: '{key}', value:'{value}'.");
            }
        }

        public bool TrySet(object key, object value)
        {
            return OnTrySet(key, value);
        }

        public async Task SetAsync(object key, object value)
        {
            if (!await OnTrySetAsync(key, value))
            {
                throw new ArgumentException($"Value can not be set by the specified key: '{key}', value:'{value}'.");
            }
        }

        public Task<bool> TrySetAsync(object key, object value)
        {
            return OnTrySetAsync(key, value);
        }

        public object Get(object key)
        {
            return TryGet(key, out object value) ? value : throw new ArgumentException($"Value not found by the specified key: '{key}'.");
        }

        public bool TryGet(object key, out object value)
        {
            return OnTryGet(key, out value);
        }

        public async Task<object> GetAsync(object key)
        {
            DatabaseGetAsyncResult result = await TryGetAsync(key);

            return result.HasValue ? result.Value : throw new ArgumentException($"Value not found by the specified key: '{key}'.");
        }

        public Task<DatabaseGetAsyncResult> TryGetAsync(object key)
        {
            return OnTryGetAsync(key);
        }

        protected abstract bool OnTrySet(object key, object value);
        protected abstract Task<bool> OnTrySetAsync(object key, object value);
        protected abstract bool OnTryGet(object key, out object value);
        protected abstract Task<DatabaseGetAsyncResult> OnTryGetAsync(object key);
    }
}
