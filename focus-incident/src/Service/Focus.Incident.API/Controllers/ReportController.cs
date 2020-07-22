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

    public class ReportController : ControllerBase
    {
        private readonly ReportService reportService;
        private readonly ILogger<ReportController> logger;

        public ReportController(ReportService reportService, ILogger<ReportController> logger)
        {
            this.reportService = reportService;
            this.logger = logger;
        }

        // GET odata/report/Application
        [Route("Application")]
        [EnableQuery]
        public async Task<IActionResult> getApplicationCount()
        {
            try
            {
                var results = await Task.Run(() => { return reportService.getApplicationCount(); });
                return Ok(results);
            }
            catch (Exception ex)
            {

                logger.LogError(ex.ToString());
                throw;
            }
        }

        // GET odata/report/AssignmentGroup
        [Route("AssignmentGroup")]
        [EnableQuery]
        public async Task<IActionResult> getAssignmentGroupCount()
        {
            try
            {
                var results = await Task.Run(() => { return reportService.getAssignmentGroupCount(); });
                return Ok(results);
            }
            catch (Exception ex)
            {

                logger.LogError(ex.ToString());
                throw;
            }
        }


        // GET odata/report/BusinessLines
        [Route("BusinessLines")]
        [EnableQuery]
        public async Task<IActionResult> getBusinessLines()
        {
            try
            {
                var results = await Task.Run(() => { return reportService.getBusinessLineCount(); });
                return Ok(results);
            }
            catch (Exception ex)
            {

                logger.LogError(ex.ToString());
                throw;
            }
        }


        // GET odata/report/Person
        [Route("Person")]
        [EnableQuery]
        public async Task<IActionResult> getPersonCount()
        {
            try
            {
                var results = await Task.Run(() => { return reportService.getPersonCount(); });
                return Ok(results);
            }
            catch (Exception ex)
            {

                logger.LogError(ex.ToString());
                throw;
            }
        }


        // GET odata/report/Summary
        [Route("Summary")]
        [EnableQuery]
        public async Task<IActionResult> getSummaryCounts()
        {
            try
            {
                var results = await Task.Run(() => { return reportService.getSummaryCounts(); });
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