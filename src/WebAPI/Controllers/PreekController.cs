using System.Web.Http;
using Prekenweb.Models.Repository;

namespace WebAPI.Controllers
{
    public class PreekController : ApiController
    {
        private readonly IPreekRepository _preekRepository;

        public PreekController()
        { 
        }

        public PreekController(IPreekRepository preekRepository)
        {
            _preekRepository = preekRepository;
        }
          
    }
}
