using DapperExtensions.Mapper;
using Data.Models;

namespace Data.Database.Dapper.ClassMappers
{
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
            Map(x => x.ShowChapter).Column("ToonHoofdstukNummer");

            // auto map all other columns
            AutoMap();
        }
    }
}