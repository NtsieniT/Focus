using System;
using System.Threading.Tasks;
using Focus.Incident.Domain.AssignmentGroup.Services;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Focus.Incident.API.Controllers
{
    [Route("odata/[controller]")]
    [Authorize(Policy = "IsLoggedIn")]
    public class AssignmentGroupController : Controller
    {
        private readonly AssignmentGroupService assignmentGroupService;
        private readonly ILogger<ReportController> logger;

        public AssignmentGroupController(AssignmentGroupService assignmentGroupService, ILogger<ReportController> logger)
        {
            this.assignmentGroupService = assignmentGroupService;
            this.logger = logger;
        }




        // GET odata/report/AssignmentGroup

        [EnableQuery]
        public async Task<IActionResult> Get()
        {
            try
            {
                var results = await Task.Run(() => { return assignmentGroupService.Read(); });
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