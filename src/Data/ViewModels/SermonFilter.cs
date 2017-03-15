namespace PrekenWeb.Data.ViewModels
{
    public class SermonFilter
    {
        public int? LanguageId { get; set; }
        public int? PageSize { get; set; } = 25;
        public int? Page { get; set; } = 0;
    }
}