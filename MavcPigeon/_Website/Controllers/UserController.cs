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

namespace _Website.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IAccount _account;
        private readonly IExceptionService _exceptionService;

        public UserController(IAccount account, IExceptionService exceptionService)
        {
            _account = account ?? throw new ArgumentNullException(nameof(account));
            _exceptionService = exceptionService ?? throw new ArgumentNullException(nameof(exceptionService));

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

        [HttpPost("[action]")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            try
            {
                return Ok(await _account.Insert(user));
            }
            catch (Exception ex)
            {
                //ExceptionLog exception = await _exceptionService.UpsertException(ex, "GetUserRoles", "", "");
                return StatusCode((int)HttpStatusCode.InternalServerError, new CustomMesageError(ex.Message).Message);
            }
        }
    }
}
