using System;
using System.Threading.Tasks;
using Focus.Incident.Domain.Application.Services;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Focus.Incident.API.Controllers
{ 
    [Route("odata/[controller]")] 
    [Authorize(Policy = "IsLoggedIn")]
    public class ApplicationController : Controller
    {
        private readonly ApplicationService repositoryApplication;
        private readonly ILogger<ReportController> logger;

        public ApplicationController(ApplicationService repositoryApplication, ILogger<ReportController> logger)
        {
            this.repositoryApplication = repositoryApplication;
            this.logger = logger;
        }
        



        // GET odata/Application

        [EnableQuery]
        public async Task<IActionResult> Get()
        {
            try
            {
                var results = await Task.Run(() => { return repositoryApplication.Read(); });
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