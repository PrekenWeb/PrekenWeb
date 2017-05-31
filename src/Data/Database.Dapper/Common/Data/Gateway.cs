using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DapperExtensions;
using Data.Database.Dapper.Common.Filtering;

namespace Data.Database.Dapper.Common.Data
{
    internal class Gateway<TData, TDataFilter> : IGateway<TData, TDataFilter> where TData: class where TDataFilter: DataFilter<TDataFilter, TData>
    {
        private readonly IDbConnectionFactory _connectionFactory;
        private readonly IPredicateFactory _predicateFactory;

        public Gateway(IDbConnectionFactory connectionFactory, IPredicateFactory predicateFactory)
        {
            _connectionFactory = connectionFactory;
            _predicateFactory = predicateFactory;
        }

        public async Task<IEnumerable<TData>> Get(TDataFilter filter)
        {
            var filterPredicate = _predicateFactory.GetPredicate<TDataFilter, TData>(filter);
            var dataItems = await _connectionFactory.GetConnection().GetListAsync<TData>(filterPredicate);
            return dataItems?.ToList();
        }

        public async Task<TData> GetSingle(int id)
        {
            return await _connectionFactory.GetConnection().GetAsync<TData>(id);
        }

        public Task<int> Add(TData model)
        {
            int id = _connectionFactory.GetConnection().Insert(model);
            return Task.FromResult(id);
        }

        public Task<bool> Update(TData model)
        {
            var success = _connectionFactory.GetConnection().Update(model);
            return Task.FromResult(success);
        }

        public Task<bool> Delete(TData speaker)
        {
            var success = _connectionFactory.GetConnection().Delete(speaker);
            return Task.FromResult(success);
        }
    }
}