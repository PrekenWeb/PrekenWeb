using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Models;

namespace Business.Services.Interfaces
{
    public interface ISermonsService
    {
        Task<Sermon> GetSingle(int id);
        Task<IEnumerable<Sermon>> Get(SermonFilter filter);
        Task<IEnumerable<Sermon>> GetNew(SermonFilter filter);
        Task<int> Add(Sermon lecture);
        Task<int> Update(Sermon lecture);
        Task<bool> Delete(Sermon lecture);
    }
}