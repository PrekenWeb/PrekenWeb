using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Prekenweb.Models;
using Prekenweb.Models.Repository;

namespace WebAPI.Controllers
{
    public class PreekController : ApiController
    {
        private readonly IPreekRepository _preekRepository;

        public PreekController (IPreekRepository preekRepository)
        {
            _preekRepository = preekRepository;
        }

        //[AllowAnonymous]
        [Authorize]
        public async Task<IEnumerable<Preek>> GetAllPreken()
        {
            return await _preekRepository.GetAllePreken(1);
        } 
    }
}
