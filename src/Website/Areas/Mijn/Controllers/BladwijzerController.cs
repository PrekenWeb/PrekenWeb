using Prekenweb.Models.Repository;
using Prekenweb.Website.Areas.Mijn.Models;
using Prekenweb.Website.Controllers;
using System.Linq;
using System.Web.Mvc;
using Prekenweb.Models.Services;
using Prekenweb.Website.Lib;

namespace Prekenweb.Website.Areas.Mijn.Controllers
{
    public class BladwijzerController : ApplicationController
    {
        private readonly IGebruikerRepository _gebruikerRepository;
        private readonly IHuidigeGebruiker _huidigeGebruiker;

        public BladwijzerController(IGebruikerRepository gebruikerRepository,
            IHuidigeGebruiker huidigeGebruiker)
        {
            _gebruikerRepository = gebruikerRepository;
            _huidigeGebruiker = huidigeGebruiker;
        }

        [Authorize]
        public ActionResult DoorMijBeluisterd()
        {
            var resultaten = _gebruikerRepository
                .GetBeluisterdePreken(_huidigeGebruiker.Id, TaalId)
                .Select(p => new ZoekresultaatItem
                {
                    Preek = p.Preek,
                    ResultaatReden = ResultaatReden.Beluisterd,
                    Cookie = p
                }); 

            return View(new DoorMijBeluisterd
            {
                Zoekresultaat = new Zoekresultaat
                {
                    Items = resultaten
                }
            });
        }

        [Authorize]
        public ActionResult MetBladwijzer()
        {
            var resultaten =  _gebruikerRepository
                .GetPrekenMetBladwijzer(_huidigeGebruiker.Id, TaalId)
                .Select(p => new ZoekresultaatItem
                {
                    Preek = p.Preek,
                    ResultaatReden = ResultaatReden.MetBladwijzer,
                    Cookie = p
                }); 

            return View(new MetBladwijzer
            {
                Zoekresultaat = new Zoekresultaat
                {
                    Items = resultaten
                }
            });
        }

    }
}
