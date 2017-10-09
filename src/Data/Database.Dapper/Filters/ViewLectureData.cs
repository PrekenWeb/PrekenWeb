using Data.Models;

namespace Data.Database.Dapper.Filters
{
    public class ViewLectureData : LectureData
    {
        public string LectureTypeName { get; set; }
        public string SpeakerName { get; set; }
    }
}