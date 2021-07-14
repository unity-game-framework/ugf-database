using System;
using System.Threading.Tasks;

namespace UGF.Database.Runtime
{
    public abstract class Database<TKey, TValue> : IDatabase<TKey, TValue>
    {
        public event DatabaseValueHandler<TKey, TValue> Added;
        public event DatabaseKeyHandler<TKey> Removed;
        public event DatabaseValueHandler<TKey, TValue> Changed;
        public event Action Cleared;

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

        public void Clear()
        {
            OnClear();

            Cleared?.Invoke();
        }

        public async Task ClearAsync()
        {
            await OnClearAsync();

            Cleared?.Invoke();
        }

        public void Set(TKey key, TValue value)
        {
            OnSet(key, value);

            Changed?.Invoke(key, value);
            m_changedHandler?.Invoke(key, value);
        }

        public async Task SetAsync(TKey key, TValue value)
        {
            await OnSetAsync(key, value);

            Changed?.Invoke(key, value);
            m_changedHandler?.Invoke(key, value);
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

        protected virtual Task OnClearAsync()
        {
            OnClear();

            return Task.CompletedTask;
        }

        protected virtual Task OnSetAsync(TKey key, TValue value)
        {
            OnSet(key, value);

            return Task.CompletedTask;
        }

        protected virtual Task<DatabaseGetAsyncResult<TValue>> OnTryGetAsync(TKey key)
        {
            return OnTryGet(key, out TValue value)
                ? Task.FromResult(new DatabaseGetAsyncResult<TValue>(value))
                : Task.FromResult(new DatabaseGetAsyncResult<TValue>());
        }

        protected abstract void OnAdd(TKey key, TValue value);
        protected abstract bool OnRemove(TKey key);
        protected abstract void OnClear();
        protected abstract void OnSet(TKey key, TValue value);
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

        Task IDatabase.SetAsync(object key, object value)
        {
            return SetAsync((TKey)key, (TValue)value);
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
