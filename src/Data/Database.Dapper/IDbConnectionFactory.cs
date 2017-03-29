using System.Data;

namespace PrekenWeb.Data.Gateways
{
    public interface IDbConnectionFactory
    {
        IDbConnection GetConnection();
    }
}