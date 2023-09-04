using BusinessLayer.Contracts;
using DomainObject;
using DomainObject.DatabaseObject;
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
    public class UsersController : ControllerBase
    {
        private readonly IUsers _users;

        public UsersController(IUsers users)
        {
            _users = users ?? throw new ArgumentNullException(nameof(users));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromBody]UserLogin user)
        {
            try
            {
                return Ok(this.Content(JsonConvert.SerializeObject(await _users.Authenticate(user)), "application/json"));
            }
            catch (Exception ex)
            {
                //ExceptionLog exception = await _exceptionService.UpsertException(ex, "GetUserRoles", "","");
                return StatusCode((int)HttpStatusCode.InternalServerError, new CustomError(ex.Message).Message);
            }


        }

        [HttpPost("[action]")]
        public async Task<IActionResult> UserSave([FromBody] User user)
        {
            try
            {
                return Ok(this.Content(JsonConvert.SerializeObject(await _users.UserSave(user)), "application/json"));
            }
            catch (Exception ex)
            {
                //ExceptionLog exception = await _exceptionService.UpsertException(ex, "GetUserRoles", "","");
                return StatusCode((int)HttpStatusCode.InternalServerError, new CustomError(ex.Message).Message);
            }
        }

        [HttpGet("[action]/{id}/{companyid}/{userid}")]
        public async Task<IActionResult> GetUserById([FromRoute(Name = "id")] Int64 id, [FromRoute(Name = "companyid")] Int64 companyid, [FromRoute(Name = "userid")] Int64 userid)
        {
            try
            {
                string token = "";
                if (!String.IsNullOrEmpty(Request.Headers["tokenBearer"])) token = Request.Headers["tokenBearer"];
                return Ok(this.Content(JsonConvert.SerializeObject(await _users.GetUserById(userid, id, companyid)), "application/json"));
            }
            catch (Exception ex)
            {
                //ExceptionLog exception = await _exceptionService.UpsertException(ex, "GetUserRoles", "","");
                return StatusCode((int)HttpStatusCode.InternalServerError, new CustomError(ex.Message).Message);
            }
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetCompany()
        {
            try
            {
                string token = "";
                if (!String.IsNullOrEmpty(Request.Headers["tokenBearer"])) token = Request.Headers["tokenBearer"];
                return Ok(this.Content(JsonConvert.SerializeObject(await _users.GetCompany()), "application/json"));
            }
            catch (Exception ex)
            {
                //ExceptionLog exception = await _exceptionService.UpsertException(ex, "GetUserRoles", "","");
                return StatusCode((int)HttpStatusCode.InternalServerError, new CustomError(ex.Message).Message);
            }
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetRole()
        {
            try
            {
                string token = "";
                if (!String.IsNullOrEmpty(Request.Headers["tokenBearer"])) token = Request.Headers["tokenBearer"];
                return Ok(this.Content(JsonConvert.SerializeObject(await _users.GetRole()), "application/json"));
            }
            catch (Exception ex)
            {
                //ExceptionLog exception = await _exceptionService.UpsertException(ex, "GetUserRoles", "","");
                return StatusCode((int)HttpStatusCode.InternalServerError, new CustomError(ex.Message).Message);
            }
        }

        [HttpGet("[action]/{id}/{eventid}/{userid}")]
        public async Task<IActionResult> GetTellerList([FromRoute(Name = "id")] Int64 companyId, [FromRoute(Name = "eventid")] Int64 eventid, [FromRoute(Name = "userid")] Int64 userid)
        {
            try
            {
                string token = "";
                if (!String.IsNullOrEmpty(Request.Headers["tokenBearer"])) token = Request.Headers["tokenBearer"];
                return Ok(this.Content(JsonConvert.SerializeObject(await _users.GetTellerList(companyId, eventid, userid)), "application/json"));
            }
            catch (Exception ex)
            {
                //ExceptionLog exception = await _exceptionService.UpsertException(ex, "GetUserRoles", "","");
                return StatusCode((int)HttpStatusCode.InternalServerError, new CustomError(ex.Message).Message);
            }
        }
    }
}
