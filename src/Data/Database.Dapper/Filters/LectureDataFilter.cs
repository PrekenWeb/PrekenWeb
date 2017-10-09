using DapperFilterExtensions.Filtering;
using Data.Models;

namespace Data.Database.Dapper.Filters
{
    public sealed class LectureDataFilter : DataFilter<LectureDataFilter, LectureData>
    {
        public int? LectureId { get; set; }
        public int? LectureTypeId { get; set; }
        public int? LanguageId { get; set; }
        public int? SpeakerId { get; set; }
        public int? CongregationId { get; set; }
        public string Title { get; set; }
    }
}