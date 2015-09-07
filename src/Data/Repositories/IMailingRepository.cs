using System.Collections.Generic;
using System.Threading.Tasks;
using PrekenWeb.Data.Tables;

namespace PrekenWeb.Data.Repositories
{
    public interface IMailingRepository : IPrekenWebRepository
    {
        Task NieuwsbrievenOverschrijvenBijGebruiker(int gebruikerId, int[] nieuwsBriefIds);
        Task NieuwsbriefToevoegenAanGebruiker(int gebruikerId, int nieuwsBriefId);
        Task NieuwsbriefOntkoppelen(int gebruikerId, int nieuwsBriefId);
        Task<IEnumerable<Mailing>> GetAlleMailings();
    }
}