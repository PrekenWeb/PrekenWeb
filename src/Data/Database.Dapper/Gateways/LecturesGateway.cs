using DapperFilterExtensions.Data;
using DapperFilterExtensions.Filtering;
using Data.Database.Dapper.Filters;
using Data.Database.Dapper.Interfaces.Gateways;
using Data.Models;

namespace Data.Database.Dapper.Gateways
{
    internal class LecturesGateway : Gateway<LectureData, LectureDataFilter>, ILecturesGateway
    {
        public LecturesGateway(IDbConnectionFactory connectionFactory, IPredicateFactory predicateFactory)
            : base(connectionFactory, predicateFactory)
        {
        }
    }
}