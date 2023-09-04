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
    public class GamefowlController : Controller
    {
        
    }
}
