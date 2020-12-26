using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace PigeonClockingAPI.Controllers
{
    public class ResultController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [AllowAnonymous]
        [Route("SendSticker")]
        public async Task<IHttpActionResult> SendSticker(string value)
        {

            var result = await Test();

            return Ok();
        }

        public static Task<sample> Test()
        {
            var a = new sample();
            a.Sampletesting = "testing";
            //return Task.Run(() => { return a; });
            return Task.FromResult(a);
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }

    public class sample
    {
        public String Sampletesting { get; set; }
    }
}