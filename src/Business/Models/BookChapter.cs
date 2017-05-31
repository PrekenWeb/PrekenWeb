namespace Business.Models
{
    public class BookChapter
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string Description { get; set; }
        public string FollowupNumber { get; set; }
    }

    public class BookChapterFilter
    {
        public int? BookChapterId { get; set; }
        public int? BookId { get; set; }

    }
}