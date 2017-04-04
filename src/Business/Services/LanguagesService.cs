using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Business.Models;
using Business.Services.Interfaces;
using Data.Database.Dapper.Gateways;
using Data.Database.Dapper.Models;

namespace Business.Services
{
    public class LanguagesService : ILanguagesService
    {
        private readonly IMapper _mapper;
        private readonly ILanguagesGateway _languagesGateway;

        public LanguagesService(IMapper mapper, ILanguagesGateway languagesGateway)
        {
            _mapper = mapper;
            _languagesGateway = languagesGateway;
        }

        public async Task<Language> GetDefault()
        {
            var languages = await _languagesGateway.Get(new LanguageDataFilter());

            var languageData = languages?.FirstOrDefault();
            if (languageData == null)
                return null;

            var defaultLanguage = _mapper.Map<LanguageData, Language>(languageData);
            return defaultLanguage;
        }

        public async Task<IEnumerable<Language>> Get(LanguageFilter filter)
        {
            var dataFilter = _mapper.Map<LanguageFilter, LanguageDataFilter>(filter);
            var languagesData = await _languagesGateway.Get(dataFilter);
            var languages = _mapper.Map<IEnumerable<LanguageData>, IEnumerable<Language>>(languagesData);
            return languages;
        }

        public async Task<Language> GetSingle(int id)
        {
            var languageData = await _languagesGateway.GetSingle(id);
            var language = _mapper.Map<LanguageData, Language>(languageData);
            return language;
        }

        public async Task<int> Add(Language language)
        {
            var languageData = _mapper.Map<Language, LanguageData>(language);
            return await _languagesGateway.Add(languageData);
        }

        public async Task<bool> Update(Language language)
        {
            var languageData = _mapper.Map<Language, LanguageData>(language);
            return await _languagesGateway.Update(languageData);
        }

        public async Task<bool> Delete(Language language)
        {
            var languageData = _mapper.Map<Language, LanguageData>(language);
            return await _languagesGateway.Delete(languageData);
        }
    }
}