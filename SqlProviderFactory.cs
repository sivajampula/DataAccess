using System.Data.Common;
using System.Data.SqlClient;

namespace EarlFileWatcher.DataAccess
{
    public class SqlProviderFactory:DbProviderFactory
    {
        public override DbCommand CreateCommand()
        {
            return new SqlCommand();
        }

        public override DbConnection CreateConnection()
        {
            return new SqlConnection();
        }

        public override DbParameter CreateParameter()
        {
            return new SqlParameter();
        }
    }
}
