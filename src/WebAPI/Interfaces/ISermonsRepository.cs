using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Interfaces
{
    [Obsolete("Should not be used anymore, succeeded by (I)LecturesRepository")]
    public interface ISermonsRepository : IRepository<SermonEditModel, SermonViewModel, SermonFilterModel>
    {
        Task<IEnumerable<SermonViewModel>> GetNew(SermonFilterModel filter);
    }
}