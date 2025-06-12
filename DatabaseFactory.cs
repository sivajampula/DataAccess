namespace EarlFileWatcher.DataAccess
{
    public class DatabaseFactory : IDatabaseFactory
    {
        public DatabaseFactory()
        { }

        public ISqlDatabase GetDatabase()
        {
            ISqlDatabase database = new SqlDatabase();
            return database;
        }
    }
}
