using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DomainObject;
using BussinessLayer;
using BussinessLayer.Contracts;
using BussinessLayer.Helper;
using System.Net;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace _Website.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class RaceController : Controller
    {
        private readonly IExceptionService _exceptionService;
        private readonly IRaceService _race;

        public RaceController(IRaceService race, IExceptionService exceptionService)
        {
            _race = race ?? throw new ArgumentNullException(nameof(race));
            _exceptionService = exceptionService ?? throw new ArgumentNullException(nameof(exceptionService));

        }
        [HttpPost("[action]")]
        public async Task<IActionResult> GetRaceResult([FromBody] RaceFilter raceFilter)
        {
            try
            {
                return Ok(this.Content(JsonConvert.SerializeObject(await _race.GetRaceResult(raceFilter)), "application/json"));
            }
            catch (Exception ex)
            {
                //ExceptionLog exception = await _exceptionService.UpsertException(ex, "GetUserRoles", "","");
                return StatusCode((int)HttpStatusCode.InternalServerError, new CustomMesageError(ex.Message).Message);
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> GetRaceEntry([FromBody] RaceFilter raceFilter)
        {
            try
            {
                return Ok(this.Content(JsonConvert.SerializeObject(await _race.GetRaceEntry(raceFilter)), "application/json"));
            }
            catch (Exception ex)
            {
                //ExceptionLog exception = await _exceptionService.UpsertException(ex, "GetUserRoles", "","");
                return StatusCode((int)HttpStatusCode.InternalServerError, new CustomMesageError(ex.Message).Message);
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> GetRaceDetails([FromBody] RaceFilter raceFilter)
        {
            try
            {
                return Ok(this.Content(JsonConvert.SerializeObject(await _race.GetRaceDetails(raceFilter)), "application/json"));
            }
            catch (Exception ex)
            {
                //ExceptionLog exception = await _exceptionService.UpsertException(ex, "GetUserRoles", "","");
                return StatusCode((int)HttpStatusCode.InternalServerError, new CustomMesageError(ex.Message).Message);
            }
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> GetRaceCategory([FromQuery] string dbName, [FromQuery] string clubName)
        {
            try
            {
                return Ok(this.Content(JsonConvert.SerializeObject(await _race.GetBirdCategory(dbName, clubName)), "application/json"));
            }
            catch (Exception ex)
            {
                //ExceptionLog exception = await _exceptionService.UpsertException(ex, "GetUserRoles", "","");
                return StatusCode((int)HttpStatusCode.InternalServerError, new CustomMesageError(ex.Message).Message);
            }
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetRaceGroup([FromQuery] string dbName, [FromQuery] string clubName)
        {
            try
            {
                return Ok(this.Content(JsonConvert.SerializeObject(await _race.GetGroupCategory(dbName,clubName)), "application/json"));
            }
            catch (Exception ex)
            {
                //ExceptionLog exception = await _exceptionService.UpsertException(ex, "GetUserRoles", "","");
                return StatusCode((int)HttpStatusCode.InternalServerError, new CustomMesageError(ex.Message).Message);
            }
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetBalance([FromQuery] string mobilenumber)
        {
            try
            {
                return Ok(this.Content(JsonConvert.SerializeObject(await _race.GetBalance(mobilenumber)), "application/json"));
            }
            catch (Exception ex)
            {
                //ExceptionLog exception = await _exceptionService.UpsertException(ex, "GetUserRoles", "","");
                return StatusCode((int)HttpStatusCode.InternalServerError, new CustomMesageError(ex.Message).Message);
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> OnlineClocking([FromBody] OnlineClocking online)
        {
            try
            {

                return Ok(this.Content(JsonConvert.SerializeObject(await _race.OnlineClocking(online)), "application/json"));
            }
            catch (Exception ex)
            {
                //ExceptionLog exception = await _exceptionService.UpsertException(ex, "GetUserRoles", "","");
                return StatusCode((int)HttpStatusCode.InternalServerError, new CustomMesageError(ex.Message).Message);
            }
        }

    }
}
