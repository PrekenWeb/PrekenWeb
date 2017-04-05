using WebAPI.Models;

namespace WebAPI.Interfaces
{
    public interface IImagesRepository : IRepository<ImageViewModel, ImageFilterModel>
    {
    }
}