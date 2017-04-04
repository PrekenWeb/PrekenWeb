using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Tables;
using Data.ViewModels;

namespace Data.Repositories
{
    public interface IPrekenRepository
    {
        Task<IList<ZoekresultaatItem>> GetNieuwePreken(ICollection<int> preekTypIds, int taalId, int gebruikerId);
        Task<IList<Preek>> GetPrekenForItunesPodcast(int taalId);
    }
}