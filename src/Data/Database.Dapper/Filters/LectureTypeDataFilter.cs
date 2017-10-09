using DapperFilterExtensions.Filtering;
using Data.Models;

namespace Data.Database.Dapper.Filters
{
    public sealed class LectureTypeDataFilter : DataFilter<LectureTypeDataFilter, LectureTypeData>
    {
        public int? LectureTypeId { get; set; }
    }
}