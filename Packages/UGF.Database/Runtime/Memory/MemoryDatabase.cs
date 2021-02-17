using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

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

        protected override void OnAdd(string key, object value)
        {
            if (string.IsNullOrEmpty(key)) throw new ArgumentException("Value cannot be null or empty.", nameof(key));
            if (value == null) throw new ArgumentNullException(nameof(value));

            m_values.Add(key, value);
        }

        protected override bool OnRemove(string key)
        {
            if (string.IsNullOrEmpty(key)) throw new ArgumentException("Value cannot be null or empty.", nameof(key));

            return m_values.Remove(key);
        }

        protected override void OnSet(string key, object value)
        {
            if (string.IsNullOrEmpty(key)) throw new ArgumentException("Value cannot be null or empty.", nameof(key));

            m_values[key] = value ?? throw new ArgumentNullException(nameof(value));
        }

        protected override bool OnTryGet(string key, out object value)
        {
            if (string.IsNullOrEmpty(key)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(key));

            return m_values.TryGetValue(key, out value);
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
