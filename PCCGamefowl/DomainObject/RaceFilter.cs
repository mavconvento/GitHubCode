using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DomainObject
{
    public class RaceFilter
    {
        public string UserID { get; set; }
        public String ClubID { get; set; }
        public string DateRelease { get; set; }
        public String FilterName { get; set; }
        public String ClubName { get; set; }
        public String Category { get; set; }
        public String Group { get; set; }
        public String MobileNumber { get; set; }
        public String Source { get; set; }
        public String DbName { get; set; }

    }
}
