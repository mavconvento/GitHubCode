using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MAVCPigeonClockingMobileApps.Models
{
    public class JsonResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Remarks { get; set; }
        public int ErrorID { get; set; } //1: session timeout, 2: unauthorized
    }
}