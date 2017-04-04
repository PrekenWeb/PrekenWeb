using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Models;

namespace Business.Services.Interfaces
{
    public interface ILecturesService
    {
        Task<IEnumerable<Lecture>> Get(LectureFilter filter);
        Task<Lecture> GetSingle(int id);
        Task<int> Add(Lecture lecture);
        Task<bool> Update(Lecture lecture);
        Task<bool> Delete(Lecture lecture);
    }
}