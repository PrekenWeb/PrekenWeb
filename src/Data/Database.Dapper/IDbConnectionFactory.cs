using System.Data;

namespace Data.Database.Dapper
{
    public interface IDbConnectionFactory
    {
        IDbConnection GetConnection();
    }
}