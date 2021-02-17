using System;
using System.Threading.Tasks;

namespace UGF.Database.Runtime
{
    public abstract class DatabaseBase : IDatabase
    {
        public event DatabaseValueHandler Added;
        public event DatabaseKeyHandler Removed;
        public event DatabaseValueHandler Changed;

        public void Add(object key, object value)
        {
            if (key == null) throw new ArgumentNullException(nameof(key));
            if (value == null) throw new ArgumentNullException(nameof(value));

            OnAdd(key, value);

            Added?.Invoke(key, value);
        }

        public async Task AddAsync(object key, object value)
        {
            if (key == null) throw new ArgumentNullException(nameof(key));
            if (value == null) throw new ArgumentNullException(nameof(value));

            await OnAddAsync(key, value);

            Added?.Invoke(key, value);
        }

        public bool Remove(object key)
        {
            if (key == null) throw new ArgumentNullException(nameof(key));

            if (OnRemove(key))
            {
                Removed?.Invoke(key);
                return true;
            }

            return false;
        }

        public async Task<bool> RemoveAsync(object key)
        {
            if (key == null) throw new ArgumentNullException(nameof(key));

            if (await OnRemoveAsync(key))
            {
                Removed?.Invoke(key);
                return true;
            }

            return false;
        }

        public void Set(object key, object value)
        {
            if (!TrySet(key, value))
            {
                throw new ArgumentException($"Value can not be set by the specified key: '{key}', value:'{value}'.");
            }
        }

        public bool TrySet(object key, object value)
        {
            if (key == null) throw new ArgumentNullException(nameof(key));
            if (value == null) throw new ArgumentNullException(nameof(value));

            if (OnTrySet(key, value))
            {
                Changed?.Invoke(key, value);
                return true;
            }

            return false;
        }

        public async Task SetAsync(object key, object value)
        {
            if (!await TrySetAsync(key, value))
            {
                throw new ArgumentException($"Value can not be set by the specified key: '{key}', value:'{value}'.");
            }
        }

        public async Task<bool> TrySetAsync(object key, object value)
        {
            if (key == null) throw new ArgumentNullException(nameof(key));
            if (value == null) throw new ArgumentNullException(nameof(value));

            if (await OnTrySetAsync(key, value))
            {
                Changed?.Invoke(key, value);
                return true;
            }

            return false;
        }

        public object Get(object key)
        {
            return TryGet(key, out object value) ? value : throw new ArgumentException($"Value not found by the specified key: '{key}'.");
        }

        public bool TryGet(object key, out object value)
        {
            if (key == null) throw new ArgumentNullException(nameof(key));

            return OnTryGet(key, out value);
        }

        public async Task<object> GetAsync(object key)
        {
            DatabaseGetAsyncResult result = await TryGetAsync(key);

            return result.HasValue ? result.Value : throw new ArgumentException($"Value not found by the specified key: '{key}'.");
        }

        public Task<DatabaseGetAsyncResult> TryGetAsync(object key)
        {
            if (key == null) throw new ArgumentNullException(nameof(key));

            return OnTryGetAsync(key);
        }

        protected virtual Task OnAddAsync(object key, object value)
        {
            OnAdd(key, value);

            return Task.CompletedTask;
        }

        protected virtual Task<bool> OnRemoveAsync(object key)
        {
            bool result = OnRemove(key);

            return Task.FromResult(result);
        }

        protected virtual Task<bool> OnTrySetAsync(object key, object value)
        {
            bool result = OnTrySet(key, value);

            return Task.FromResult(result);
        }

        protected virtual Task<DatabaseGetAsyncResult> OnTryGetAsync(object key)
        {
            return OnTryGet(key, out object value)
                ? Task.FromResult(new DatabaseGetAsyncResult(value))
                : Task.FromResult(new DatabaseGetAsyncResult());
        }

        protected abstract void OnAdd(object key, object value);
        protected abstract bool OnRemove(object key);
        protected abstract bool OnTrySet(object key, object value);
        protected abstract bool OnTryGet(object key, out object value);
    }
}
