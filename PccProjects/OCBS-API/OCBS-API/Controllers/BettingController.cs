using BusinessLayer.Contracts;
using DomainObject;
using DomainObject.DatabaseObject;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OCBS_API.Helper;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace OCBS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BettingController : ControllerBase
    {
        private readonly IBettings _betting;

        public BettingController(IBettings betting)
        {
            _betting = betting ?? throw new ArgumentNullException(nameof(betting));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> BettingSave([FromBody] Bet bet)
        {
            try
            {
                string token = "";
                if (!String.IsNullOrEmpty(Request.Headers["tokenBearer"])) token = Request.Headers["tokenBearer"];
                return Ok(this.Content(JsonConvert.SerializeObject(await _betting.BettingSave(bet, token)), "application/json"));
            }
            catch (Exception ex)
            {
                //ExceptionLog exception = await _exceptionService.UpsertException(ex, "GetUserRoles", "","");
                return StatusCode((int)HttpStatusCode.InternalServerError, new CustomError(ex.Message).Message);
            }


        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetPlotWinner([FromRoute(Name = "id")] string eventId)
        {
            try
            {
                string token = "";
                if (!String.IsNullOrEmpty(Request.Headers["tokenBearer"])) token = Request.Headers["tokenBearer"];
                var result = await _betting.GetPlotWinner(eventId);

                if (result.Count<100)
                {
                    for (int i = 0; i < 200-result.Count; i++)
                    {
                        result.Add(new PlotWinner() { Row1 = "" });
                    }
                }

                return Ok(this.Content(JsonConvert.SerializeObject(result), "application/json"));
            }
            catch (Exception ex)
            {
                //ExceptionLog exception = await _exceptionService.UpsertException(ex, "GetUserRoles", "","");
                return StatusCode((int)HttpStatusCode.InternalServerError, new CustomError(ex.Message).Message);
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> ClaimPayout([FromBody]Payout payout)
        {
            try
            {
                string token = "";
                if (!String.IsNullOrEmpty(Request.Headers["tokenBearer"])) token = Request.Headers["tokenBearer"];
                return Ok(this.Content(JsonConvert.SerializeObject(await _betting.ClaimPayout(payout)), "application/json"));
            }
            catch (Exception ex)
            {
                //ExceptionLog exception = await _exceptionService.UpsertException(ex, "GetUserRoles", "","");
                return StatusCode((int)HttpStatusCode.InternalServerError, new CustomError(ex.Message).Message);
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CancelBetting([FromBody] Payout payout)
        {
            try
            {
                string token = "";
                if (!String.IsNullOrEmpty(Request.Headers["tokenBearer"])) token = Request.Headers["tokenBearer"];
                return Ok(this.Content(JsonConvert.SerializeObject(await _betting.CancelBetting(payout)), "application/json"));
            }
            catch (Exception ex)
            {
                //ExceptionLog exception = await _exceptionService.UpsertException(ex, "GetUserRoles", "","");
                return StatusCode((int)HttpStatusCode.InternalServerError, new CustomError(ex.Message).Message);
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> TellerPointSave([FromBody]Points point)
        {
            try
            {
                string token = "";
                if (!String.IsNullOrEmpty(Request.Headers["tokenBearer"])) token = Request.Headers["tokenBearer"];
                return Ok(this.Content(JsonConvert.SerializeObject(await _betting.TellerPointSave(point)), "application/json"));
            }
            catch (Exception ex)
            {
                //ExceptionLog exception = await _exceptionService.UpsertException(ex, "GetUserRoles", "","");
                return StatusCode((int)HttpStatusCode.InternalServerError, new CustomError(ex.Message).Message);
            }
        }

        [HttpGet("[action]/{platformuserid}/{userid}/{IsOffline}/{id}")]
        public async Task<IActionResult> GetCurrentPoints([FromRoute(Name = "id")] string eventId,[FromRoute(Name = "platformuserid")] string platformuserid, [FromRoute(Name = "userid")] Int64 userid, [FromRoute(Name = "IsOffline")] bool IsOffline)
        {
            try
            {
                string token = "";
                if (!String.IsNullOrEmpty(Request.Headers["tokenBearer"])) token = Request.Headers["tokenBearer"];
                return Ok(this.Content(JsonConvert.SerializeObject(await _betting.GetCurrentPoints(token, platformuserid, userid, IsOffline,Int64.Parse(eventId))), "application/json"));
            }
            catch (Exception ex)
            {
                //ExceptionLog exception = await _exceptionService.UpsertException(ex, "GetUserRoles", "","");
                return StatusCode((int)HttpStatusCode.InternalServerError, new CustomError(ex.Message).Message);
            }
        }

        [HttpGet("[action]/{id}/{userid}/{platformuserid}/{refid}/{IsOffline}")]
        public async Task<IActionResult> GetClaims([FromRoute(Name = "id")] string eventId, [FromRoute(Name = "userid")] Int64 userid, [FromRoute(Name = "platformuserid")] string platformuserid, [FromRoute(Name = "refid")] string refid, [FromRoute(Name = "IsOffline")] bool IsOffline)
        {
            try
            {
                string token = "";
                if (!String.IsNullOrEmpty(Request.Headers["tokenBearer"])) token = Request.Headers["tokenBearer"];
                return Ok(this.Content(JsonConvert.SerializeObject(await _betting.GetBettingByRefId(eventId,refid, token,userid,platformuserid,IsOffline)), "application/json"));
            }
            catch (Exception ex)
            {
                //ExceptionLog exception = await _exceptionService.UpsertException(ex, "GetUserRoles", "","");
                return StatusCode((int)HttpStatusCode.InternalServerError, new CustomError(ex.Message).Message);
            }


        }

        [HttpGet("[action]/{id}/{userid}/{platformuserid}/{refid}/{IsOffline}")]
        public async Task<IActionResult> GetCancelBet([FromRoute(Name = "id")] string eventId, [FromRoute(Name = "userid")] Int64 userid, [FromRoute(Name = "platformuserid")] string platformuserid, [FromRoute(Name = "refid")] string refid, [FromRoute(Name = "IsOffline")] bool IsOffline)
        {
            try
            {
                string token = "";
                if (!String.IsNullOrEmpty(Request.Headers["tokenBearer"])) token = Request.Headers["tokenBearer"];
                return Ok(this.Content(JsonConvert.SerializeObject(await _betting.GetCancelBettingByRefId(eventId, refid, token, userid, platformuserid, IsOffline)), "application/json"));
            }
            catch (Exception ex)
            {
                //ExceptionLog exception = await _exceptionService.UpsertException(ex, "GetUserRoles", "","");
                return StatusCode((int)HttpStatusCode.InternalServerError, new CustomError(ex.Message).Message);
            }


        }

        [HttpGet("[action]/{id}/{userid}/{fightno}")]
        public async Task<IActionResult> GetBettingByFightNo([FromRoute(Name = "id")] string eventId, [FromRoute(Name = "userid")] Int64 userid, [FromRoute(Name = "fightno")] string fightno)
        {
            try
            {
                string token = "";
                if (!String.IsNullOrEmpty(Request.Headers["tokenBearer"])) token = Request.Headers["tokenBearer"];
                return Ok(this.Content(JsonConvert.SerializeObject(await _betting.GetBettingByFightNo(fightno,eventId,userid)), "application/json"));
            }
            catch (Exception ex)
            {
                //ExceptionLog exception = await _exceptionService.UpsertException(ex, "GetUserRoles", "","");
                return StatusCode((int)HttpStatusCode.InternalServerError, new CustomError(ex.Message).Message);
            }


        }
        [HttpGet("[action]/{id}/{userid}/{fightno}")]
        public async Task<IActionResult> GetHighBettingByFightNo([FromRoute(Name = "id")] string eventId, [FromRoute(Name = "userid")] Int64 userid, [FromRoute(Name = "fightno")] string fightno)
        {
            try
            {
                string token = "";
                if (!String.IsNullOrEmpty(Request.Headers["tokenBearer"])) token = Request.Headers["tokenBearer"];
                return Ok(this.Content(JsonConvert.SerializeObject(await _betting.GetHighBettingByFightNo(fightno, eventId, userid)), "application/json"));
            }
            catch (Exception ex)
            {
                //ExceptionLog exception = await _exceptionService.UpsertException(ex, "GetUserRoles", "","");
                return StatusCode((int)HttpStatusCode.InternalServerError, new CustomError(ex.Message).Message);
            }


        }

        [HttpGet("[action]/{id}/{userid}")]
        public async Task<IActionResult> GetUnclaimedTicket([FromRoute(Name = "id")] string eventId, [FromRoute(Name = "userid")] Int64 userid)
        {
            try
            {
                    string token = "";
                if (!String.IsNullOrEmpty(Request.Headers["tokenBearer"])) token = Request.Headers["tokenBearer"];
                return Ok(this.Content(JsonConvert.SerializeObject(await _betting.GetUnClaimedTicket(eventId, userid)), "application/json"));
            }
            catch (Exception ex)
            {
                //ExceptionLog exception = await _exceptionService.UpsertException(ex, "GetUserRoles", "","");
                return StatusCode((int)HttpStatusCode.InternalServerError, new CustomError(ex.Message).Message);
            }
        }

        [HttpGet("[action]/{id}/{userid}")]
        public async Task<IActionResult> GetClaimedTicket([FromRoute(Name = "id")] string eventId, [FromRoute(Name = "userid")] Int64 userid)
        {
            try
            {
                string token = "";
                if (!String.IsNullOrEmpty(Request.Headers["tokenBearer"])) token = Request.Headers["tokenBearer"];
                return Ok(this.Content(JsonConvert.SerializeObject(await _betting.GetUnClaimedTicket(eventId, userid)), "application/json"));
            }
            catch (Exception ex)
            {
                //ExceptionLog exception = await _exceptionService.UpsertException(ex, "GetUserRoles", "","");
                return StatusCode((int)HttpStatusCode.InternalServerError, new CustomError(ex.Message).Message);
            }


        }

        [HttpGet("[action]/{id}/{userid}")]
        public async Task<IActionResult> GetBettingHistoryByEvent([FromRoute(Name = "id")] string eventId, [FromRoute(Name = "userid")] Int64 userid)
        {
            try
            {
                string token = "";
                if (!String.IsNullOrEmpty(Request.Headers["tokenBearer"])) token = Request.Headers["tokenBearer"];
                return Ok(this.Content(JsonConvert.SerializeObject(await _betting.GetBettingHistoryByEvent(eventId, userid)), "application/json"));
            }
            catch (Exception ex)
            {
                //ExceptionLog exception = await _exceptionService.UpsertException(ex, "GetUserRoles", "","");
                return StatusCode((int)HttpStatusCode.InternalServerError, new CustomError(ex.Message).Message);
            }


        }
        [HttpGet("[action]/{id}/{userid}")]
        public async Task<IActionResult> GetLastClaims([FromRoute(Name = "id")] string eventId, [FromRoute(Name = "userid")] Int64 userid)
        {
            try
            {
                string token = "";
                if (!String.IsNullOrEmpty(Request.Headers["tokenBearer"])) token = Request.Headers["tokenBearer"];
                return Ok(this.Content(JsonConvert.SerializeObject(await _betting.GetLastClaims(eventId, userid)), "application/json"));
            }
            catch (Exception ex)
            {
                //ExceptionLog exception = await _exceptionService.UpsertException(ex, "GetUserRoles", "","");
                return StatusCode((int)HttpStatusCode.InternalServerError, new CustomError(ex.Message).Message);
            }


        }

        [HttpGet("[action]/{id}/{userid}")]
        public async Task<IActionResult> GetPointsHistory([FromRoute(Name = "id")] string eventId, [FromRoute(Name = "userid")] Int64 userid)
        {
            try
            {
                string token = "";
                if (!String.IsNullOrEmpty(Request.Headers["tokenBearer"])) token = Request.Headers["tokenBearer"];
                return Ok(this.Content(JsonConvert.SerializeObject(await _betting.GetPointHistory(eventId, userid)), "application/json"));
            }
            catch (Exception ex)
            {
                //ExceptionLog exception = await _exceptionService.UpsertException(ex, "GetUserRoles", "","");
                return StatusCode((int)HttpStatusCode.InternalServerError, new CustomError(ex.Message).Message);
            }


        }
    }
}
