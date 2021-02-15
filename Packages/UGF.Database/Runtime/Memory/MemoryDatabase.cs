using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace UGF.Database.Runtime.Memory
{
    public class MemoryDatabase : Database<string, object>, IEnumerable<KeyValuePair<string, object>>
    {
        public IReadOnlyDictionary<string, object> Values { get; }

        private readonly Dictionary<string, object> m_values = new Dictionary<string, object>();

        public MemoryDatabase()
        {
            Values = new ReadOnlyDictionary<string, object>(m_values);
        }

        public Dictionary<string, object>.Enumerator GetEnumerator()
        {
            return m_values.GetEnumerator();
        }

        protected override bool OnTrySet(string key, object value)
        {
            if (string.IsNullOrEmpty(key)) throw new ArgumentException("Value cannot be null or empty.", nameof(key));

            m_values[key] = value ?? throw new ArgumentNullException(nameof(value));

            return true;
        }

        protected override Task<bool> OnTrySetAsync(string key, object value)
        {
            bool result = OnTrySet(key, value);

            return Task.FromResult(result);
        }

        protected override bool OnTryGet(string key, out object value)
        {
            if (string.IsNullOrWhiteSpace(key)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(key));

            return m_values.TryGetValue(key, out value);
        }

        protected override Task<DatabaseGetAsyncResult<object>> OnTryGetAsync(string key)
        {
            return OnTryGet(key, out object value) ? Task.FromResult(new DatabaseGetAsyncResult<object>(value)) : Task.FromResult(new DatabaseGetAsyncResult<object>());
        }

        IEnumerator<KeyValuePair<string, object>> IEnumerable<KeyValuePair<string, object>>.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
