using DapperExtensions.Mapper;

namespace Data.Database.Dapper.Models
{
    public class SpeakerData
    {
        public int Id { get; set; }
        public string Titles { get; set; }
        public string Initials { get; set; }
        public string Prefixes { get; set; }
        public string LastName { get; set; }
        public string Congregation { get; set; }
        public string PeriodOfLife { get; set; }
        public int? CongregationId { get; set; }
        public string Remarks { get; set; }
        public int LanguageId { get; set; }
        public bool HideFromIndexingRobots { get; set; }
        public bool HideFromPodcast { get; set; }
    }

    public class SpeakerDataFilter : DataFilter<SpeakerDataFilter, SpeakerData>
    {
        public int? SpeakerId { get; set; }
        public string LastName { get; set; }
    }

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
