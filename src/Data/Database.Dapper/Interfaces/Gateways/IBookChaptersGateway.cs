using Data.Database.Dapper.Common.Data;
using Data.Database.Dapper.Models;

namespace Data.Database.Dapper.Interfaces.Gateways
{
    public interface IBookChaptersGateway : IGateway<BookChapterData, BookChapterDataFilter>
    {
    }
}