using Data.Database.Dapper.Common.Data;
using Data.Database.Dapper.Models;

namespace Data.Database.Dapper.Interfaces.Gateways
{
    public interface IBooksGateway : IGateway<BookData, BookDataFilter>
    {
    }
}