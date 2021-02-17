using System;
using System.Collections.Generic;

namespace UGF.Database.Runtime
{
    public readonly struct DatabaseGetAsyncResult<TValue> : IEquatable<DatabaseGetAsyncResult<TValue>>
    {
        public TValue Value { get { return HasValue ? m_value : throw new InvalidOperationException("Value is null."); } }
        public bool HasValue { get { return !typeof(TValue).IsClass || EqualityComparer<TValue>.Default.Equals(m_value, default); } }

        private readonly TValue m_value;

        public DatabaseGetAsyncResult(TValue value = default)
        {
            m_value = value;
        }

        public bool IsValid()
        {
            return HasValue;
        }

        public bool Equals(DatabaseGetAsyncResult<TValue> other)
        {
            return EqualityComparer<TValue>.Default.Equals(m_value, other.m_value);
        }

        public override bool Equals(object obj)
        {
            return obj is DatabaseGetAsyncResult<TValue> other && Equals(other);
        }

        public override int GetHashCode()
        {
            return EqualityComparer<TValue>.Default.GetHashCode(m_value);
        }

        public static implicit operator DatabaseGetAsyncResult(DatabaseGetAsyncResult<TValue> result)
        {
            return result.HasValue ? new DatabaseGetAsyncResult(result.Value) : new DatabaseGetAsyncResult();
        }

        public override string ToString()
        {
            return $"{nameof(m_value)}: '{m_value}'";
        }
    }
}
