using System.Data.SqlClient;

namespace EventStore.MSSQL.Connection
{
    public class SqlConnectionFactory : ISqlConnectionFactory
    {
        private readonly string _connectionString;

        public SqlConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SqlConnection SqlConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
