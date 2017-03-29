using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using PrekenWeb.Data.Services.Interfaces;
using PrekenWeb.Data.ViewModels;
using WebAPI.Common;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Repositories
{
    internal class SermonsRepository : ISermonsRepository
    {
        private readonly ILanguagesRepository _languagesRepository;

        private readonly ISermonsService _sermonsService;
        private readonly IMapper _mapper;

        public SermonsRepository(ILanguagesRepository languagesRepository, ISermonsService sermonsService, IMapper mapper)
        {
            _sermonsService = sermonsService;
            _mapper = mapper;
            _languagesRepository = languagesRepository;
        }

        public async Task<IEnumerable<SermonViewModel>> Get(SermonFilterModel filterModel)
        {
            filterModel = await EnsureLanguage(filterModel);

            var filter = _mapper.Map<SermonFilter>(filterModel);
            var sermons = _sermonsService.Get(filter);
            return _mapper.Map<IEnumerable<SermonViewModel>>(sermons);
        }

        public async Task<IEnumerable<SermonViewModel>> GetNew(SermonFilterModel filterModel)
        {
            filterModel = await EnsureLanguage(filterModel);

            var filter = _mapper.Map<SermonFilter>(filterModel);
            var sermons = await _sermonsService.GetNew(filter);
            return _mapper.Map<IEnumerable<SermonViewModel>>(sermons);
        }

        public async Task<SermonViewModel> GetSingle(int id)
        {
            var sermon = await _sermonsService.GetSingle(id);
            if (sermon == null) throw new ItemNotFoundException();
            return _mapper.Map<SermonViewModel>(sermon);
        }

        public async Task<int> Add(SermonEditModel sermonModel)
        {
            var sermon = _mapper.Map<Sermon>(sermonModel);
            return await _sermonsService.Add(sermon);
        }

        public async Task<int> Update(SermonEditModel sermonModel)
        {
            var existing = _sermonsService.GetSingle(sermonModel.Id);
            if (existing == null) throw new ItemNotFoundException();

            var sermon = _mapper.Map<Sermon>(sermonModel);
            return await _sermonsService.Update(sermon);
        }

        public async Task<bool> Delete(int id)
        {
            var existing = await _sermonsService.GetSingle(id);
            if (existing == null) throw new ItemNotFoundException();
            return await _sermonsService.Delete(existing);
        }

        // private methods
        #region EnsureLanguage

        private async Task<SermonFilterModel> EnsureLanguage(SermonFilterModel filter)
        {
            if (filter?.LanguageId != null)
                return filter;

            if (filter == null)
                filter = new SermonFilterModel();

            var defaultLanguage = await _languagesRepository.GetDefault();
            filter.LanguageId = defaultLanguage.Id;
            return filter;
        }

        #endregion
    }
}