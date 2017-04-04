using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Models;

namespace Business.Services.Interfaces
{
    public interface ILanguagesService
    {
        Task<Language> GetDefault();
        Task<IEnumerable<Language>> Get(LanguageFilter filter);
        Task<Language> GetSingle(int id);
        Task<int> Add(Language language);
        Task<bool> Update(Language language);
        Task<bool> Delete(Language language);
    }
}