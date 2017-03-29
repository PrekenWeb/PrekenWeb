namespace PrekenWeb.Data.ViewModels
{
    public class Speaker
    {
        public int Id { get; set; }
        public string LastName { get; set; }
    }

    public class SpeakerFilter
    {
        public int? SpeakerId { get; set; }
        public string LastName { get; set; }
    }
}