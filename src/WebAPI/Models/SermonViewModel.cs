namespace WebAPI.Models
{
    public class SermonEditModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Information { get; set; }
    }

    public class SermonViewModel : SermonEditModel
    {
    }

    public class SermonFilterModel
    {
        public int? LanguageId { get; set; }
        public int? PageSize { get; set; } = 25;
        public int? Page { get; set; } = 0;
    }
}