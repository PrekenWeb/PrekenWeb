using System.Collections.Generic;
using System.Threading.Tasks;
using PrekenWeb.Data.ViewModels;

namespace PrekenWeb.Data.Services.Interfaces
{
    public interface ISermonsService
    {
        Task<SermonModel> GetSingle(int id);
        Task<IEnumerable<SermonModel>> Get(SermonFilter filter);
        Task<IEnumerable<SermonModel>> GetNew(SermonFilter filter);
    }
}