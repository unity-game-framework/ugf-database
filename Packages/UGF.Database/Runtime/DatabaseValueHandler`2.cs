namespace UGF.Database.Runtime
{
    public delegate void DatabaseValueHandler<in TKey, in TValue>(TKey key, TValue value);
}
