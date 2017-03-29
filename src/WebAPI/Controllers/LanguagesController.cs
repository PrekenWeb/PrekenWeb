using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [RoutePrefix("api/Languages")]
    public class LanguagesController : ApiController
    {
        private readonly ILanguagesRepository _languagesRepository;

        public LanguagesController(ILanguagesRepository languagesRepository)
        {
            _languagesRepository = languagesRepository;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("{id:int}")]
        public async Task<LanguageViewModel> Get(int id)
        {
            return await _languagesRepository.GetSingle(id);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("")]
        public async Task<IEnumerable<LanguageViewModel>> Get([FromUri]LanguageFilterModel filter)
        {
            return await _languagesRepository.Get(filter);
        }
    }
}