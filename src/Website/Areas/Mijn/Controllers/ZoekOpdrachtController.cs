using Prekenweb.Models.Identity;
using Prekenweb.Website.Areas.Mijn.Models;
using Prekenweb.Website.Controllers;
using System.Linq;
using System.Web.Mvc;
using Prekenweb.Models;
using System.Web.Routing;
using Prekenweb.Website.Lib;

namespace Prekenweb.Website.Areas.Mijn.Controllers
{
    public class ZoekOpdrachtController : ApplicationController
    {
        private readonly IPrekenwebContext<Gebruiker> _context;
        private readonly IHuidigeGebruiker _huidigeGebruiker;

        public ZoekOpdrachtController(IPrekenwebContext<Gebruiker> context,
            IHuidigeGebruiker huidigeGebruiker)
        {
            _context = context;
            _huidigeGebruiker = huidigeGebruiker;
        }

        [Authorize]
        public ActionResult OpgeslagenZoekOpdrachten()
        {
            var viewModel = new OpgeslagenZoekOpdrachten
            {
                ZoekOpdrachten = _context.ZoekOpdrachten.Where(x => x.GebruikerId == _huidigeGebruiker.Id).OrderByDescending(x => x.Id)
            };

            return View(viewModel);
        }

        [Authorize]
        public ActionResult RedirectToZoekOpdracht(int id)
        {
            var opdracht = _context.ZoekOpdrachten.Single(x => x.Id == id && x.GebruikerId == _huidigeGebruiker.Id);
            var route = new RouteValueDictionary(opdracht);
            route.Add("Area", "Website");
            route.Remove("Gebruiker");
            route.Remove("PreekTypeIds");
            route.Remove("Omschrijving");
            return RedirectToAction("Index", "Zoeken", route);
        }
    }
}
