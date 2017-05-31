using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Business.Models;
using Business.Services.Interfaces;
using Data.Database.Dapper.Interfaces.Gateways;
using Data.Database.Dapper.Models;

namespace Business.Services
{
    internal class LanguagesService : Service<Language, LanguageFilter, LanguageData, LanguageDataFilter>, ILanguagesService
    {
        public LanguagesService(IMapper mapper, ILanguagesGateway languagesGateway)
            : base(mapper, languagesGateway)
        {
        }

        public async Task<Language> GetDefault()
        {
            var languages = await Gateway.Get(new LanguageDataFilter());

            var languageData = languages?.FirstOrDefault();
            if (languageData == null)
                return null;

            var defaultLanguage = Mapper.Map<LanguageData, Language>(languageData);
            return defaultLanguage;
        }
    }
}