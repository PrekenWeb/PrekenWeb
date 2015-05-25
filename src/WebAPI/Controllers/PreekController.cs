using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Prekenweb.Models.Dtos;
using Prekenweb.Models.Services;

namespace WebAPI.Controllers
{
    public class PreekController : ApiController
    {
        private readonly IHomeService _homeService;

        public PreekController()
        {
        }

        public PreekController(IHomeService homeService)
        {
            _homeService = homeService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<Preek>> Nieuw()
        { 
            return await _homeService.NieuwePreken(1);
        }
    }
}
