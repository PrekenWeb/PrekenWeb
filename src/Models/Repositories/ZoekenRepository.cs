using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;
using Prekenweb.Models.Identity;

namespace Prekenweb.Models.Repository
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

    public class ZoekenRepository : PrekenWebRepositoryBase, IZoekenRepository
    {
        public ZoekenRepository(IPrekenwebContext<Gebruiker> prekenWebContext)
            : base(prekenWebContext)
        {
        }

        public IQueryable<Preek> GetPrekenQueryable(int taalId, IList<int> preekTypeIds)
        {
            return Context
                .Preeks
                .Include(x => x.Predikant)
                .Include(x => x.Gemeente)
                .Include(x => x.BoekHoofdstuk)
                .Include(x => x.BoekHoofdstuk.Boek)
                .Include(x => x.PreekType)
                .Include(x => x.Gebeurtenis)
                .Include(x => x.LezingCategorie)
                .Include(x => x.Serie)
                .Where(p => p.TaalId == taalId
                            && p.Gepubliceerd
                            && preekTypeIds.Contains(p.PreekTypeId));
        }

        public async Task<Predikant> GetPredikantById(int predikantId, int taalId)
        {
            return await Context
                .Predikants
                .Where(x => x.Id == predikantId)
                .Where(x => x.TaalId == taalId)
                .SingleOrDefaultAsync();
        }
        public async Task<Gebeurtenis> GetGebeurtenisById(int gebeurtenisId, int taalId)
        {
            return await Context
                .Gebeurtenis
                .Where(x => x.Id == gebeurtenisId)
                .Where(x => x.TaalId == taalId)
                .SingleOrDefaultAsync();
        }
        public async Task<Boek> GetBoekById(int boekId, int taalId)
        {
            return await Context
                .Boeks
                .Where(x => x.Id == boekId)
                .Where(x => x.TaalId == taalId)
                .SingleOrDefaultAsync();
        }
        public async Task<BoekHoofdstuk> GetBoekhoofdstukById(int boekhoofdstukId)
        {
            return await Context
                .BoekHoofdstuks
                .Where(x => x.Id == boekhoofdstukId) 
                .SingleOrDefaultAsync();
        }
        public async Task<Serie> GetSerieById(int serieId, int taalId)
        {
            return await Context
                .Series
                .Where(x => x.Id == serieId)
                .Where(x => x.TaalId == taalId)
                .SingleOrDefaultAsync();
        }
        public async Task<Gemeente> GetGemeenteById(int gemeenteId)
        {
            return await Context
                .Gemeentes
                .Where(x => x.Id == gemeenteId) 
                .SingleOrDefaultAsync();
        }
        public async Task<LezingCategorie> GetLezingCategorieId(int lezingCategorieId)
        {
            return await Context
                .LezingCategories
                .Where(x => x.Id == lezingCategorieId) 
                .SingleOrDefaultAsync();
        }

        public async Task<ZoekOpdracht> GetZoekopdrachtById(Guid zoekopdrachtId)
        {
            return await Context
                .ZoekOpdrachten
                .SingleOrDefaultAsync(x => x.PubliekeSleutel == zoekopdrachtId);  
        }
    }
}
