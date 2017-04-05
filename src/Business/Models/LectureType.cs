namespace Business.Models
{
    public class LectureType
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class LectureTypeFilter
    {
        public int? LectureTypeId { get; set; }
    }
}
