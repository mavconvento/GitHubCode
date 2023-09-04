using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DomainObject
{
    public class MemberLogs
    {
        public String ClubID { get; set; }
        public String MobileNumber { get; set; }
        public String Keyword { get; set; }
        public String DateFrom { get; set; }
        public String DateTo { get; set; }
        public String DBName { get; set; }

    }
}
