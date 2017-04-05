using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DapperExtensions;
using Data.Database.Dapper.Common.Data;
using Data.Database.Dapper.Common.Filtering;
using Data.Database.Dapper.Interfaces.Gateways;
using Data.Database.Dapper.Models;

namespace Data.Database.Dapper.Gateways
{
    internal class LectureTypesGateway : ILectureTypesGateway
    {
        private readonly IDbConnectionFactory _connectionFactory;
        private readonly IPredicateFactory _predicateFactory;

        public LectureTypesGateway(IDbConnectionFactory connectionFactory, IPredicateFactory predicateFactory)
        {
            _connectionFactory = connectionFactory;
            _predicateFactory = predicateFactory;
        }

        public async Task<IEnumerable<LectureTypeData>> Get(LectureTypeDataFilter filter)
        {
            var filterPredicate = _predicateFactory.GetPredicate<LectureTypeDataFilter, LectureTypeData>(filter);
            var speakers = await _connectionFactory.GetConnection().GetListAsync<LectureTypeData>(filterPredicate);
            return speakers?.ToList();
        }

        public async Task<LectureTypeData> GetSingle(int id)
        {
            return await _connectionFactory.GetConnection().GetAsync<LectureTypeData>(id);
        }

        public Task<int> Add(LectureTypeData model)
        {
            int id = _connectionFactory.GetConnection().Insert(model);
            return Task.FromResult(id);
        }

        public Task<bool> Update(LectureTypeData model)
        {
            var success = _connectionFactory.GetConnection().Update(model);
            return Task.FromResult(success);
        }

        public Task<bool> Delete(LectureTypeData speaker)
        {
            var success = _connectionFactory.GetConnection().Delete(speaker);
            return Task.FromResult(success);
        }
    }
}