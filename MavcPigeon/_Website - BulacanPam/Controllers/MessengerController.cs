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
using System.IO;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace _Website.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MessengerController : Controller
    {
        private string fbToken = "EAAPKb0JdTc0BADdamyKPcqAyvuur2oI3QDQkZCsTBirl1hZAvmXTlFQvKRYnvsNhlRP5cOV6HBmhJ8V3tWqtVyebIesRKCWzv1eJoCpZAKn3p5nD26ALjqNSPVWQ1quSLeHvAq9ZBlJnUMotjxfII84ty8TZAHZA9uWynIeA7V4zqznjxdLbd9WMYBXLIwyZA0ZD";
        private string postUrl = "https://graph.facebook.com/v2.6/me/messages";

        private readonly IExceptionService _exceptionService;

        public MessengerController(IExceptionService exceptionService)
        {
            _exceptionService = exceptionService ?? throw new ArgumentNullException(nameof(exceptionService));
        }

        [HttpGet("[action]")]
        public string Webhook([FromQuery(Name = "hub.mode")] string mode,[FromQuery(Name = "hub.challenge")] string challenge,[FromQuery(Name = "hub.verify_token")] string verify_token)
        {
            var json = Request.Query;

            if (verify_token.Equals("my_token_is_great"))
            {
                return challenge;
            }
            else
            {
                return "";
            }
        }

        [HttpPost("[action]")]
        public void Webhook()
        {
            var json = (dynamic)null;
            try
            {
                using (StreamReader sr = new StreamReader(this.Request.Body))
                {
                    json = sr.ReadToEnd();
                }
                dynamic data = JsonConvert.DeserializeObject(json);
                postToFB((string)data.entry[0].messaging[0].sender.id, (string)data.entry[0].messaging[0].message.text);
            }
            catch (Exception ex)
            {
                return;
            }
        }

        public void postToFB(string recipientId, string messageText)
        {
            //Post to ApiAi
            string messageTextAnswer = postApiAi(messageText);
            string postParameters = string.Format("access_token={0}&recipient={1}&message={2}", fbToken, "{ id:" + recipientId + "}", "{ text:\"" + messageTextAnswer + "\"}");
            //Response from ApiAI or answer to FB question from user post it to   FB back.
            var client = new HttpClient();
            client.PostAsync(postUrl, new StringContent(postParameters, Encoding.UTF8, "application/json"));
        }

        private string apiAiToken = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxx";
        public String postApiAi(string messageText)
        {
            //var config = new AIConfiguration(apiAiToken,SupportedLanguage.English);
            //apiAi = new ApiAi(config);
            //var response = apiAi.TextRequest(messageText);
            return null;  //response.Result.Fulfillment.Speech;
        }
    }
}
