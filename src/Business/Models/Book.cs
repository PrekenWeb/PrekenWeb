namespace Business.Models
{
    public class Book
    {
        public int Id { get; set; }
        public int LanguageId { get; set; }
        public string Name { get; set; }
        public string FollowupNumber { get; set; }
        public string Abbreviation { get; set; }
        public string ShowChapter { get; set; }
    }

    public class BookFilter
    {
        public int? BookId { get; set; }
    }
}