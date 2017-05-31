using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Data.Database.Dapper.Common.Data;

namespace Business.Services
{
    internal class Service<TBusiness, TBusinessFilter, TData, TDataFilter>
    {
        protected readonly IMapper Mapper;
        protected readonly IGateway<TData, TDataFilter> Gateway;

        public Service(IMapper mapper, IGateway<TData, TDataFilter> gateway)
        {
            Mapper = mapper;
            Gateway = gateway;
        }

        public async Task<IEnumerable<TBusiness>> Get(TBusinessFilter businessFilter)
        {
            var dataFilter = Mapper.Map<TBusinessFilter, TDataFilter>(businessFilter);
            var dataObjects = await Gateway.Get(dataFilter);
            var businessObjects = Mapper.Map<IEnumerable<TData>, IEnumerable<TBusiness>>(dataObjects);
            return businessObjects;
        }

        public async Task<TBusiness> GetSingle(int id)
        {
            var dataObject = await Gateway.GetSingle(id);
            var businessObject = Mapper.Map<TData, TBusiness>(dataObject);
            return businessObject;
        }

        public async Task<int> Add(TBusiness businessObject)
        {
            var dataObject = Mapper.Map<TBusiness, TData>(businessObject);
            return await Gateway.Add(dataObject);
        }

        public async Task<bool> Update(TBusiness businessObject)
        {
            var dataObject = Mapper.Map<TBusiness, TData>(businessObject);
            return await Gateway.Update(dataObject);
        }

        public async Task<bool> Delete(TBusiness businessObject)
        {
            var dataObject = Mapper.Map<TBusiness, TData>(businessObject);
            return await Gateway.Delete(dataObject);
        }
    }
}