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
    public class AccountController : Controller
    {
        private readonly IAccount _account;
        private readonly IExceptionService _exceptionService;

        public AccountController(IAccount account, IExceptionService exceptionService)
        {
            _account = account ?? throw new ArgumentNullException(nameof(account));
            _exceptionService = exceptionService ?? throw new ArgumentNullException(nameof(exceptionService));

        }
        [HttpPost("[action]")]

        public async Task<IActionResult> Unreg([FromBody] OnlineClocking online)
        {
            try
            {
                return Ok(this.Content(JsonConvert.SerializeObject(await _account.Unreg(online.ClubName, online.MobileNumber, "Unreg", online.DBName, online.UserID)), "application/json"));
            }
            catch (Exception ex)
            {
                //ExceptionLog exception = await _exceptionService.UpsertException(ex, "GetUserRoles", "","");
                return StatusCode((int)HttpStatusCode.InternalServerError, new CustomMesageError(ex.Message).Message);
            }
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> SetAsPrimary([FromBody] OnlineClocking online)
        {
            try
            {
                return Ok(this.Content(JsonConvert.SerializeObject(await _account.SetAsPrimary(online.MobileNumber, online.UserID)), "application/json"));
            }
            catch (Exception ex)
            {
                //ExceptionLog exception = await _exceptionService.UpsertException(ex, "GetUserRoles", "","");
                return StatusCode((int)HttpStatusCode.InternalServerError, new CustomMesageError(ex.Message).Message);
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> PasaLoad([FromBody] OnlineClocking online)
        {
            try
            {
                return Ok(this.Content(JsonConvert.SerializeObject(await _account.Pasaload(online.MobileNumber, online.MobileNumberLoadReceiver, online.Amount)), "application/json"));
            }
            catch (Exception ex)
            {
                //ExceptionLog exception = await _exceptionService.UpsertException(ex, "GetUserRoles", "","");
                return StatusCode((int)HttpStatusCode.InternalServerError, new CustomMesageError(ex.Message).Message);
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> LoadMavcCard([FromBody] OnlineClocking online)
        {
            try
            {
                return Ok(this.Content(JsonConvert.SerializeObject(await _account.LoadMavcCard(online.ClubName, online.MobileNumber, online.Keyword, online.DBName)), "application/json"));
            }
            catch (Exception ex)
            {
                //ExceptionLog exception = await _exceptionService.UpsertException(ex, "GetUserRoles", "","");
                return StatusCode((int)HttpStatusCode.InternalServerError, new CustomMesageError(ex.Message).Message);
            }
        }
    }
}
