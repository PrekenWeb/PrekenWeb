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
    internal class SpeakersGateway : ISpeakersGateway
    {
        private readonly IDbConnectionFactory _connectionFactory;
        private readonly IPredicateFactory _predicateFactory;

        public SpeakersGateway(IDbConnectionFactory connectionFactory, IPredicateFactory predicateFactory)
        {
            _connectionFactory = connectionFactory;
            _predicateFactory = predicateFactory;
        }

        public async Task<IEnumerable<SpeakerData>> Get(SpeakerDataFilter filter)
        {
            var filterPredicate = _predicateFactory.GetPredicate<SpeakerDataFilter, SpeakerData>(filter);
            var speakers = await _connectionFactory.GetConnection().GetListAsync<SpeakerData>(filterPredicate);
            return speakers?.ToList();
        }

        public async Task<SpeakerData> GetSingle(int id)
        {
            return await _connectionFactory.GetConnection().GetAsync<SpeakerData>(id);
        }

        public Task<int> Add(SpeakerData model)
        {
            int id = _connectionFactory.GetConnection().Insert(model);
            return Task.FromResult(id);
        }

        public Task<bool> Update(SpeakerData model)
        {
            var success = _connectionFactory.GetConnection().Update(model);
            return Task.FromResult(success);
        }

        public Task<bool> Delete(SpeakerData speaker)
        {
            var success = _connectionFactory.GetConnection().Delete(speaker);
            return Task.FromResult(success);
        }
    }
}
