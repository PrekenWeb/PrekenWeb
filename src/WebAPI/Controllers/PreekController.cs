using System.Threading.Tasks;
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
         
        [Authorize] 
        public async Task<string> GetAllPreken()
        {
            return User.Identity.Name;
           // return await Task.FromResult(new List<Preek> { new Preek { BijbeltekstOmschrijving = "asd" } });
            //return await _preekRepository.GetAllePreken(1);
        }
    }
}
