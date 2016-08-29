using PrekenWeb.Data;
using PrekenWeb.Data.Identity;
using Prekenweb.Website.Areas.Mijn.Models;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Hangfire;
using Prekenweb.Website.Areas.Website.Models;
using Prekenweb.Website.Lib;
using Prekenweb.Website.Lib.Hangfire;

namespace Prekenweb.Website.Areas.Mijn.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IPrekenwebContext<Gebruiker> _context;

        public HomeController(IPrekenwebContext<Gebruiker> context)
        {
            _context = context;
        } 

        public ActionResult Index()
        {
            return View(new HomeMijn
            {
                Inbox = _context.Inboxes.Include(x => x.InboxType).OrderByDescending(i => i.Aangemaakt).ToList()
            });
        }

        public ActionResult Nieuwsbrief()
        {
            var taalId = TaalInfoHelper.FromRouteData(RouteData).Id;
            return View(new NieuwsbriefIndexViewModel
            {
                NieuwsbriefInschrijvingen = _context.NieuwsbriefInschrijvings.Where(ni => ni.TaalId == taalId).ToList()
            });
        }
        public ActionResult Stamgegevens()
        {
            return View();
        }

        [Authorize(Roles = "Bestandsbeheer,BestandsbeheerPrekenWeb")]
        public ActionResult Bestandsbeheer()
        {
            return View();
        }

        public ActionResult HerstelWerkzaamheden()
        {
            BackgroundJob.Enqueue<AchtergrondTaken>(x => x.HerstelWerkzaamheden());
            return RedirectToAction("Index");
        }

    }
}
