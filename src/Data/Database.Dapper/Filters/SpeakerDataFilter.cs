using DapperFilterExtensions.Filtering;
using Data.Models;

namespace Data.Database.Dapper.Filters
{
    public class SpeakerDataFilter : DataFilter<SpeakerDataFilter, SpeakerData>
    {
        public int? SpeakerId { get; set; }
        public int? LanguageId { get; set; }
        public string LastName { get; set; }
    }
}
