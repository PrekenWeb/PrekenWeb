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
    internal class LanguagesRepository : ILanguagesRepository
    {
        private readonly ILanguagesService _languagesService;
        private readonly IMapper _mapper;

        public LanguagesRepository(ILanguagesService languagesService, IMapper mapper)
        {
            _languagesService = languagesService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LanguageViewModel>> Get(LanguageFilterModel filterModel)
        {
            var filter = _mapper.Map<LanguageFilter>(filterModel);
            var languages = await _languagesService.Get(filter);
            return _mapper.Map<IEnumerable<LanguageViewModel>>(languages);
        }

        public async Task<LanguageViewModel> GetDefault()
        {
            var language = await _languagesService.GetDefault();
            return _mapper.Map<LanguageViewModel>(language);
        }

        public async Task<LanguageViewModel> GetSingle(int id)
        {
            var language = await _languagesService.GetSingle(id);
            if (language == null) throw new ItemNotFoundException();
            return _mapper.Map<LanguageViewModel>(language);
        }

        public async Task<int> Add(LanguageViewModel languageModel)
        {
            var language = _mapper.Map<Language>(languageModel);
            return await _languagesService.Add(language);
        }

        public async Task<int> Update(LanguageViewModel languageModel)
        {
            var existing = _languagesService.GetSingle(languageModel.Id);
            if (existing == null) throw new ItemNotFoundException();

            var language = _mapper.Map<Language>(languageModel);
            return await _languagesService.Update(language);
        }

        public async Task<bool> Delete(int id)
        {
            var existing = await _languagesService.GetSingle(id);
            if (existing == null) throw new ItemNotFoundException();
            return await _languagesService.Delete(existing);
        }
    }
}