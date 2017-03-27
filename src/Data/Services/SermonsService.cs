using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using PrekenWeb.Data.Repositories;
using PrekenWeb.Data.Services.Interfaces;
using PrekenWeb.Data.Tables;
using PrekenWeb.Data.ViewModels;

namespace PrekenWeb.Data.Services
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

        public async Task<SermonModel> GetSingle(int id)
        {
            var preek = await _preekRepository.GetSingle(id);

            var sermon = _mapper.Map<Preek, SermonModel>(preek);
            return sermon;
        }

        public async Task<IEnumerable<SermonModel>> Get(SermonFilter filter)
        {
            var preken = await _preekRepository.Get(_mapper.Map<SermonFilter,SermonDataFilter>(filter));
            var sermons = _mapper.Map<IEnumerable<Preek>, IEnumerable<SermonModel>>(preken);
            return sermons;
        }

        public async Task<IEnumerable<SermonModel>> GetNew(SermonFilter filter)
        {
            if(filter == null)
                filter = new SermonFilter();

            var dataFilter = _mapper.Map<SermonFilter, SermonDataFilter>(filter);
            dataFilter.Page = 0;
            dataFilter.PageSize = 10;
            //dataFilter.SortBy = preek => preek.DatumGepubliceerd;
            dataFilter.SortDirection = SortDirection.Descending;

            var preken = await _preekRepository.Get(dataFilter);
            var sermons = _mapper.Map<IEnumerable<Preek>, IEnumerable<SermonModel>>(preken);
            return sermons;
        }
    }
}