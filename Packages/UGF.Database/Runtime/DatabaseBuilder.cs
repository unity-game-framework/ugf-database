namespace UGF.Database.Runtime
{
    public class DatabaseBuilder<TDatabase> : DatabaseBuilderBase where TDatabase : class, IDatabase, new()
    {
        protected override IDatabase OnBuild()
        {
            return new TDatabase();
        }
    }
}
