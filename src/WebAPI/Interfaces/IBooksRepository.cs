using WebAPI.Models;

namespace WebAPI.Interfaces
{
    public interface IBooksRepository : IRepository<BookViewModel, BookFilterModel>
    {
    }
}