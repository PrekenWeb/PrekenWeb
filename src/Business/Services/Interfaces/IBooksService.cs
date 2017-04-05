using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Models;

namespace Business.Services.Interfaces
{
    public interface IBooksService
    {
        Task<Book> GetSingle(int id);
        Task<IEnumerable<Book>> Get(BookFilter filter);
        Task<int> Add(Book book);
        Task<bool> Update(Book book);
        Task<bool> Delete(Book book);
    }
}