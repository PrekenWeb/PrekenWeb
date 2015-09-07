using System.Threading.Tasks;
using PrekenWeb.Data;
using PrekenWeb.Data.Identity;
using PrekenWeb.Security;
using Prekenweb.Website.Areas.Mijn.Models;
using Prekenweb.Website.Controllers;
using System.Linq;
using System.Web.Mvc;
using Prekenweb.Models;
using System.Web.Routing;

namespace Prekenweb.Website.Areas.Mijn.Controllers
{
    public class ZoekOpdrachtController : ApplicationController
    {
        private readonly IPrekenwebContext<Gebruiker> _context;
        private readonly IHuidigeGebruiker _huidigeGebruiker;
        private readonly IPrekenWebUserManager _prekenWebUserManager;

        public ZoekOpdrachtController(IPrekenwebContext<Gebruiker> context,
            IHuidigeGebruiker huidigeGebruiker,
            IPrekenWebUserManager prekenWebUserManager)
        {
            _context = context;
            _huidigeGebruiker = huidigeGebruiker;
            _prekenWebUserManager = prekenWebUserManager;
        }

        [Authorize]
        public async Task<ActionResult> OpgeslagenZoekOpdrachten()
        {
            var gebruikerId = await _huidigeGebruiker.GetId(_prekenWebUserManager, User);
            var viewModel = new OpgeslagenZoekOpdrachten
            {
                ZoekOpdrachten = _context.ZoekOpdrachten.Where(x => x.GebruikerId == gebruikerId).OrderByDescending(x => x.Id)
            };

            return View(viewModel);
        }

        [Authorize]
        public async Task<RedirectToRouteResult> RedirectToZoekOpdracht(int id)
        {
            var gebruikerId = await _huidigeGebruiker.GetId(_prekenWebUserManager, User);
            var opdracht = _context.ZoekOpdrachten.Single(x => x.Id == id && x.GebruikerId == gebruikerId);
            var route = new RouteValueDictionary(opdracht);
            route.Add("Area", "Website");
            route.Remove("Gebruiker");
            route.Remove("PreekTypeIds");
            route.Remove("Omschrijving");
            return RedirectToAction("Index", "Zoeken", route);
        }
    }
}
