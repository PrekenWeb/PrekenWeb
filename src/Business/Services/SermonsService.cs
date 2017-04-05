using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Business.Models;
using Business.Services.Interfaces;
using Data.Repositories;
using Data.Tables;

namespace Business.Services
{
    public class SermonsService : ISermonsService
    {
        private readonly IMapper _mapper;
        private readonly IPreekRepository _preekRepository;

        public SermonsService(IMapper mapper, IPreekRepository preekRepository)
        {
            _mapper = mapper;
            _preekRepository = preekRepository;
        }

        public async Task<Sermon> GetSingle(int id)
        {
            var preek = await _preekRepository.GetSingle(id);

            var sermon = _mapper.Map<Preek, Sermon>(preek);
            return sermon;
        }

        public async Task<IEnumerable<Sermon>> Get(SermonFilter filter)
        {
            var preken = await _preekRepository.Get(_mapper.Map<SermonFilter,SermonDataFilter>(filter));
            var sermons = _mapper.Map<IEnumerable<Preek>, IEnumerable<Sermon>>(preken);
            return sermons;
        }

        public async Task<IEnumerable<Sermon>> GetNew(SermonFilter filter)
        {
            if(filter == null)
                filter = new SermonFilter();

            var dataFilter = _mapper.Map<SermonFilter, SermonDataFilter>(filter);
            dataFilter.Page = 0;
            dataFilter.PageSize = 10;
            //dataFilter.SortBy = preek => preek.DatumGepubliceerd;
            dataFilter.SortDirection = SortDirection.Descending;

            var preken = await _preekRepository.Get(dataFilter);
            var sermons = _mapper.Map<IEnumerable<Preek>, IEnumerable<Sermon>>(preken);
            return sermons;
        }

        public async Task<int> Add(Sermon lecture)
        {
            throw new System.NotImplementedException();
        }

        public async Task<int> Update(Sermon lecture)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> Delete(Sermon lecture)
        {
            throw new System.NotImplementedException();
        }
    }
}