using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Models;

namespace Business.Services.Interfaces
{
    public interface ISermonsService
    {
        Task<Lecture> GetSingle(int id);
        Task<IEnumerable<Lecture>> Get(LectureFilter filter);
        Task<IEnumerable<Lecture>> GetNew(LectureFilter filter);
        Task<int> Add(Lecture lecture);
        Task<int> Update(Lecture lecture);
        Task<bool> Delete(Lecture lecture);
    }
}