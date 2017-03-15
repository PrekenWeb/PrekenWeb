using System.Collections.Generic;
using System.Threading.Tasks;
using Prekenweb.Models.Dtos;

namespace PrekenWeb.Data.Services.Interfaces
{
    public interface IHomeService
    {
        Task<IEnumerable<Preek>> NieuwePreken(int taalId);
    }
}