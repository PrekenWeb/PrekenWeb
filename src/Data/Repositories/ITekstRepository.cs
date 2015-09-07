using System.Collections.Generic;
using PrekenWeb.Data.Tables;
using PrekenWeb.Data.ViewModels;

namespace PrekenWeb.Data.Repositories
{
    public interface ITekstRepository : IPrekenWebRepository
    {
        TekstPagina GetTekstPagina(string identifier, int taalId);
        IList<Tekst> GetHomepageTeksten(int taalId);
    }
}