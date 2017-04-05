using DapperExtensions.Mapper;
using Data.Database.Dapper.Common.Filtering;

namespace Data.Database.Dapper.Models
{
    public class LectureTypeData
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public sealed class LectureTypeDataFilter : DataFilter<LectureTypeDataFilter, LectureTypeData>
    {
        public int? LectureTypeId { get; set; }
    }

    public sealed class LectureTypeDataMapper : ClassMapper<LectureTypeData>
    {
        public LectureTypeDataMapper()
        {
            TableName = "PreekType";

            //have a custom primary key
            Map(x => x.Id).Key(KeyType.Assigned);

            // Map Dutch column names to English property names
            Map(x => x.Name).Column("Omschrijving");

            // auto map all other columns
            AutoMap();
        }
    }
}