using DapperExtensions.Mapper;
using Data.Models;

namespace Data.Database.Dapper.ClassMappers
{
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