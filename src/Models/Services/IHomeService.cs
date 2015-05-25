using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prekenweb.Models.Services
{
    public interface IHomeService
    {
        Task<IEnumerable<Dtos.Preek>> NieuwePreken(int taalId);
    }
}