using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Focus.Incident.Domain.Incident.Service;
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
    //[Produces("application/json")]
    public class IncidentController : ControllerBase
    {

        private readonly IncidentService incidentService;
        private readonly ReportService reportService;
        private readonly ILogger<IncidentController> logger;

        public IncidentController(IncidentService incidentService, ReportService reportService, ILogger<IncidentController> logger)
        {
            this.incidentService = incidentService;
            this.reportService = reportService;
            this.logger = logger;
        }

        
        [EnableQuery]
        public async Task<IActionResult> getIncidents()
        {
            try
            {
                var results = await Task.Run(() => { return incidentService.Read(); });
                return Ok(results);
            }
            catch (Exception ex)
            {

                logger.LogError(ex.ToString());
                throw;
            }
        }



        // GET: api/Incident
        //[HttpGet]
        //public List<Domain.Incident.Models.Incident> Get()
        //{
        //    return incidentService.Read();
        //}


        //[Route("PrimaryBusinessLineCounts")]



        //[EnableQuery]
        //public async Task<IActionResult> Get()
        //{
        //    try
        //    {
        //        var results = await Task.Run(() => { return incidentService.Read(); });
        //        return Ok(results);
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.LogError(ex.ToString());
        //        throw;
        //    }

        //}



        //// GET: api/Incident/5
        //[HttpGet("{id}", Name = "Get")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST: api/Incident
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT: api/Incident/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
