using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Interfaces
{
    public interface ILanguagesRepository : IRepository<LanguageViewModel, LanguageFilterModel>
    {
        Task<LanguageViewModel> GetDefault();
    }
}