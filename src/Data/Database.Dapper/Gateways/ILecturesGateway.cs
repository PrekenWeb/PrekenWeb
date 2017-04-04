using Data.Database.Dapper.Models;

namespace Data.Database.Dapper.Gateways
{
    public interface ILecturesGateway : IGateway<LectureData, LectureDataFilter>
    {
    }
}