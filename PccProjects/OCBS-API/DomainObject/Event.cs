using DomainObject.DatabaseObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainObject
{
    public class Event : User
    {
        public int eventId { get; set; }
        public string event_name { get; set; }
        public bool isoffline { get; set; }
    }
}
