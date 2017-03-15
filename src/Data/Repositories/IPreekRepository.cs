using System.Collections.Generic;
using System.Threading.Tasks;
using PrekenWeb.Data.Tables;

namespace PrekenWeb.Data.Repositories
{
    public interface IPreekRepository
    {
        Task<Preek> GetSingle(int id);
        Task<IEnumerable<Preek>> Get(SermonDataFilter filter);
    }
}