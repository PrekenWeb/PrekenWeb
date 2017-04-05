using System.Data;

namespace Data.Database.Dapper.Common.Data
{
    public interface IDbConnectionFactory
    {
        IDbConnection GetConnection();
    }
}