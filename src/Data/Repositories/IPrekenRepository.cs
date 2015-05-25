using System.Collections.Generic;
using System.Threading.Tasks;
using PrekenWeb.Data.Tables;
using PrekenWeb.Data.ViewModels;

namespace PrekenWeb.Data.Repositories
{
    public interface IPrekenRepository
    {
        Task<IList<ZoekresultaatItem>> GetNieuwePreken(ICollection<int> preekTypIds, int taalId, int gebruikerId);
        Task<IList<Preek>> GetPrekenForItunesPodcast(int taalId);
    }
}