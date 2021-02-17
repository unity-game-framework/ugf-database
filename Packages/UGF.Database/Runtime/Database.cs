using System;
using System.Threading.Tasks;

namespace UGF.Database.Runtime
{
    public abstract class Database<TKey, TValue> : IDatabase<TKey, TValue>
    {
        public event DatabaseValueHandler<TKey, TValue> Added;
        public event DatabaseKeyHandler<TKey> Removed;
        public event DatabaseValueHandler<TKey, TValue> Changed;

        event DatabaseValueHandler IDatabase.Added { add { m_addedHandler += value; } remove { m_addedHandler -= value; } }
        event DatabaseKeyHandler IDatabase.Removed { add { m_removedHandler += value; } remove { m_removedHandler -= value; } }
        event DatabaseValueHandler IDatabase.Changed { add { m_changedHandler += value; } remove { m_changedHandler -= value; } }

        private DatabaseValueHandler m_addedHandler;
        private DatabaseKeyHandler m_removedHandler;
        private DatabaseValueHandler m_changedHandler;

        public void Add(TKey key, TValue value)
        {
            OnAdd(key, value);

            Added?.Invoke(key, value);
            m_addedHandler?.Invoke(key, value);
        }

        public async Task AddAsync(TKey key, TValue value)
        {
            await OnAddAsync(key, value);

            Added?.Invoke(key, value);
            m_addedHandler?.Invoke(key, value);
        }

        public bool Remove(TKey key)
        {
            if (OnRemove(key))
            {
                Removed?.Invoke(key);
                m_removedHandler?.Invoke(key);
                return true;
            }

            return false;
        }

        public async Task<bool> RemoveAsync(TKey key)
        {
            if (await OnRemoveAsync(key))
            {
                Removed?.Invoke(key);
                m_removedHandler?.Invoke(key);
                return true;
            }

            return false;
        }

        public void Set(TKey key, TValue value)
        {
            if (!TrySet(key, value))
            {
                throw new ArgumentException($"Value can not be set by the specified key: '{key}', value:'{value}'.");
            }
        }

        public bool TrySet(TKey key, TValue value)
        {
            if (OnTrySet(key, value))
            {
                Changed?.Invoke(key, value);
                m_changedHandler?.Invoke(key, value);
                return true;
            }

            return false;
        }

        public async Task SetAsync(TKey key, TValue value)
        {
            if (!await TrySetAsync(key, value))
            {
                throw new ArgumentException($"Value can not be set by the specified key: '{key}', value:'{value}'.");
            }
        }

        public async Task<bool> TrySetAsync(TKey key, TValue value)
        {
            if (await OnTrySetAsync(key, value))
            {
                Changed?.Invoke(key, value);
                m_changedHandler?.Invoke(key, value);
                return true;
            }

            return false;
        }

        public TValue Get(TKey key)
        {
            return TryGet(key, out TValue value) ? value : throw new ArgumentException($"Value not found by the specified key: '{key}'.");
        }

        public bool TryGet(TKey key, out TValue value)
        {
            return OnTryGet(key, out value);
        }

        public async Task<TValue> GetAsync(TKey key)
        {
            DatabaseGetAsyncResult<TValue> result = await TryGetAsync(key);

            return result.HasValue ? result.Value : throw new ArgumentException($"Value not found by the specified key: '{key}'.");
        }

        public Task<DatabaseGetAsyncResult<TValue>> TryGetAsync(TKey key)
        {
            return OnTryGetAsync(key);
        }

        protected virtual Task OnAddAsync(TKey key, TValue value)
        {
            OnAdd(key, value);

            return Task.CompletedTask;
        }

        protected virtual Task<bool> OnRemoveAsync(TKey key)
        {
            bool result = OnRemove(key);

            return Task.FromResult(result);
        }

        protected virtual Task<bool> OnTrySetAsync(TKey key, TValue value)
        {
            bool result = OnTrySet(key, value);

            return Task.FromResult(result);
        }

        protected virtual Task<DatabaseGetAsyncResult<TValue>> OnTryGetAsync(TKey key)
        {
            return OnTryGet(key, out TValue value)
                ? Task.FromResult(new DatabaseGetAsyncResult<TValue>(value))
                : Task.FromResult(new DatabaseGetAsyncResult<TValue>());
        }

        protected abstract void OnAdd(TKey key, TValue value);
        protected abstract bool OnRemove(TKey key);
        protected abstract bool OnTrySet(TKey key, TValue value);
        protected abstract bool OnTryGet(TKey key, out TValue value);

        void IDatabase.Add(object key, object value)
        {
            Add((TKey)key, (TValue)value);
        }

        Task IDatabase.AddAsync(object key, object value)
        {
            return AddAsync((TKey)key, (TValue)value);
        }

        bool IDatabase.Remove(object key)
        {
            return Remove((TKey)key);
        }

        Task<bool> IDatabase.RemoveAsync(object key)
        {
            return RemoveAsync((TKey)key);
        }

        void IDatabase.Set(object key, object value)
        {
            Set((TKey)key, (TValue)value);
        }

        bool IDatabase.TrySet(object key, object value)
        {
            return TrySet((TKey)key, (TValue)value);
        }

        Task IDatabase.SetAsync(object key, object value)
        {
            return SetAsync((TKey)key, (TValue)value);
        }

        Task<bool> IDatabase.TrySetAsync(object key, object value)
        {
            return TrySetAsync((TKey)key, (TValue)value);
        }

        object IDatabase.Get(object key)
        {
            return Get((TKey)key);
        }

        bool IDatabase.TryGet(object key, out object value)
        {
            if (TryGet((TKey)key, out TValue result))
            {
                value = result;
                return true;
            }

            value = default;
            return false;
        }

        async Task<object> IDatabase.GetAsync(object key)
        {
            return await GetAsync((TKey)key);
        }

        async Task<DatabaseGetAsyncResult> IDatabase.TryGetAsync(object key)
        {
            return await TryGetAsync((TKey)key);
        }
    }
}
