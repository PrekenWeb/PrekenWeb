using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [RoutePrefix("api/Books")]
    public class BooksController : ApiController
    {
        private readonly IBooksRepository _booksRepository;

        public BooksController(IBooksRepository booksRepository)
        {
            _booksRepository = booksRepository;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("{id:int}")]
        public async Task<BookViewModel> Get(int id)
        {
            return await _booksRepository.GetSingle(id);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("")]
        public async Task<IEnumerable<BookViewModel>> Get([FromUri]BookFilterModel filter)
        {
            return await _booksRepository.Get(filter);
        }
    }
}