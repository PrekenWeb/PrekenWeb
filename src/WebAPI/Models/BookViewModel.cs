namespace WebAPI.Models
{
    public class BookViewModel
    {
        public int Id { get; set; }
        public int LanguageId { get; set; }
        public string Name { get; set; }
        public string FollowupNumber { get; set; }
        public string Abbreviation { get; set; }
        public string ShowChapter { get; set; }
    }

    public class BookFilterModel
    {
        public int? BookId { get; set; }
    }
}