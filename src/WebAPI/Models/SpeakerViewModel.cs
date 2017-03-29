namespace WebAPI.Models
{
    public class SpeakerViewModel
    {
        public int Id { get; set; }
        public string LastName { get; set; }
    }

    public class SpeakerFilterModel
    {
        public int? SpeakerId { get; set; }
        public string LastName { get; set; }
    }
}