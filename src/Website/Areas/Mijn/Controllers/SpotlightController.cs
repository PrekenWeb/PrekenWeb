using Prekenweb.Models;
using Prekenweb.Models.Identity;
using Prekenweb.Website.Areas.Mijn.Models;
using Prekenweb.Website.Controllers;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace Prekenweb.Website.Areas.Mijn.Controllers
{
    [Authorize(Roles = "Spotlight")]
    public class SpotlightController : ApplicationController
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
                Spotlights = _context.Spotlights.Where(sl => sl.TaalId == TaalId).OrderBy(sl => sl.Sortering).ToList()
            });
        }

        public ActionResult Verwijder(int id)
        {
            var spotlight = _context.Spotlights.Single(sl => sl.Id == id);

            _context.Spotlights.Remove(spotlight);
            _context.SaveChanges();

            ClearOutputCaches();

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

            ClearOutputCaches();

            return View(viewModel);
        }

        public ActionResult Maak()
        {
            return View(new SpotlightEditViewModel
            {
                Spotlight = new Spotlight { TaalId = TaalId }
            });
        }

        [HttpPost]
        public ActionResult Maak(SpotlightEditViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Spotlights.Add(viewModel.Spotlight);
                _context.SaveChanges();

                ClearOutputCaches();

                return RedirectToAction("Bewerk", new {viewModel.Spotlight.Id });
            }

            return View(viewModel);
        }
    }
}
