using System;

namespace UGF.Database.Runtime
{
    public readonly struct DatabaseGetAsyncResult : IEquatable<DatabaseGetAsyncResult>
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

        public bool Equals(DatabaseGetAsyncResult other)
        {
            return Equals(m_value, other.m_value);
        }

        public override bool Equals(object obj)
        {
            return obj is DatabaseGetAsyncResult other && Equals(other);
        }

        public override int GetHashCode()
        {
            return m_value != null ? m_value.GetHashCode() : 0;
        }

        public override string ToString()
        {
            return $"{nameof(m_value)}: '{m_value}'";
        }
    }
}
