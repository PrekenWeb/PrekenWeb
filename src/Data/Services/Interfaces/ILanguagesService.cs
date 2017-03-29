using System.Collections.Generic;
using System.Threading.Tasks;
using PrekenWeb.Data.ViewModels;

namespace PrekenWeb.Data.Services.Interfaces
{
    public interface ILanguagesService
    {
        Task<Language> GetDefault();
        Task<IEnumerable<Language>> Get(LanguageFilter filter);
        Task<Language> GetSingle(int id);
        Task<int> Add(Language language);
        Task<int> Update(Language language);
        Task<bool> Delete(Language language);
    }
}