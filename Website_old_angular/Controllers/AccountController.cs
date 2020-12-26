using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DomainObject;
using BussinessLayer.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

namespace Website.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly IAccount _accountService;
        private IConfiguration _configuration;
        public AccountController(IAccount accountService, IConfiguration configuration)
        {
            _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        public async Task<string> Register([FromBody] User userdetails)
        {
            return await _accountService.Upsert(userdetails);
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody] User credential)
        {
            IActionResult response = Unauthorized();
            var user = await _accountService.Authenticate(credential.UserName ,credential.Password);

            if (user != null)
            {
                response = Ok(new {token = user.Token, userid = user.UserID});
            }

            return response;
        }
    }
}
