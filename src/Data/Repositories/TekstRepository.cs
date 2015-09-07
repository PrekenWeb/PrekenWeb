using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using PrekenWeb.Data.Identity;
using PrekenWeb.Data.Tables;
using PrekenWeb.Data.ViewModels;

namespace PrekenWeb.Data.Repositories
{
    public class TekstRepository : PrekenWebRepositoryBase, ITekstRepository
    {
        public TekstRepository(IPrekenwebContext<Gebruiker> prekenWebContext)
            : base(prekenWebContext)
        {
        }
        public TekstPagina GetTekstPagina(string identifier, int taalId)
        {
            var pagina = Context.Paginas.Include(x => x.Teksts).Single(p => p.Identifier == identifier);
            if (pagina.Teksts.Any(t => t.TaalId == taalId))
            {
                var tekst = pagina.Teksts.First(t => t.TaalId == taalId);
                return new TekstPagina
                {
                    HuidigeTaalId = taalId,
                    Kop = tekst.Kop,
                    PaginaTaalId = tekst.TaalId,
                    Tekst = tekst.Tekst_
                };
            }
            else
            {
                var tekst = pagina.Teksts.FirstOrDefault();
                if (tekst != null)
                    return new TekstPagina
                    {
                        HuidigeTaalId = taalId,
                        Kop = tekst.Kop,
                        PaginaTaalId = tekst.TaalId,
                      
                        Tekst = tekst.Tekst_
                    };
                return new TekstPagina
                {
                    HuidigeTaalId = taalId,
                    Kop = identifier,
                    PaginaTaalId = taalId,
                    Tekst = identifier
                };
            }
        }

        public IList<Tekst> GetHomepageTeksten(int taalId)
        {
            return Context
                      .Teksts 
                      .Where(t => t.TaalId == taalId && t.Pagina.TonenOpHomepage)
                      .OrderByDescending(t => t.Pagina.Bijgewerkt)
                      .Take(8)
                      .ToList();
        }
    }
}
