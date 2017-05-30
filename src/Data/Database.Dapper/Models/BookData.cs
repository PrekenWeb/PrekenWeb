using DapperExtensions.Mapper;
using Data.Database.Dapper.Common.Filtering;

namespace Data.Database.Dapper.Models
{
    public class BookData
    {
        public int Id { get; set; }
        public int LanguageId { get; set; }
        public string Name { get; set; }
        public string FollowupNumber { get; set; }
        public string Abbreviation { get; set; }
        public string ShowChapter { get; set; }
    }

    public sealed class BookDataFilter : DataFilter<BookDataFilter, BookData>
    {
        public int? BookId { get; set; }
    }

    public sealed class BookDataMapper : ClassMapper<BookData>
    {
        public BookDataMapper()
        {
            TableName = "Boek";

            //have a custom primary key
            Map(x => x.Id).Key(KeyType.Assigned);

            // Map Dutch column names to English property names
            Map(x => x.LanguageId).Column("TaalId");
            Map(x => x.Name).Column("Boeknaam");
            Map(x => x.FollowupNumber).Column("Sortering");
            Map(x => x.Abbreviation).Column("Afkorting");

            // auto map all other columns
            AutoMap();
        }
    }
}