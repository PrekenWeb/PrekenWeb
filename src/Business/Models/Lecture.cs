using System;

namespace Business.Models
{
    public class Lecture
    {
        public int Id { get; set; }
        public int LectureTypeId { get; set; }
        public int LanguageId { get; set; }
        public int SpeakerId { get; set; }
        public int CongregationId { get; set; }
        public string Title { get; set; }
        public string Information { get; set; }
        public string Description { get; set; }
        public DateTime LectureDateTime { get; set; }
        public string FileName { get; set; }
    }

    public class ViewLecture : Lecture
    {
        public string LectureTypeName { get; set; }
        public string SpeakerName { get; set; }
    }

    public class LectureFilter
    {
        public int? LectureId { get; set; }
        public int? LectureTypeId { get; set; }
        public int? LanguageId { get; set; }
        public int? SpeakerId { get; set; }
        public int? CongregationId { get; set; }
        public string Title { get; set; }
    }
}
