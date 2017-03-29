using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Interfaces
{
    public interface ISermonsRepository : IRepository<SermonEditModel, SermonViewModel, SermonFilterModel>
    {
        Task<IEnumerable<SermonViewModel>> GetNew(SermonFilterModel filter);
    }
}