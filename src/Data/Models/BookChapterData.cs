namespace Data.Models
{
    public class BookChapterData
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string Description { get; set; }
        public string FollowupNumber { get; set; }
    }
}