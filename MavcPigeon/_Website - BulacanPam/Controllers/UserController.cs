using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using DomainObject;
using BussinessLayer.Contracts;
using BussinessLayer.Helper;
using System.Net;
using Newtonsoft.Json;
using System.Drawing;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;

namespace _Website.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IAccount _account;
        private readonly IExceptionService _exceptionService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UserController(IAccount account, IExceptionService exceptionService, IWebHostEnvironment webHostEnvironment)
        {
            _account = account ?? throw new ArgumentNullException(nameof(account));
            _exceptionService = exceptionService ?? throw new ArgumentNullException(nameof(exceptionService));
            _webHostEnvironment = webHostEnvironment ?? throw new ArgumentNullException(nameof(webHostEnvironment));

        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Authenticate([FromBody] User user)
        {
            try
            {
                return Ok(await _account.Authenticate(user.UserName, user.Password));
            }
            catch (Exception ex)
            {
                //ExceptionLog exception = await _exceptionService.UpsertException(ex, "GetUserRoles", "","");
                return StatusCode((int)HttpStatusCode.InternalServerError, new CustomMesageError(ex.Message).Message);
            }
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetMobileByEmail([FromQuery] string email)
        {
            try
            {
                return Ok(this.Content(JsonConvert.SerializeObject((await _account.GetMobileListByEmail(email))), "application/json"));
                //return Ok(await Task.FromResult("success"));
            }
            catch (Exception ex)
            {
                //ExceptionLog exception = await _exceptionService.UpsertException(ex, "GetUserRoles", "","");
                return StatusCode((int)HttpStatusCode.InternalServerError, new CustomMesageError(ex.Message).Message);
            }
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> SendPassword([FromQuery] string email, [FromQuery] string mobile)
        {
            try
            {
                return Ok(this.Content((await _account.ValidateMobileNumber(email, mobile)), "application/json"));
            }
            catch (Exception ex)
            {
                //ExceptionLog exception = await _exceptionService.UpsertException(ex, "GetUserRoles", "","");
                return StatusCode((int)HttpStatusCode.InternalServerError, new CustomMesageError(ex.Message).Message);
            }
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            try
            {
                return Ok(this.Content(await _account.Insert(user), "application/json"));
            }
            catch (Exception ex)
            {
                //ExceptionLog exception = await _exceptionService.UpsertException(ex, "GetUserRoles", "", "");
                return StatusCode((int)HttpStatusCode.InternalServerError, new CustomMesageError(ex.Message).Message);
            }
        }

        [HttpPost("[action]")]
        [Authorize]
        public async Task<IActionResult> UpdateProfile([FromForm] Profile profile)
        {
            try
            {
                return Ok(this.Content(JsonConvert.SerializeObject((await _account.Update(profile))), "application/json"));
            }
            catch (Exception ex)
            {
                //ExceptionLog exception = await _exceptionService.UpsertException(ex, "GetUserRoles", "", "");
                return StatusCode((int)HttpStatusCode.InternalServerError, new CustomMesageError(ex.Message).Message);
            }
        }

        [HttpPost("[action]")]
        [Authorize]
        public async Task<IActionResult> LinkMobileNumber([FromBody] LinkMobile linkMobile)
        {
            try
            {
                return Ok(this.Content(await _account.LinkMobileNumber(linkMobile), "application/json"));
            }
            catch (Exception ex)
            {
                //ExceptionLog exception = await _exceptionService.UpsertException(ex, "GetUserRoles", "", "");
                return StatusCode((int)HttpStatusCode.InternalServerError, new CustomMesageError(ex.Message).Message);
            }
        }

        [HttpGet("[action]/{id}")]
        [Authorize]
        public async Task<IActionResult> GetMobileLinkList([FromRoute(Name = "id")] string id)
        {
            try
            {
                return Ok(this.Content(JsonConvert.SerializeObject(await _account.GetLinkMobileNumber(id)), "application/json"));
            }
            catch (Exception ex)
            {
                //ExceptionLog exception = await _exceptionService.UpsertException(ex, "GetUserRoles", "", "");
                return StatusCode((int)HttpStatusCode.InternalServerError, new CustomMesageError(ex.Message).Message);
            }

        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetVideo([FromQuery] string type)
        {
            try
            {
                return Ok(this.Content(JsonConvert.SerializeObject(await _account.GetVideo(type)), "application/json"));
            }
            catch (Exception ex)
            {
                //ExceptionLog exception = await _exceptionService.UpsertException(ex, "GetUserRoles", "", "");
                return StatusCode((int)HttpStatusCode.InternalServerError, new CustomMesageError(ex.Message).Message);
            }

        }

        [HttpGet("[action]")]
        [Authorize]
        public async Task<IActionResult> GetMemberCoordinates([FromQuery] string memberidno,[FromQuery] string clubname, [FromQuery] string dbName)
        {
            try
            {
                return Ok(this.Content(JsonConvert.SerializeObject(await _account.GetMemberCoordinates(memberidno, clubname, dbName)), "application/json"));
            }
            catch (Exception ex)
            {
                //ExceptionLog exception = await _exceptionService.UpsertException(ex, "GetUserRoles", "", "");
                return StatusCode((int)HttpStatusCode.InternalServerError, new CustomMesageError(ex.Message).Message);
            }

        }
    }
}


