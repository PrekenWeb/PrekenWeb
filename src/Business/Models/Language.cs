namespace Business.Models
{
    public class Language
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }

    public class LanguageFilter
    {
        public int? LanguageId { get; set; }
        public string Code { get; set; }
    }
}