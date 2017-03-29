using PrekenWeb.Data.DataModels;

namespace PrekenWeb.Data.Database.Dapper.Models
{
    public class SpeakerDataFilter : DataFilter<SpeakerDataFilter, SpeakerData>
    {
        public int? SpeakerId { get; set; }
        public string LastName { get; set; }
    }
}