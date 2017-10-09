using DapperExtensions.Mapper;
using Data.Models;

namespace Data.Database.Dapper.ClassMappers
{
    public sealed class ImageDataMapper : ClassMapper<ImageData>
    {
        public ImageDataMapper()
        {
            TableName = "Afbeelding";

            //have a custom primary key
            Map(x => x.Id).Key(KeyType.Assigned);

            // Map Dutch column names to English property names
            Map(x => x.Filename).Column("Bestandsnaam");
            Map(x => x.Description).Column("Omschrijving");

            // auto map all other columns
            AutoMap();
        }
    }
}