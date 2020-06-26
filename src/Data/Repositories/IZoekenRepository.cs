using System;
using System.Linq;
using System.Threading.Tasks;
using Data.Tables;

namespace Data.Repositories
{
    public interface IZoekenRepository
    { 
        IQueryable<Preek> GetPrekenQueryable(int taalId, bool audioPreken, bool videoPreken, bool leesPreken, bool lezingen, bool meditaties);
        Task<Predikant> GetPredikantById(int predikantId, int taalId);
        Task<Gebeurtenis> GetGebeurtenisById(int gebeurtenisId, int taalId);
        Task<Boek> GetBoekById(int boekId, int taalId);
        Task<BoekHoofdstuk> GetBoekhoofdstukById(int boekhoofdstukId);
        Task<Serie> GetSerieById(int serieId, int taalId);
        Task<Gemeente> GetGemeenteById(int gemeenteId);
        Task<LezingCategorie> GetLezingCategorieId(int lezingCategorieId);
        Task<ZoekOpdracht> GetZoekopdrachtById(Guid id);
    }
}