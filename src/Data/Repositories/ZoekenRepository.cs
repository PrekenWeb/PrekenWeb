using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Data.Identity;
using Data.Tables;
using Data.ViewModels;

namespace Data.Repositories
{
    public class ZoekenRepository : PrekenWebRepositoryBase, IZoekenRepository
    {
        public ZoekenRepository(IPrekenwebContext<Gebruiker> prekenWebContext)
            : base(prekenWebContext)
        {
        }

        public IQueryable<Preek> GetPrekenQueryable(int taalId, bool audioPreken, bool videoPreken, bool leesPreken, bool lezingen, bool meditaties)
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
                            && ((audioPreken && p.PreekTypeId == (int)PreekTypeEnum.Preek && p.Video == null) ||
                                (videoPreken && p.PreekTypeId == (int)PreekTypeEnum.Preek && p.Video != null) ||
                                (leesPreken && p.PreekTypeId == (int)PreekTypeEnum.LeesPreek) ||
                                (lezingen && p.PreekTypeId == (int)PreekTypeEnum.Lezing) ||
                                (meditaties && p.PreekTypeId == (int)PreekTypeEnum.Meditatie)));
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
