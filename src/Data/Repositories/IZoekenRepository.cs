using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrekenWeb.Data.Tables;

namespace PrekenWeb.Data.Repositories
{
    public interface IZoekenRepository
    { 
        IQueryable<Preek> GetPrekenQueryable(int taalId, IList<int> preekTypeIds);
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