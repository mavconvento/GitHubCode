using DomainObject.DatabaseObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainObject
{
    public class EventOffline 
    {
        public int eventId { get; set; }
        public string event_name { get; set; }
        public bool IsOffline { get; set; }
        public Int64 userId { get; set; }
        public string platformUserId { get; set; }
    }
}
