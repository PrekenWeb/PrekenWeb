using System.Threading.Tasks;
using Prekenweb.Models.Repository;
using PrekenWeb.Security;
using Prekenweb.Website.Areas.Mijn.Models;
using Prekenweb.Website.Controllers;
using System.Linq;
using System.Web.Mvc;
using Prekenweb.Models.Services;

namespace Prekenweb.Website.Areas.Mijn.Controllers
{
    public class BladwijzerController : ApplicationController
    {
        private readonly IGebruikerRepository _gebruikerRepository;
        private readonly IHuidigeGebruiker _huidigeGebruiker;
        private readonly IPrekenWebUserManager _prekenWebUserManager;

        public BladwijzerController(IGebruikerRepository gebruikerRepository,
            IHuidigeGebruiker huidigeGebruiker,
            IPrekenWebUserManager prekenWebUserManager)
        {
            _gebruikerRepository = gebruikerRepository;
            _huidigeGebruiker = huidigeGebruiker;
            _prekenWebUserManager = prekenWebUserManager;
        }

        [Authorize]
        public async Task<ActionResult> DoorMijBeluisterd()
        { 
            var resultaten = _gebruikerRepository
                .GetBeluisterdePreken(await _huidigeGebruiker.GetId(_prekenWebUserManager, User), TaalId)
                .Select(p => new ZoekresultaatItem
                {
                    Preek = p.Preek,
                    ResultaatReden = ResultaatReden.Beluisterd,
                    //Cookie = p
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
        public async Task<ActionResult> MetBladwijzer()
        {
            var resultaten =  _gebruikerRepository
                .GetPrekenMetBladwijzer(await _huidigeGebruiker.GetId(_prekenWebUserManager, User), TaalId)
                .Select(p => new ZoekresultaatItem
                {
                    Preek = p.Preek,
                    ResultaatReden = ResultaatReden.MetBladwijzer,
                    //Cookie = p
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
