using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prekenweb.Models.Repository
{
    public interface IPreekRepository
    {
        Task<IEnumerable<Preek>> GetAllePreken(int taalId);
    }
}