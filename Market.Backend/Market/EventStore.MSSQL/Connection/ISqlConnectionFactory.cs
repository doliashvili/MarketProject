using System.Data.SqlClient;

namespace EventStore.MSSQL.Connection
{
    public interface ISqlConnectionFactory
    {
        SqlConnection SqlConnection();
    }
}
