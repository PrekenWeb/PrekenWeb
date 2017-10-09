using DapperExtensions.Mapper;
using Data.Models;

namespace Data.Database.Dapper.ClassMappers
{
    public sealed class LectureDataMapper : ClassMapper<LectureData>
    {
        public LectureDataMapper()
        {
            TableName = "Preek";

            //have a custom primary key
            Map(x => x.Id).Key(KeyType.Assigned);

            // Map Dutch column names to English property names
            Map(x => x.LectureTypeId).Column("PreekTypeId");
            Map(x => x.LanguageId).Column("TaalId");
            Map(x => x.SpeakerId).Column("PredikantId");
            Map(x => x.CongregationId).Column("GemeenteId");
            Map(x => x.Title).Column("Omschrijving");
            Map(x => x.Information).Column("Informatie");
            Map(x => x.Description).Column("LezingOmschrijving");
            Map(x => x.LectureDateTime).Column("DatumPreek");
            Map(x => x.FileName).Column("Bestandsnaam");

            // auto map all other columns
            AutoMap();
        }
    }
}