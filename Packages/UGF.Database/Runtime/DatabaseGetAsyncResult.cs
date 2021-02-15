using System;

namespace UGF.Database.Runtime
{
    public readonly struct DatabaseGetAsyncResult
    {
        public object Value { get { return m_value ?? throw new InvalidOperationException("Value is null."); } }
        public bool HasValue { get { return m_value != null; } }

        private readonly object m_value;

        public DatabaseGetAsyncResult(object value = null)
        {
            m_value = value;
        }

        public bool IsValid()
        {
            return HasValue;
        }
    }
}
