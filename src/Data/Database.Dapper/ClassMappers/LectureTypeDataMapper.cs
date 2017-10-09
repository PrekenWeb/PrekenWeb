using DapperExtensions.Mapper;
using Data.Models;

namespace Data.Database.Dapper.ClassMappers
{
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