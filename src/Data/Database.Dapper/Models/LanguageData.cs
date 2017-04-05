using DapperExtensions.Mapper;
using Data.Database.Dapper.Common.Filtering;

namespace Data.Database.Dapper.Models
{
    public class LanguageData
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }

    public class LanguageDataFilter : DataFilter<LanguageDataFilter, LanguageData>
    {
        public int? LanguageId { get; set; }
        public string Code { get; set; }
    }

    public sealed class LanguageDataMapper : ClassMapper<LanguageData>
    {
        public LanguageDataMapper()
        {
            TableName = "Taal";

            //have a custom primary key
            Map(x => x.Id).Key(KeyType.Assigned);

            // Map Dutch column names to English property names
            Map(x => x.Description).Column("Omschrijving");

            // auto map all other columns
            AutoMap();
        }
    }

}