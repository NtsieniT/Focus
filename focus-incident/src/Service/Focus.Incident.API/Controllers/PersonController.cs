using System;
using System.Threading.Tasks;
using Focus.Incident.Domain.Person.Services;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Focus.Incident.API.Controllers
{
    [Route("odata/[controller]")]
    [Authorize(Policy = "IsLoggedIn")]
    public class PersonController : Controller
    {
        private readonly PersonService PersonService;
        private readonly ILogger<ReportController> logger;

        public PersonController(PersonService personService, ILogger<ReportController> logger)
        {
            PersonService = personService;
            this.logger = logger;
        }
        // GET odata/Person

        [EnableQuery]
        public async Task<IActionResult> Get()
        {
            try
            {
                var results = await Task.Run(() => { return PersonService.Read(); });
                return Ok(results);
            }
            catch (Exception ex)
            {

                logger.LogError(ex.ToString());
                throw;
            }
        }
    }
}