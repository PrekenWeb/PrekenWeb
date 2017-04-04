using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DapperExtensions;
using Data.Database.Dapper.Metadata;
using Data.Database.Dapper.Models;

namespace Data.Database.Dapper.Gateways
{
    internal class LecturesGateway : ILecturesGateway
    {
        private readonly IDbConnectionFactory _connectionFactory;
        private readonly IPredicateFactory _predicateFactory;

        public LecturesGateway(IDbConnectionFactory connectionFactory, IPredicateFactory predicateFactory)
        {
            _connectionFactory = connectionFactory;
            _predicateFactory = predicateFactory;
        }

        public async Task<IEnumerable<LectureData>> Get(LectureDataFilter filter)
        {
            var filterPredicate = _predicateFactory.GetPredicate<LectureDataFilter, LectureData>(filter);
            var lectures = await _connectionFactory.GetConnection().GetListAsync<LectureData>(filterPredicate);
            return lectures?.ToList();
        }

        public async Task<LectureData> GetSingle(int id)
        {
            return await _connectionFactory.GetConnection().GetAsync<LectureData>(id);
        }

        public Task<int> Add(LectureData model)
        {
            int id = _connectionFactory.GetConnection().Insert(model);
            return Task.FromResult(id);
        }

        public Task<bool> Update(LectureData model)
        {
            var success = _connectionFactory.GetConnection().Update(model);
            return Task.FromResult(success);
        }

        public Task<bool> Delete(LectureData lecture)
        {
            var success = _connectionFactory.GetConnection().Delete(lecture);
            return Task.FromResult(success);
        }
    }
}