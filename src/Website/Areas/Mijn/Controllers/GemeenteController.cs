using PrekenWeb.Data;
using PrekenWeb.Data.Identity;
using PrekenWeb.Data.Tables;
using Prekenweb.Website.Areas.Mijn.Models;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace Prekenweb.Website.Areas.Mijn.Controllers
{
    [Authorize(Roles = "Stamgegevens")]
    public class GemeenteController : Controller
    {
        private readonly IPrekenwebContext<Gebruiker> _context;

        public GemeenteController(IPrekenwebContext<Gebruiker> context)
        {
            _context = context;
        } 

        public ActionResult Index()
        {
            return View(new GemeenteIndexViewModel
            {
                Gemeentes = _context.Gemeentes.OrderBy(g => g.Omschrijving).ToList()
            });
        }

        public ActionResult Verwijder(int id)
        {
            var gemeente = _context.Gemeentes.Single(g => g.Id == id);

            _context.Gemeentes.Remove(gemeente);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Bewerk(int id)
        {
            var gemeente = _context.Gemeentes.Single(g => g.Id == id);

            return View(new GemeenteEditViewModel
            {
                Gemeente = gemeente
            });
        }

        [HttpPost]
        public ActionResult Bewerk(GemeenteEditViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            _context.Entry(viewModel.Gemeente).State = EntityState.Modified;
            _context.SaveChanges();

            return View(viewModel);
        }

        public ActionResult Maak()
        {
            return View(new GemeenteEditViewModel
            {
                Gemeente = new Gemeente()
            });
        }

        [HttpPost]
        public ActionResult Maak(GemeenteEditViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            _context.Gemeentes.Add(viewModel.Gemeente);
            _context.SaveChanges();

            return RedirectToAction("Bewerk", new { viewModel.Gemeente.Id });
        }
    }
}
