using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using PrekenWeb.Data.Services.Interfaces;
using PrekenWeb.Data.ViewModels;

namespace WebAPI.Controllers
{
    [RoutePrefix("api/Sermons")]
    public class SermonsController : ApiController
    {
        private readonly ISermonsService _sermonsService;
        private readonly ILanguagesService _languagesService;

        //public SermonsController()
        //{
        //}

        public SermonsController(ISermonsService homeService, ILanguagesService languagesService)
        {
            _sermonsService = homeService;
            _languagesService = languagesService;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("{id:int}")]
        public async Task<SermonModel> Get(int id)
        {
            return await _sermonsService.GetSingle(id);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("")]
        public async Task<IEnumerable<SermonModel>> Get([FromUri]SermonFilter filter)
        {
            filter = EnsureLanguage(filter);
            return await _sermonsService.Get(filter);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("New")]
        public async Task<IEnumerable<SermonModel>> New([FromUri]SermonFilter filter)
        {
            filter = EnsureLanguage(filter);
            return await _sermonsService.GetNew(filter);
        }

        // private methods
        #region EnsureLanguage

        private SermonFilter EnsureLanguage(SermonFilter filter)
        {
            if (filter?.LanguageId != null)
                return filter;

            if (filter == null)
                filter = new SermonFilter();

            filter.LanguageId = _languagesService.GetDefault().Id;
            return filter;
        }

        #endregion
    }
}
