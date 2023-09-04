using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainObject
{
   
    public class Fight
    {
        public string eventId { get; set; }
        public string fightId { get; set; }
        public string fightNo { get; set; }
        public string status { get; set; }
        public string declare { get; set; }
        public bool isLastCall { get; set; }
        public string requestStatus { get; set; }
        public string userRole { get; set; }
    }
}
