using Prekenweb.Models;
using Prekenweb.Models.Identity;
using Prekenweb.Website.Areas.Mijn.Models;
using Prekenweb.Website.Controllers;
using Prekenweb.Website.ViewModels;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace Prekenweb.Website.Areas.Mijn.Controllers
{
    [Authorize]
    public class HomeController : ApplicationController
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
                NieuwsbriefInschrijvingen = _context.NieuwsbriefInschrijvings.Where(ni => ni.TaalId == TaalId).ToList()
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
