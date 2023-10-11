using BusinessLayer.Contracts;
using DomainObject;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OCBS_API.Helper;
using System;
using System.Net;
using System.Threading.Tasks;

namespace OCBS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReports _reports;

        public ReportController(IReports reports)
        {
            _reports = reports ?? throw new ArgumentNullException(nameof(reports));

        }

        [HttpGet("[action]/{id}/{userid}/{fightno}")]
        public async Task<IActionResult> GetBettingReportByFightNo([FromRoute(Name = "id")] Int64 eventId, [FromRoute(Name = "userid")] Int64 userid, [FromRoute(Name = "fightno")] Int64 fightno)
        {
            try
            {
                string token = "";
                if (!String.IsNullOrEmpty(Request.Headers["tokenBearer"])) token = Request.Headers["tokenBearer"];
                return Ok(this.Content(JsonConvert.SerializeObject(await _reports.BettingReportByFightNo(eventId, fightno)), "application/json"));
            }
            catch (Exception ex)
            {
                //ExceptionLog exception = await _exceptionService.UpsertException(ex, "GetUserRoles", "","");
                return StatusCode((int)HttpStatusCode.InternalServerError, new CustomError(ex.Message).Message);
            }

        }

        [HttpGet("[action]/{id}/{userid}")]
        public async Task<IActionResult> GetBettingReportSummary([FromRoute(Name = "id")] Int64 eventId, [FromRoute(Name = "userid")] Int64 userid)
        {
            try
            {
                string token = "";
                if (!String.IsNullOrEmpty(Request.Headers["tokenBearer"])) token = Request.Headers["tokenBearer"];
                return Ok(this.Content(JsonConvert.SerializeObject(await _reports.BettingReportSummary(eventId,userid)), "application/json"));
            }
            catch (Exception ex)
            {
                //ExceptionLog exception = await _exceptionService.UpsertException(ex, "GetUserRoles", "","");
                return StatusCode((int)HttpStatusCode.InternalServerError, new CustomError(ex.Message).Message);
            }

        }

    }
}
