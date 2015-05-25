using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using PrekenWeb.Data.Repositories;
using PrekenWeb.Data.Tables;
using Prekenweb.Models;
using PrekenWeb.Security;

namespace WebAPI.Controllers
{
    public class GebruikerController : ApiController
    {
        private readonly IGebruikerRepository _gebruikerRepository;
        private readonly IHuidigeGebruiker _huidigeGebruiker;
        private readonly IPrekenWebUserManager _userManager;

        public GebruikerController(IGebruikerRepository gebruikerRepository,IPrekenWebUserManager userManager, IHuidigeGebruiker huidigeGebruiker)
        {
            _gebruikerRepository = gebruikerRepository;
            _huidigeGebruiker = huidigeGebruiker;
            _userManager = userManager;
        }

        [Authorize] 
        [HttpGet]
        public async Task<IEnumerable<PreekCookie>> GeopendePreken([FromUri]int[] preekIds)
        {
            var gebruikerId = await _huidigeGebruiker.GetId(_userManager, User); 
            return await _gebruikerRepository.GetPreekCookies(gebruikerId, preekIds); 
        }
    }
}
