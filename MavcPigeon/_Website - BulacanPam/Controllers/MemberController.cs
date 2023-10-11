using BussinessLayer.Contracts;
using BussinessLayer.Helper;
using DomainObject;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace _Website.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MemberController : Controller
    {
        private readonly IMemberService _member;
        private readonly IExceptionService _exceptionService;

        public MemberController(IMemberService member, IExceptionService exceptionService)
        {
            _member = member ?? throw new ArgumentNullException(nameof(member));
            _exceptionService = exceptionService ?? throw new ArgumentNullException(nameof(exceptionService));

        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetMemberDistance([FromQuery] string clubname, string memberidno, string dbname)
        {
            try
            {
                return Ok(this.Content(JsonConvert.SerializeObject(await _member.GetMemberDistance(clubname,memberidno,dbname)), "application/json"));
            }
            catch (Exception ex)
            {
                //ExceptionLog exception = await _exceptionService.UpsertException(ex, "GetUserRoles", "","");
                return StatusCode((int)HttpStatusCode.InternalServerError, new CustomMesageError(ex.Message).Message);
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> GetViewLogs([FromBody] MemberLogs memberLogs)
        {

            try
            {
                return Ok(this.Content(JsonConvert.SerializeObject(await _member.GetLogs(memberLogs.ClubID, memberLogs.MobileNumber, memberLogs.Keyword, memberLogs.DateFrom, memberLogs.DateTo, memberLogs.DBName)), "application/json")); ;
            }
            catch (Exception ex)
            {
                //ExceptionLog exception = await _exceptionService.UpsertException(ex, "GetUserRoles", "","");
                return StatusCode((int)HttpStatusCode.InternalServerError, new CustomMesageError(ex.Message).Message);
            }
        }
    }
}
