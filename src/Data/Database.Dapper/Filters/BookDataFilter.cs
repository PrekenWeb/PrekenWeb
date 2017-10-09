using DapperFilterExtensions.Filtering;
using Data.Models;

//using Data.Database.Dapper.Common.Filtering;

namespace Data.Database.Dapper.Filters
{
    public sealed class BookDataFilter : DataFilter<BookDataFilter, BookData>
    {
        public int? BookId { get; set; }
        public int? LanguageId { get; set; }
    }
}