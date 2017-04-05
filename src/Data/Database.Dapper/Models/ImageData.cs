using DapperExtensions.Mapper;
using Data.Database.Dapper.Common.Filtering;

namespace Data.Database.Dapper.Models
{
    public class ImageData
    {
        public int Id { get; set; }
        public string Filename { get; set; }
        public string Description { get; set; }
        public string ContentType { get; set; }
    }

    public sealed class ImageDataFilter : DataFilter<ImageDataFilter, ImageData>
    {
        public int? ImageId { get; set; }
    }

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