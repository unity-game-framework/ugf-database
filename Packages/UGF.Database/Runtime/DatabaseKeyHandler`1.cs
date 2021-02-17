namespace UGF.Database.Runtime
{
    public delegate void DatabaseKeyHandler<in TKey>(TKey key);
}
