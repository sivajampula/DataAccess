using System;
using System.Data;
using System.Data.Common;

namespace EarlFileWatcher.DataAccess
{
    public interface ISqlDatabase :IDisposable
    {
        IDataReader ExecuteReader(IDbCommand command);

        void Open(string connectionString);

        IDbCommand CreateCommand();

        IDbCommand CreateCommand(string commandText, params IDbDataParameter[] parameters);

        IDbDataParameter CreateParameter(string name);

        IDbDataParameter CreateParameter(string name, object value);

        DbParameter CreateParameter(string name, SqlDbType type,int size, object value);

        DbParameter CreateParameter(string name, SqlDbType type, string typeName, object value);

        int ExecuteNonQuery(IDbCommand command);

        int ExecuteSPNonQuery(IDbCommand command);

        IDbConnection GetConnection();

        DataSet ExecuteDataSet(IDbCommand command);

        object ExecuteScalar(IDbCommand command);

        void PrepareCommand(IDbCommand command, IDbConnection connection);

        void PrepareCommand(IDbCommand command, IDbConnection connection, params IDbDataParameter[] parameters);

        void PrepareSPCommand(IDbCommand command, IDbConnection connection, params IDbDataParameter[] parameters);

        void SetCommandParameters(IDbCommand command,  params IDbDataParameter[] parameters);

        IDataReader ExecuteSPReader(IDbCommand command);

        

    }
}
