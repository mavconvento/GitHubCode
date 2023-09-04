using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Helper
{
    public class Helper
    {
        //var ret = itexmo(MobileNumber, ReplyMessage, "PR-MARKA754822_4H5EX", SenderID, "nc]xkei6ti");
        public IConfiguration _configuration { get; }
        public Helper(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<object> itexmo(string Number, string Message, string API_CODE = "", string SenderID = "", string Password = "", Boolean isImportant = false)
        {
            object functionReturnValue = null;
            using (System.Net.WebClient client = new System.Net.WebClient())
            {
                System.Collections.Specialized.NameValueCollection parameter = new System.Collections.Specialized.NameValueCollection();
                string url = _configuration["ItextMo:Url"];
                parameter.Add("1", Number);
                parameter.Add("2", Message);
                parameter.Add("3", _configuration["ItextMo:APICode"]);
                parameter.Add("6", _configuration["ItextMo:SenderID"]);
                parameter.Add("passwd", _configuration["ItextMo:Secret"]);

                if (isImportant)
                {
                    parameter.Add("5", "HIGH");
                }

                dynamic rpb = client.UploadValues(url, "POST", parameter);
                functionReturnValue = (new System.Text.UTF8Encoding()).GetString(rpb);
            }
            return  await Task.FromResult(functionReturnValue);
        }
    }
}
