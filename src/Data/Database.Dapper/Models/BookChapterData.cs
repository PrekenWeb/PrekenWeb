using DapperExtensions.Mapper;
using Data.Database.Dapper.Common.Filtering;

namespace Data.Database.Dapper.Models
{
    public class BookChapterData
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string Description { get; set; }
        public string FollowupNumber { get; set; }
    }

    public sealed class BookChapterDataFilter : DataFilter<BookChapterDataFilter, BookChapterData>
    {
        public int? BookChapterId { get; set; }
        public int? BookId { get; set; }
    }

    public sealed class BookChapterDataMapper : ClassMapper<BookChapterData>
    {
        public BookChapterDataMapper()
        {
            TableName = "BoekHoofdstuk";

            //have a custom primary key
            Map(x => x.Id).Key(KeyType.Assigned);

            // Map Dutch column names to English property names
            Map(x => x.BookId).Column("BoekId");
            Map(x => x.Description).Column("Omschrijving");
            Map(x => x.FollowupNumber).Column("Sortering");

            // auto map all other columns
            AutoMap();
        }
    }
}