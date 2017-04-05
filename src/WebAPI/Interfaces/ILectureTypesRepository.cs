using WebAPI.Models;

namespace WebAPI.Interfaces
{
    public interface ILectureTypesRepository : IRepository<LectureTypeViewModel, LectureTypeFilterModel>
    {
    }
}