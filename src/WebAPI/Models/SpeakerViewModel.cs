namespace WebAPI.Models
{
    public class SpeakerViewModel
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

    public class SpeakerFilterModel
    {
        public int? SpeakerId { get; set; }
        public string LastName { get; set; }
    }
}