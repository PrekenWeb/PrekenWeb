namespace PrekenWeb.WebApi.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using PrekenWeb.WebApi.Models;

    using System.Collections.Generic;
    using System.Linq;

    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class PrekenController : ControllerBase
    {
        private readonly PrekenWebContext dbContext;

        public PrekenController(PrekenWebContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IEnumerable<Preek> Get()
        {
            return this.dbContext.Preek.Take(10);
        }
    }
}
