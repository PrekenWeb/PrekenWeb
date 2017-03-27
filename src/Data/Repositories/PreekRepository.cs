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

        public async Task<Preek> GetSingle(int id)
        {
            Context.Configuration.ProxyCreationEnabled = false;  // to prevent proxy creation, proxies are not serializable by the API

            return await Context.Preeks
                .Include(x => x.PreekCookies)
                .Include(x => x.Predikant)
                .Include(x => x.Gemeente)
                .Include(x => x.BoekHoofdstuk)
                .Include(x => x.BoekHoofdstuk.Boek)
                .Include(x => x.PreekType)
                .Where(p => p.Gepubliceerd)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Preek>> Get(SermonDataFilter filter)
        {
            Context.Configuration.ProxyCreationEnabled = false;  // to prevent proxy creation, proxies are not serializable by the API

            var query = Context.Preeks

                .Include(preek => preek.PreekCookies)
                .Include(preek => preek.Predikant)
                .Include(preek => preek.Gemeente)
                .Include(preek => preek.BoekHoofdstuk)
                .Include(preek => preek.BoekHoofdstuk.Boek)
                .Include(preek => preek.PreekType)

                // Filter
                .Where(p => p.TaalId == filter.LanguageId)
                .Where(p => p.Gepubliceerd);


            //if (filter.SortBy != null)
            //{
            // Order
            var orderableQuery = query.OrderByDescending(x => x.DatumGepubliceerd);
            //query = filter.SortDirection == SortDirection.Ascending
            //    ? query.OrderByDescending(filter.SortBy)
            //    : query.OrderBy(filter.SortBy);

            // Paging
            query = orderableQuery
                .Skip(filter.Page * filter.PageSize)
                .Take(filter.PageSize);
            //}

            return await query
                .ToListAsync();
        }
    }
}
