using System.Collections.Generic;
using Data.Tables;
using Data.ViewModels;

namespace Data.Repositories
{
    public interface ITekstRepository : IPrekenWebRepository
    {
        TekstPagina GetTekstPagina(string identifier, int taalId);
        IList<Tekst> GetHomepageTeksten(int taalId);
    }
}