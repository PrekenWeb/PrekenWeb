using Prekenweb.Models;
using Prekenweb.Models.Identity;
using Prekenweb.Website.Controllers;
using Prekenweb.Website.Lib;
using Prekenweb.Website.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace Prekenweb.Website.Areas.Website.Controllers
{
    public class TooltipController : ApplicationController
    {
        private readonly IPrekenwebContext<Gebruiker> _context;

        public TooltipController(IPrekenwebContext<Gebruiker> context)
        {
            _context = context;
        } 

        public ActionResult Predikant(int id, int? preekId)
        {
            return PartialView(new TooltipBase<Predikant>
            {
                DataObject = _context.Predikants.Single(p => p.Id == id),
                Preek = preekId.HasValue ? _context.Preeks.Single(p => p.Id == preekId) : null
            });
        }
        public ActionResult BoekHoofdstuk(int id, int? preekId)
        {
            return PartialView(new TooltipBase<BoekHoofdstuk>
            {
                DataObject = _context.BoekHoofdstuks.Single(bh => bh.Id == id),
                Preek = preekId.HasValue ? _context.Preeks.Single(p => p.Id == preekId) : null
            });
        }
        public ActionResult Gebeurtenis(int id, int? preekId)
        {
            return PartialView(new TooltipBase<Gebeurtenis>
            {
                DataObject = _context.Gebeurtenis.Single(g => g.Id == id),
                Preek = preekId.HasValue ? _context.Preeks.Single(p => p.Id == preekId) : null
            });
        }
        public ActionResult Serie(int id, int? preekId)
        {
            return PartialView(new TooltipBase<Serie>
            {
                DataObject = _context.Series.Single(s => s.Id == id),
                Preek = preekId.HasValue ? _context.Preeks.Single(p => p.Id == preekId) : null
            });
        }
        public ActionResult PreekLink(Preek preek)
        {
            return PartialView(preek);
        }
         
    }
}
