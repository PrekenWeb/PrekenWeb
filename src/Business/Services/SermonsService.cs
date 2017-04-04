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

        public async Task<Lecture> GetSingle(int id)
        {
            var preek = await _preekRepository.GetSingle(id);

            var sermon = _mapper.Map<Preek, Lecture>(preek);
            return sermon;
        }

        public async Task<IEnumerable<Lecture>> Get(LectureFilter filter)
        {
            var preken = await _preekRepository.Get(_mapper.Map<LectureFilter,SermonDataFilter>(filter));
            var sermons = _mapper.Map<IEnumerable<Preek>, IEnumerable<Lecture>>(preken);
            return sermons;
        }

        public async Task<IEnumerable<Lecture>> GetNew(LectureFilter filter)
        {
            if(filter == null)
                filter = new LectureFilter();

            var dataFilter = _mapper.Map<LectureFilter, SermonDataFilter>(filter);
            dataFilter.Page = 0;
            dataFilter.PageSize = 10;
            //dataFilter.SortBy = preek => preek.DatumGepubliceerd;
            dataFilter.SortDirection = SortDirection.Descending;

            var preken = await _preekRepository.Get(dataFilter);
            var sermons = _mapper.Map<IEnumerable<Preek>, IEnumerable<Lecture>>(preken);
            return sermons;
        }

        public async Task<int> Add(Lecture lecture)
        {
            throw new System.NotImplementedException();
        }

        public async Task<int> Update(Lecture lecture)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> Delete(Lecture lecture)
        {
            throw new System.NotImplementedException();
        }
    }
}