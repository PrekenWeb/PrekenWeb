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
    internal class LanguagesGateway : ILanguagesGateway
    {
        private readonly IDbConnectionFactory _connectionFactory;
        private readonly IPredicateFactory _predicateFactory;

        public LanguagesGateway(IDbConnectionFactory connectionFactory, IPredicateFactory predicateFactory)
        {
            _connectionFactory = connectionFactory;
            _predicateFactory = predicateFactory;
        }

        public async Task<IEnumerable<LanguageData>> Get(LanguageDataFilter filter)
        {
            var filterPredicate = _predicateFactory.GetPredicate<LanguageDataFilter, LanguageData>(filter);
            var languages = await _connectionFactory.GetConnection().GetListAsync<LanguageData>(filterPredicate);
            return languages?.ToList();
        }

        public async Task<LanguageData> GetSingle(int id)
        {
            return await _connectionFactory.GetConnection().GetAsync<LanguageData>(id);
        }

        public Task<int> Add(LanguageData model)
        {
            int id = _connectionFactory.GetConnection().Insert(model);
            return Task.FromResult(id);
        }

        public Task<bool> Update(LanguageData model)
        {
            var success = _connectionFactory.GetConnection().Update(model);
            return Task.FromResult(success);
        }

        public Task<bool> Delete(LanguageData language)
        {
            var success = _connectionFactory.GetConnection().Delete(language);
            return Task.FromResult(success);
        }
    }
}