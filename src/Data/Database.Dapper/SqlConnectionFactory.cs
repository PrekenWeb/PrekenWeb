using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Data.Database.Dapper
{
    public class SqlConnectionFactory : IDbConnectionFactory
    {
        private readonly string _connectionString;

        public SqlConnectionFactory()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["PrekenwebContext"].ConnectionString;
        }

        public SqlConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}