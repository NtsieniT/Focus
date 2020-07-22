using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Focus.Incident.Domain.Report.Services;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Focus.Incident.API.Controllers
{
    [Route("odata/[controller]")]
    [Authorize(Policy = "IsLoggedIn")]
    public class SummaryController : Controller
    {

        private readonly SummaryService summaryService;
        private readonly ILogger<PrimaryBusinessLinesController> logger;

        public SummaryController(SummaryService summaryService, ILogger<PrimaryBusinessLinesController> logger)
        {
            this.summaryService = summaryService;
            this.logger = logger;
        }


        [EnableQuery]
        public async Task<IActionResult> Get()
        {
            try
            {
                var results = await Task.Run(() => { return summaryService.Read(); });
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