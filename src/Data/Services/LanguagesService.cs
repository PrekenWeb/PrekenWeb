using System.Collections.Generic;
using System.Threading.Tasks;
using PrekenWeb.Data.Services.Interfaces;
using PrekenWeb.Data.ViewModels;

namespace PrekenWeb.Data.Services
{
    public class LanguagesService : ILanguagesService
    {
        public async Task<Language> GetDefault()
        {
            return new Language { Id = 1 }; // TODO get default language from database
        }

        public async Task<IEnumerable<Language>> Get(LanguageFilter filter)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Language> GetSingle(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<int> Add(Language language)
        {
            throw new System.NotImplementedException();
        }

        public async Task<int> Update(Language language)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> Delete(Language language)
        {
            throw new System.NotImplementedException();
        }
    }
}