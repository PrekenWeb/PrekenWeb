using Data.Database.Dapper.Filters;
using Data.Models;

namespace Data.Database.Dapper.Interfaces.Gateways
{
    public interface IBookChaptersGateway : IGateway<BookChapterData, BookChapterDataFilter>
    {
    }
}