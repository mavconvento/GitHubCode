using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainObject.DatabaseObject
{
    public class Event
    {
        public string EventId { get; set; }
        public string Description { get; set; }
        public string PlatformEventId { get; set; }
    }
}
