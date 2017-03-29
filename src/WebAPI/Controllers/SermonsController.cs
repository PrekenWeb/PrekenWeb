using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [RoutePrefix("api/Sermons")]
    public class SermonsController : ApiController
    {
        private readonly ISermonsRepository _sermonsRepository;

        public SermonsController(ISermonsRepository sermonsRepository)
        {
            _sermonsRepository = sermonsRepository;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("{id:int}")]
        public async Task<SermonViewModel> Get(int id)
        {
            return await _sermonsRepository.GetSingle(id);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("")]
        public async Task<IEnumerable<SermonViewModel>> Get([FromUri]SermonFilterModel filter)
        {
            return await _sermonsRepository.Get(filter);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("New")]
        public async Task<IEnumerable<SermonViewModel>> New([FromUri]SermonFilterModel filter)
        {
            return await _sermonsRepository.GetNew(filter);
        }
    }
}
