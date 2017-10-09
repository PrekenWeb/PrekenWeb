using System.Data;

namespace DapperFilterExtensions.Data
{
    public interface IDbConnectionFactory
    {
        IDbConnection GetConnection();
    }
}