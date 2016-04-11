using PrekenWeb.Data;
using PrekenWeb.Data.Identity;
using Prekenweb.Website.Areas.Mijn.Models;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Prekenweb.Website.Areas.Website.Models;
using Prekenweb.Website.Lib;

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
            return View(new NieuwsbriefIndexViewModel
            {
                NieuwsbriefInschrijvingen = _context.NieuwsbriefInschrijvings.Where(ni => ni.TaalId == TaalInfoHelper.FromRouteData(RouteData).Id).ToList()
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

    }
}
