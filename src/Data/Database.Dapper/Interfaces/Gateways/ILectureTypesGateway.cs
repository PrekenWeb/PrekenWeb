using Data.Database.Dapper.Filters;
using Data.Models;

namespace Data.Database.Dapper.Interfaces.Gateways
{
    public interface ILectureTypesGateway : IGateway<LectureTypeData, LectureTypeDataFilter>
    {
    }
}