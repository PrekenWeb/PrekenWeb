namespace WebAPI.Models
{
    public class LanguageViewModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }

    public class LanguageFilterModel
    {
        public int? LanguageId { get; set; }
        public string Code { get; set; }
    }
}