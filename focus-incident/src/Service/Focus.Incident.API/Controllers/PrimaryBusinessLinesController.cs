using System;
using System.Threading.Tasks;
using Focus.Incident.Domain.PrimaryBusinessLine.Services;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Focus.Incident.API.Controllers
{
    [Route("odata/[controller]")]
    [Authorize(Policy = "IsLoggedIn")]
    public class PrimaryBusinessLinesController : Controller
    {
        private readonly PrimaryBusinessLineService primaryBusinessLineService;
        private readonly ILogger<PrimaryBusinessLinesController> logger;

        public PrimaryBusinessLinesController(PrimaryBusinessLineService primaryBusinessLineService, ILogger<PrimaryBusinessLinesController> logger)
        {
            this.primaryBusinessLineService = primaryBusinessLineService ?? throw new ArgumentNullException(nameof(primaryBusinessLineService));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        
        [EnableQuery]
        public async Task<IActionResult> Get()
        {
            try
            {
                var results = await Task.Run(() => { return primaryBusinessLineService.Read(); });
                return Ok(results);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw;
            }
        }

        //public IQueryable<PrimaryBusinessLine> Get()
        //{
        //    return primaryBusinessLineService.Read();
        //}
    }
}