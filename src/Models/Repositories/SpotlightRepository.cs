using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Prekenweb.Models.Identity;
using Prekenweb.Models.ViewModels;

namespace Prekenweb.Models.Repository
{
    public interface ISpotlightRepository : IPrekenWebRepository
    {
        IList<Spotlight> GetSpotlightItemsForHomepage(int taalId);
        Afbeelding GetAfbeelding(int afbeeldingId);
    }

    public class SpotlightRepository : PrekenWebRepositoryBase, ISpotlightRepository
    {
        public SpotlightRepository(IPrekenwebContext<Gebruiker> prekenWebContext)
            : base(prekenWebContext)
        {
        }

        public IList<Spotlight> GetSpotlightItemsForHomepage(int taalId)
        {
            return Context
                .Spotlights
                .Where(sp => sp.TaalId == taalId)
                .OrderBy(sp => sp.Sortering)
                .ToList();
        }

        public Afbeelding GetAfbeelding(int afbeeldingId)
        {
            return Context.Afbeeldings.Single(a => a.Id == afbeeldingId);
        }
    }
}
