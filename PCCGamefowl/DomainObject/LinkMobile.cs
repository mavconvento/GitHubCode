using System;
using System.Collections.Generic;
using System.Text;

namespace DomainObject
{
    public class LinkMobile
    {
        public string MobileNumber { get; set; }
        public string OtpCode { get; set; }
        public string ReferenceID { get; set; }
        public string Action { get; set; }
        public Guid? UserId { get; set; }
    }
}
