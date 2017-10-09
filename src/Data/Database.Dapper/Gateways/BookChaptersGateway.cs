using DapperFilterExtensions.Data;
using DapperFilterExtensions.Filtering;
using Data.Database.Dapper.Filters;
using Data.Database.Dapper.Interfaces.Gateways;
using Data.Models;

namespace Data.Database.Dapper.Gateways
{
    internal class BookChaptersGateway : Gateway<BookChapterData, BookChapterDataFilter>, IBookChaptersGateway
    {
        public BookChaptersGateway(IDbConnectionFactory connectionFactory, IPredicateFactory predicateFactory)
            : base(connectionFactory, predicateFactory)
        {
        }
    }
}