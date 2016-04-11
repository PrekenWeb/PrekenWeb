using PrekenWeb.Data;
using PrekenWeb.Data.Identity;
using PrekenWeb.Data.Tables;
using Prekenweb.Website.Areas.Mijn.Models;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Prekenweb.Website.Lib;

namespace Prekenweb.Website.Areas.Mijn.Controllers
{
    [Authorize(Roles = "Spotlight")]
    public class SpotlightController : Controller
    {
        private readonly IPrekenwebContext<Gebruiker> _context;

        public SpotlightController(IPrekenwebContext<Gebruiker> context)
        {
            _context = context;
        } 

        public ActionResult Index()
        {
            return View(new SpotlightIndexViewModel
            {
                Spotlights = _context.Spotlights.Where(sl => sl.TaalId == TaalInfoHelper.FromRouteData(RouteData).Id).OrderBy(sl => sl.Sortering).ToList()
            });
        }

        public ActionResult Verwijder(int id)
        {
            var spotlight = _context.Spotlights.Single(sl => sl.Id == id);

            _context.Spotlights.Remove(spotlight);
            _context.SaveChanges();

            OutputCacheHelpers.ClearOutputCaches(Response, Url);

            return RedirectToAction("Index");
        }

        public ActionResult Bewerk(int id)
        {
            var spotlight = _context.Spotlights.Single(sl => sl.Id == id);
            return View(new SpotlightEditViewModel
            {
                Spotlight = spotlight
            });
        }

        [HttpPost]
        public ActionResult Bewerk(SpotlightEditViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(viewModel.Spotlight).State = EntityState.Modified;
                _context.SaveChanges();
            }

            OutputCacheHelpers.ClearOutputCaches(Response, Url);

            return View(viewModel);
        }

        public ActionResult Maak()
        {
            return View(new SpotlightEditViewModel
            {
                Spotlight = new Spotlight { TaalId = TaalInfoHelper.FromRouteData(RouteData).Id }
            });
        }

        [HttpPost]
        public ActionResult Maak(SpotlightEditViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Spotlights.Add(viewModel.Spotlight);
                _context.SaveChanges();

                OutputCacheHelpers.ClearOutputCaches(Response, Url);

                return RedirectToAction("Bewerk", new {viewModel.Spotlight.Id });
            }

            return View(viewModel);
        }
    }
}
