using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Models;

namespace Business.Services.Interfaces
{
    public interface IBookChaptersService
    {
        Task<BookChapter> GetSingle(int id);
        Task<IEnumerable<BookChapter>> Get(BookChapterFilter filter);
        Task<int> Add(BookChapter bookChapter);
        Task<bool> Update(BookChapter bookChapter);
        Task<bool> Delete(BookChapter bookChapter);
    }
}