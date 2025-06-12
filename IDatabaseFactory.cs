namespace EarlFileWatcher.DataAccess
{
    public interface  IDatabaseFactory
    {
        ISqlDatabase GetDatabase();
    }
}
