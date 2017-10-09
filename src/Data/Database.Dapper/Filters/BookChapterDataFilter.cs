using DapperFilterExtensions.Filtering;
using Data.Models;

namespace Data.Database.Dapper.Filters
{
    public sealed class BookChapterDataFilter : DataFilter<BookChapterDataFilter, BookChapterData>
    {
        public int? BookChapterId { get; set; }
        public int? BookId { get; set; }
    }
}