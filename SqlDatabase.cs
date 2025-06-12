using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace EarlFileWatcher.DataAccess
{
    public class SqlDatabase :ISqlDatabase
    {
        private readonly DbProviderFactory _dbProviderFactory;

        private DbConnection _databaseConnection;

        private bool _disposed;
       
        public SqlDatabase(): this(new SqlProviderFactory())
        { }

        public SqlDatabase(DbProviderFactory dbProviderFactory)
        {
            _dbProviderFactory = dbProviderFactory;
        }

        public string ConnectionString
        {
            get;
            private set;
        }

        public virtual IDbDataParameter CreateParameter(string name)
        {
            DbParameter parameter = _dbProviderFactory.CreateParameter();
            parameter.ParameterName = BuildParameterName(name);
            return parameter;
        }

        public virtual IDbDataParameter CreateParameter(string name, object value)
        {
            IDbDataParameter param = CreateParameter(name);
            if(value == null)
            {
                param.Value = DBNull.Value;
            }
            else
            {
                param.Value = value;
            }
            return param;
        }

        public virtual IDbDataParameter CreateParameter(string name, DbType type, int size, object value)
        {
            IDbDataParameter param = CreateParameter(name);
            param.DbType = type;
            param.Size = size;
            if (value == null)
            {
                param.Value = DBNull.Value;
            }
            else
            {
                param.Value = value;
            }
            return param;
        }

        public  DbParameter CreateParameter(string name, SqlDbType type, int size, object value)
        {
            var param = (SqlParameter)CreateParameter(name);
            param.SqlDbType = type;
            param.Size = size;
            if (value == null)
            {
                param.Value = DBNull.Value;
            }
            else
            {
                param.Value = value;
            }
            return param;
        }

        public DbParameter CreateParameter(string name, SqlDbType type, string typeName, object value)
        {
            var param = (SqlParameter)CreateParameter(name);
            param.SqlDbType = type;
            param.TypeName = typeName;
            if (value == null)
            {
                param.Value = DBNull.Value;
            }
            else
            {
                param.Value = value;
            }
            return param;
        }

        public int ExecuteNonQuery(IDbCommand command)
        {           
            IDbConnection connection = null;
            try
            {
                connection = GetConnection();
                PrepareCommand(command, connection);
                return command.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                throw;
            }                       
        }

        public IDataReader ExecuteReader(IDbCommand command)
        {
            IDbConnection connection = null;
            IDataReader result = null;

            try
            {
                connection = GetConnection();
                PrepareCommand(command, connection);
                result = command.ExecuteReader();
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DataSet ExecuteDataSet(IDbCommand command)
        {
            IDbConnection connection = null;
            DataSet dsResult = new DataSet();

            try
            {
                connection = GetConnection();
                PrepareCommand(command, connection);
                using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter((SqlCommand)command))
                {
                    sqlDataAdapter.Fill(dsResult);
                }               
                return dsResult;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public object ExecuteScalar(IDbCommand command)
        {
            IDbConnection connection = null;
            object result = null;

            try
            {
                connection = GetConnection();
                PrepareCommand(command, connection);
                result = command.ExecuteScalar();
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IDataReader ExecuteSPReader(IDbCommand command)
        {
            IDbConnection connection = null;
            IDataReader result = null;

            try
            {
                connection = GetConnection();
                PrepareCommand(command, connection);
                result = command.ExecuteReader();
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int ExecuteSPNonQuery(IDbCommand command)
        {
            IDbConnection connection = null;
            int result = 0;

            try
            {
                connection = GetConnection();
                PrepareCommand(command, connection);
                result = command.ExecuteNonQuery();
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public IDbConnection GetConnection()
        {
            if(_databaseConnection.State != ConnectionState.Open)
            {
                _databaseConnection.Open();
            }
            return _databaseConnection;
        }

        public IDbCommand CreateCommand(string commandText, params IDbDataParameter[] parameters)
        {
            IDbCommand command = _dbProviderFactory.CreateCommand();
            command.CommandText = commandText;
            SetCommandParameters(command, parameters);
            return command;
        }

        public IDbCommand CreateCommand()
        {
            return _dbProviderFactory.CreateCommand();
        }

        public void Open(string connectionString)
        {
            if(_databaseConnection == null)
            {
                ConnectionString = connectionString;
                _databaseConnection = _dbProviderFactory.CreateConnection();
                _databaseConnection.ConnectionString = ConnectionString;
            }
        }

        public void PrepareCommand(IDbCommand command, IDbConnection connection)
        {
            command.Connection = connection;
            SetCommandTimeOut(command);
        }

        public void PrepareCommand(IDbCommand command, IDbConnection connection, params IDbDataParameter[] parameters)
        {
            command.Connection = connection;
            SetCommandParameters(command, parameters);
            SetCommandTimeOut(command);
        }

        public void PrepareSPCommand(IDbCommand command, IDbConnection connection, params IDbDataParameter[] parameters)
        {
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            SetCommandParameters(command, parameters);
            SetCommandTimeOut(command);
        }

        public void SetCommandParameters(IDbCommand command, params IDbDataParameter[] parameters)
        {
            if(parameters!=null)
            {
                foreach (IDbDataParameter parameter in parameters)
                {
                    command.Parameters.Add(parameter);
                }
            }
        }

        public string BuildParameterName(string name)
        {
            return name;
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
                Dispose(disposing: true);
                GC.SuppressFinalize(this);
            }
        }

        private void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                {
                    if (_databaseConnection != null)
                    {
                        _databaseConnection.Close();
                        _databaseConnection = null;
                    }
                }
            }
            finally
            {
                _disposed = true;
            }
        }

        private void SetCommandTimeOut(IDbCommand command)
        {
            command.CommandTimeout = 60;          
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~SqlDatabase()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

       
    }
}
