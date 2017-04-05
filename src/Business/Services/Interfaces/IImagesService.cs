using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Models;

namespace Business.Services.Interfaces
{
    public interface IImagesService
    {
        Task<Image> GetSingle(int id);
        Task<IEnumerable<Image>> Get(ImageFilter filter);
        Task<int> Add(Image image);
        Task<bool> Update(Image image);
        Task<bool> Delete(Image image);
    }
}