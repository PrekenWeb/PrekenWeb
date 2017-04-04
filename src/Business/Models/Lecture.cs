namespace Business.Models
{
    public class Lecture
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Information { get; set; }
    }

    public class LectureFilter
    {
        public int? LanguageId { get; set; }
        public int? PageSize { get; set; } = 25;
        public int? Page { get; set; } = 0;
    }
}
