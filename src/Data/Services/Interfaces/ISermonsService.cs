using System.Collections.Generic;
using System.Threading.Tasks;
using PrekenWeb.Data.ViewModels;

namespace PrekenWeb.Data.Services.Interfaces
{
    public interface ISermonsService
    {
        Task<Sermon> GetSingle(int id);
        Task<IEnumerable<Sermon>> Get(SermonFilter filter);
        Task<IEnumerable<Sermon>> GetNew(SermonFilter filter);
        Task<int> Add(Sermon sermon);
        Task<int> Update(Sermon sermon);
        Task<bool> Delete(Sermon sermon);
    }
}