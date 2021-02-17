using System;
using System.Threading.Tasks;

namespace UGF.Database.Runtime
{
    public abstract class Database<TKey, TValue> : DatabaseBase, IDatabase<TKey, TValue>
    {
        public new event DatabaseValueHandler<TKey, TValue> Changed;

        public void Add(TKey key, TValue value)
        {
            OnAdd(key, value);
        }

        public Task AddAsync(TKey key, TValue value)
        {
            return OnAddAsync(key, value);
        }

        public bool Remove(TKey key)
        {
            return OnRemove(key);
        }

        public Task<bool> RemoveAsync(TKey key)
        {
            return OnRemoveAsync(key);
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

        protected override void OnAdd(object key, object value)
        {
            OnAdd((TKey)key, (TValue)value);
        }

        protected override Task OnAddAsync(object key, object value)
        {
            return OnAddAsync((TKey)key, (TValue)value);
        }

        protected override bool OnRemove(object key)
        {
            return OnRemove((TKey)key);
        }

        protected override Task<bool> OnRemoveAsync(object key)
        {
            return OnRemoveAsync((TKey)key);
        }

        protected override bool OnTrySet(object key, object value)
        {
            return OnTrySet((TKey)key, (TValue)value);
        }

        protected override Task<bool> OnTrySetAsync(object key, object value)
        {
            return OnTrySetAsync((TKey)key, (TValue)value);
        }

        protected override bool OnTryGet(object key, out object value)
        {
            if (OnTryGet((TKey)key, out TValue result))
            {
                value = result;
                return true;
            }

            value = default;
            return false;
        }

        protected override async Task<DatabaseGetAsyncResult> OnTryGetAsync(object key)
        {
            DatabaseGetAsyncResult<TValue> result = await OnTryGetAsync((TKey)key);

            return result.HasValue ? new DatabaseGetAsyncResult(result.Value) : new DatabaseGetAsyncResult();
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
    }
}
