using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Tables;

namespace Data.Repositories
{
    public interface IPreekRepository
    {
        Task<Preek> GetSingle(int id);
        Task<IEnumerable<Preek>> Get(SermonDataFilter filter);
    }
}