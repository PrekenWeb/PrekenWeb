using System;
using DapperExtensions.Mapper;
using Data.Database.Dapper.Common.Filtering;

namespace Data.Database.Dapper.Models
{
    public class LectureData
    {
        public int Id { get; set; }
        public int LectureTypeId { get; set; }
        public int LanguageId { get; set; }
        public int SpeakerId { get; set; }
        public int CongregationId { get; set; }
        public string Title { get; set; }
        public string Information { get; set; }
        public string Description { get; set; }
        public DateTime LectureDateTime { get; set; }
        public string FileName { get; set; }
    }

    public class ViewLectureData : LectureData
    {
        public string LectureTypeName { get; set; }
        public string SpeakerName { get; set; }
    }

    public sealed class LectureDataFilter : DataFilter<LectureDataFilter, LectureData>
    {
        public int? LectureId { get; set; }
        public int? LectureTypeId { get; set; }
        public int? LanguageId { get; set; }
        public int? SpeakerId { get; set; }
        public int? CongregationId { get; set; }
        public string Title { get; set; }
    }

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