using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainObject.PlatformObject
{
    public class PlatfromFightDetails
    {
        public Int64 fightId { get; set; }
        public Int64 fightNumber { get; set; }
        public string status { get; set; }
        public string declare { get; set; }
        public bool isLastCall { get; set; }
        public string userRole { get; set; }
    }
}
