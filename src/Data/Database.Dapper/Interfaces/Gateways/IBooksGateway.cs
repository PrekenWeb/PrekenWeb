using Data.Database.Dapper.Filters;
using Data.Models;

namespace Data.Database.Dapper.Interfaces.Gateways
{
    public interface IBooksGateway : IGateway<BookData, BookDataFilter>
    {
    }
}