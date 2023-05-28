using Docker_tests.Service;
using Docker_Tests.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Docker_tests.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PeopleController : ControllerBase
    {
        private readonly IPeopleService _peopleService;
        public PeopleController(IPeopleService peopleService)
        {
            _peopleService = peopleService;
        }

        [HttpGet]
        public async Task<IEnumerable<Person>> Get(string filter)
        {
            return await _peopleService.GetFiltered(filter);
        }
    }
}