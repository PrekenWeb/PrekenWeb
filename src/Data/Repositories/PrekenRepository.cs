using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Data.Identity;
using Data.Tables;
using Data.ViewModels;

namespace Data.Repositories
{
    public class PrekenRepository : PrekenWebRepositoryBase, IPrekenRepository
    {
        public PrekenRepository(IPrekenwebContext<Gebruiker> prekenWebContext)
            : base(prekenWebContext)
        {
        }

        public async Task<IList<ZoekresultaatItem>> GetNieuwePreken(ICollection<int> preekTypIds, int taalId, int gebruikerId)
        {
            return (await Context
                .Preeks
                .Include(x => x.Predikant)
                .Include(x => x.Gemeente)
                .Include(x => x.BoekHoofdstuk)
                .Include(x => x.BoekHoofdstuk.Boek)
                .Include(x => x.PreekType)
                .Where(p => p.TaalId == taalId)
                .Where(p => preekTypIds.Contains(p.PreekTypeId) || (preekTypIds.Contains((int)PreekTypeEnum.VideoPreek) && p.Video != null))
                .Where(p => p.Gepubliceerd)
                .OrderByDescending(p => p.DatumGepubliceerd)
                .Take(10)
                .ToListAsync())
                .Select(p => new ZoekresultaatItem
                {
                    Preek = p,
                    ResultaatReden = ResultaatReden.Nieuw,
                })
                .ToList();
        }

        public async Task<IList<Preek>> GetPrekenForItunesPodcast(int taalId)
        {
            return await Context.Preeks.Where(p =>
                p.PreekTypeId != (int)PreekTypeEnum.LeesPreek
                && p.Gepubliceerd
                && p.TaalId == taalId
                && p.Duur.HasValue)
                .ToListAsync();
        } 
    }
}
