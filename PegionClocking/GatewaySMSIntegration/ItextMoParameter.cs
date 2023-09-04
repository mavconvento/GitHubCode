using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GatewaySMSIntegration
{
    public class ItextMoParameter
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string[] Recipients { get; set; }
        public string Message { get; set; }
        public string ApiCode { get; set; }
        public string SenderId { get; set; }
    }
}
