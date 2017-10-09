using DapperExtensions.Mapper;
using Data.Models;

namespace Data.Database.Dapper.ClassMappers
{
    public sealed class SpeakerDataMapper : ClassMapper<SpeakerData>
    {
        public SpeakerDataMapper()
        {
            TableName = "Predikant";

            //have a custom primary key
            Map(x => x.Id).Key(KeyType.Assigned);

            // Map Dutch column names to English property names
            Map(x => x.Titles).Column("Titels");
            Map(x => x.Initials).Column("Voorletters");
            Map(x => x.Prefixes).Column("Tussenvoegsels");
            Map(x => x.LastName).Column("Achternaam");
            Map(x => x.Congregation).Column("Gemeente"); // TODO Normalize to Congregations table?
            Map(x => x.PeriodOfLife).Column("LevensPeriode");
            Map(x => x.CongregationId).Column("GemeenteId");
            Map(x => x.Remarks).Column("Opmerking");
            Map(x => x.LanguageId).Column("TaalId");

            // auto map all other columns
            AutoMap();
        }
    }
}