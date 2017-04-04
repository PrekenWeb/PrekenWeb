using System.Collections.Generic;
using System.Linq;
using Data.Identity;
using Data.Tables;

namespace Data.Repositories
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
