using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Models;

namespace Business.Services.Interfaces
{
    public interface ILectureTypesService
    {
        Task<LectureType> GetSingle(int id);
        Task<IEnumerable<LectureType>> Get(LectureTypeFilter filter);
        Task<int> Add(LectureType lectureType);
        Task<bool> Update(LectureType lectureType);
        Task<bool> Delete(LectureType lectureType);
    }
}