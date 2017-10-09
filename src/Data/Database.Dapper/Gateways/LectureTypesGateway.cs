using DapperFilterExtensions.Data;
using DapperFilterExtensions.Filtering;
using Data.Database.Dapper.Filters;
using Data.Database.Dapper.Interfaces.Gateways;
using Data.Models;

namespace Data.Database.Dapper.Gateways
{
    internal class LectureTypesGateway : Gateway<LectureTypeData, LectureTypeDataFilter>, ILectureTypesGateway
    {
        public LectureTypesGateway(IDbConnectionFactory connectionFactory, IPredicateFactory predicateFactory)
            : base(connectionFactory, predicateFactory)
        {
        }
    }
}