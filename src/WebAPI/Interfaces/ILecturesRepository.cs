using WebAPI.Models;

namespace WebAPI.Interfaces
{
    public interface ILecturesRepository : IRepository<LectureViewModel, LectureFilterModel>
    {
    }
}