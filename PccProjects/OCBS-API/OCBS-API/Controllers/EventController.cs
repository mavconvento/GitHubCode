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
    public class EventController : ControllerBase
    {
        private readonly IEvents _events;
       
        public EventController(IEvents events)
        {
            _events = events ?? throw new ArgumentNullException(nameof(events));

        }

        [HttpGet("[action]/{userid}/{platformuserid}/{IsOffline}")]
        public async Task<IActionResult> GetEvent([FromRoute(Name = "userid")] Int64 userid, [FromRoute(Name = "platformuserid")] string platformuserid, [FromRoute(Name = "IsOffline")] bool IsOffline)
        {
            try
            {
                string token = "";
                if (!String.IsNullOrEmpty(Request.Headers["tokenBearer"])) token = Request.Headers["tokenBearer"];
                return Ok(this.Content(JsonConvert.SerializeObject(await _events.GetCurrentEvent(token,userid,platformuserid,IsOffline,"",0)), "application/json"));
            }
            catch (Exception ex)
            {
                //ExceptionLog exception = await _exceptionService.UpsertException(ex, "GetUserRoles", "","");
                return StatusCode((int)HttpStatusCode.InternalServerError, new CustomError(ex.Message).Message);
            }


        }

        [HttpGet("[action]/{companyid}/{eventid}")]
        public async Task<IActionResult> GetEventById([FromRoute(Name = "companyid")] Int64 companyid, [FromRoute(Name = "eventid")] Int64 eventid)
        {
            try
            {
                string token = "";
                if (!String.IsNullOrEmpty(Request.Headers["tokenBearer"])) token = Request.Headers["tokenBearer"];
                return Ok(this.Content(JsonConvert.SerializeObject(await _events.GetEventById(companyid, eventid)), "application/json"));
            }
            catch (Exception ex)
            {
                //ExceptionLog exception = await _exceptionService.UpsertException(ex, "GetUserRoles", "","");
                return StatusCode((int)HttpStatusCode.InternalServerError, new CustomError(ex.Message).Message);
            }


        }

        [HttpPost("[action]")]
        public async Task<IActionResult> EventSave([FromRoute(Name = "userid")] Int64 userid, [FromRoute(Name = "platformuserid")] string platformuserid, [FromRoute(Name = "IsOffline")] bool IsOffline)
        {
            try
            {
                string token = "";
                if (!String.IsNullOrEmpty(Request.Headers["tokenBearer"])) token = Request.Headers["tokenBearer"];
                return Ok(this.Content(JsonConvert.SerializeObject(await _events.GetCurrentEvent(token, userid, platformuserid, IsOffline, "",0)), "application/json"));
            }
            catch (Exception ex)
            {
                //ExceptionLog exception = await _exceptionService.UpsertException(ex, "GetUserRoles", "","");
                return StatusCode((int)HttpStatusCode.InternalServerError, new CustomError(ex.Message).Message);
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> EventOfflineSave([FromBody] EventOffline events)
        {
            try
            {
                string token = "";
                if (!String.IsNullOrEmpty(Request.Headers["tokenBearer"])) token = Request.Headers["tokenBearer"];
                return Ok(this.Content(JsonConvert.SerializeObject(await _events.GetCurrentEvent(token, events.userId, events.platformUserId, events.IsOffline, events.event_name,events.eventId)), "application/json"));
            }
            catch (Exception ex)
            {
                //ExceptionLog exception = await _exceptionService.UpsertException(ex, "GetUserRoles", "","");
                return StatusCode((int)HttpStatusCode.InternalServerError, new CustomError(ex.Message).Message);
            }


        }

        [HttpPost("[action]")]
        public async Task<IActionResult> OfflineFightSave([FromBody]  FightOffline fightOffline)
        {
            try
            {
                string token = "";
                if (!String.IsNullOrEmpty(Request.Headers["tokenBearer"])) token = Request.Headers["tokenBearer"];
                return Ok(this.Content(JsonConvert.SerializeObject(await _events.FightOfflineSave(fightOffline)), "application/json"));
            }
            catch (Exception ex)
            {
                //ExceptionLog exception = await _exceptionService.UpsertException(ex, "GetUserRoles", "","");
                return StatusCode((int)HttpStatusCode.InternalServerError, new CustomError(ex.Message).Message);
            }
        }

        [HttpGet("[action]/{id}/{userid}/{fightno}")]
        public async Task<IActionResult> GetBettingReportByFightNo([FromRoute(Name = "id")] Int64 eventId, [FromRoute(Name = "userid")] Int64 userid, [FromRoute(Name = "fightno")] Int64 fightno)
        {
            try
            {
                string token = "";
                if (!String.IsNullOrEmpty(Request.Headers["tokenBearer"])) token = Request.Headers["tokenBearer"];
                return Ok(this.Content(JsonConvert.SerializeObject(await _events.BettingReportByFightNo(eventId, fightno)), "application/json"));
            }
            catch (Exception ex)
            {
                //ExceptionLog exception = await _exceptionService.UpsertException(ex, "GetUserRoles", "","");
                return StatusCode((int)HttpStatusCode.InternalServerError, new CustomError(ex.Message).Message);
            }

        }

        [HttpGet("[action]/{id}/{userid}/{platformuserid}/{IsOffline}")]
        public async Task<IActionResult> GetCurrentFight([FromRoute(Name = "id")] string eventId, [FromRoute(Name = "userid")] Int64 userid, [FromRoute(Name = "platformuserid")] string platformuserid, [FromRoute(Name = "IsOffline")] bool IsOffline)
        {
            try
            {
                string token = "";
                if (!String.IsNullOrEmpty(Request.Headers["tokenBearer"])) token = Request.Headers["tokenBearer"];
                return Ok(this.Content(JsonConvert.SerializeObject(await _events.GetFight(eventId, token,userid,platformuserid,IsOffline)), "application/json"));
            }
            catch (Exception ex)
            {
                //ExceptionLog exception = await _exceptionService.UpsertException(ex, "GetUserRoles", "","");
                return StatusCode((int)HttpStatusCode.InternalServerError, new CustomError(ex.Message).Message);
            }

        }

        [HttpGet("[action]/{id}/{userid}/{platformuserid}/{IsOffline}")]
        public async Task<IActionResult> GetCurrentFightOdds([FromRoute(Name = "id")] string eventId, [FromRoute(Name = "userid")] Int64 userid, [FromRoute(Name = "platformuserid")] string platformuserid, [FromRoute(Name = "IsOffline")] bool IsOffline)
        {
            try
            {
                string token = "";
                if (!String.IsNullOrEmpty(Request.Headers["tokenBearer"])) token = Request.Headers["tokenBearer"];
                return Ok(this.Content(JsonConvert.SerializeObject(await _events.GetFightOdds(eventId, token, userid,platformuserid,IsOffline)), "application/json"));
            }
            catch (Exception ex)
            {
                //ExceptionLog exception = await _exceptionService.UpsertException(ex, "GetUserRoles", "","");
                return StatusCode((int)HttpStatusCode.InternalServerError, new CustomError(ex.Message).Message);
            }


        }
        [HttpGet("[action]/{id}/{userid}/{fightno?}")]
        public async Task<IActionResult> GetCurrentFightOffline([FromRoute(Name = "id")] Int64 eventId, [FromRoute(Name = "userid")] Int64 userid, [FromRoute(Name = "fightno")] string fightno)
        {
            try
            {
                string token = "";
                if (!String.IsNullOrEmpty(Request.Headers["tokenBearer"])) token = Request.Headers["tokenBearer"];
                return Ok(this.Content(JsonConvert.SerializeObject(await _events.GetFightOffline(eventId, userid, fightno)), "application/json"));
            }
            catch (Exception ex)
            {
                //ExceptionLog exception = await _exceptionService.UpsertException(ex, "GetUserRoles", "","");
                return StatusCode((int)HttpStatusCode.InternalServerError, new CustomError(ex.Message).Message);
            }


        }
    }
}
