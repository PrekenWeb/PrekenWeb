using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [RoutePrefix("api/Speakers")]
    public class SpeakersController : ApiController
    {
        private readonly ISpeakersRepository _speakersRepository;

        public SpeakersController(ISpeakersRepository speakersRepository)
        {
            _speakersRepository = speakersRepository;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("{id:int}")]
        public async Task<SpeakerViewModel> Get(int id)
        {
            return await _speakersRepository.GetSingle(id);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("")]
        public async Task<IEnumerable<SpeakerViewModel>> Get([FromUri]SpeakerFilterModel filter)
        {
            return await _speakersRepository.Get(filter);
        }
    }
}