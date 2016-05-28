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
    [Authorize(Roles = "Stamgegevens")]
    public class GelegenheidController : Controller
    {
        private readonly IPrekenwebContext<Gebruiker> _context;

        public GelegenheidController(IPrekenwebContext<Gebruiker> context)
        {
            _context = context;
        } 

        public ActionResult Index()
        {
            var taalId = TaalInfoHelper.FromRouteData(RouteData).Id;
            return View(new GebeurtenisIndexViewModel
            {
                Gebeurteniss = _context.Gebeurtenis.Where(g => g.TaalId == taalId).OrderBy(g => g.Sortering).ThenBy(g => g.Omschrijving).ToList()
            });
        }

        public ActionResult Verwijder(int id)
        {
            var gebeurtenis = _context.Gebeurtenis.Single(g => g.Id == id);
            _context.Gebeurtenis.Remove(gebeurtenis);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Bewerk(int id)
        {
            var gebeurtenis = _context.Gebeurtenis.Single(g => g.Id == id);
            return View(new GebeurtenisEditViewModel
            {
                Gebeurtenis = gebeurtenis
            });
        }

        [HttpPost]
        public ActionResult Bewerk(GebeurtenisEditViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            _context.Entry(viewModel.Gebeurtenis).State = EntityState.Modified;
            _context.SaveChanges();

            return View(viewModel);
        }

        public ActionResult Maak()
        {
            return View(new GebeurtenisEditViewModel
            {
                Gebeurtenis = new Gebeurtenis { TaalId = TaalInfoHelper.FromRouteData(RouteData).Id }
            });
        }

        [HttpPost]
        public ActionResult Maak(GebeurtenisEditViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            _context.Gebeurtenis.Add(viewModel.Gebeurtenis);
            _context.SaveChanges();

            return RedirectToAction("Bewerk", new { viewModel.Gebeurtenis.Id });
        }
    }
}
