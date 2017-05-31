using Data.Database.Dapper.Common.Data;
using Data.Database.Dapper.Common.Filtering;
using Data.Database.Dapper.Interfaces.Gateways;
using Data.Database.Dapper.Models;

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