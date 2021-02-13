using System;
using UGF.Description.Runtime;

namespace UGF.Database.Runtime
{
    public abstract class DatabaseDescribed<TDescription, TKey, TValue> : Database<TKey, TValue>, IDescribed<TDescription> where TDescription : class, IDescription
    {
        public TDescription Description { get; }

        protected DatabaseDescribed(TDescription description)
        {
            Description = description ?? throw new ArgumentNullException(nameof(description));
        }

        public T GetDescription<T>() where T : class, IDescription
        {
            return (T)GetDescription();
        }

        public IDescription GetDescription()
        {
            return Description;
        }
    }
}
