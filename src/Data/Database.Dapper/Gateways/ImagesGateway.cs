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
    internal class ImagesGateway : IImagesGateway
    {
        private readonly IDbConnectionFactory _connectionFactory;
        private readonly IPredicateFactory _predicateFactory;

        public ImagesGateway(IDbConnectionFactory connectionFactory, IPredicateFactory predicateFactory)
        {
            _connectionFactory = connectionFactory;
            _predicateFactory = predicateFactory;
        }

        public async Task<IEnumerable<ImageData>> Get(ImageDataFilter filter)
        {
            var filterPredicate = _predicateFactory.GetPredicate<ImageDataFilter, ImageData>(filter);
            var speakers = await _connectionFactory.GetConnection().GetListAsync<ImageData>(filterPredicate);
            return speakers?.ToList();
        }

        public async Task<ImageData> GetSingle(int id)
        {
            return await _connectionFactory.GetConnection().GetAsync<ImageData>(id);
        }

        public Task<int> Add(ImageData model)
        {
            int id = _connectionFactory.GetConnection().Insert(model);
            return Task.FromResult(id);
        }

        public Task<bool> Update(ImageData model)
        {
            var success = _connectionFactory.GetConnection().Update(model);
            return Task.FromResult(success);
        }

        public Task<bool> Delete(ImageData speaker)
        {
            var success = _connectionFactory.GetConnection().Delete(speaker);
            return Task.FromResult(success);
        }
    }
}