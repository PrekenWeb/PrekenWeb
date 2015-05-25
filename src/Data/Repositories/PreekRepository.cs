using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using PrekenWeb.Data.Tables;

namespace PrekenWeb.Data.Repositories
{
    public class PreekRepository : PrekenWebRepositoryBase, IPreekRepository
    {
        public PreekRepository(PrekenwebContext prekenWebContext)
            : base(prekenWebContext)
        {
        }

        public async Task<IEnumerable<Preek>> GetAllePreken(int taalId)
        {
            Context.Configuration.ProxyCreationEnabled = false;  // to prevent proxy creation, proxies are not serializable by the API

            return await Context.Preeks 
                .Include(x => x.PreekCookies)
                .Include(x => x.Predikant)
                .Include(x => x.Gemeente)
                .Include(x => x.BoekHoofdstuk)
                .Include(x => x.BoekHoofdstuk.Boek)
                .Include(x => x.PreekType)
                .Where(p => p.TaalId == taalId) 
                .Where(p => p.Gepubliceerd)
                .OrderByDescending(x => x.DatumAangemaakt)
                .Take(10)
                .ToListAsync();
        }
    }
}
