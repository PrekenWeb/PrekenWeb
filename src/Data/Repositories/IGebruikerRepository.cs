using System.Collections.Generic;
using System.Threading.Tasks;
using PrekenWeb.Data.Tables;

namespace PrekenWeb.Data.Repositories
{
    public interface IGebruikerRepository : IPrekenWebRepository
    { 
        IEnumerable<PreekCookie> GetBeluisterdePreken(int gebruikerId, int taalId);
        Task<IEnumerable<PreekCookie>> GetPreekCookies(int gebruikerId, int[] preekIds);
        IEnumerable<PreekCookie> GetPrekenMetBladwijzer(int gebruikerId, int taalId);
        Task VerwijderGebruikerData(int gebruikerId);
        Task<IList<PreekCookie>> GetCookiesVoorGebruiker(int gebruikerId); 
        Task AddToRolesAsync(int gebruikerId, string[] roleNames);
        Task RemoveAllRolesAsync(int gebruikerId);
    }
}