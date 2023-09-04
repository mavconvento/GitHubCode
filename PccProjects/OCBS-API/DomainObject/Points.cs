using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainObject
{
    public class Points
    {
        public Int64 UserId { get; set; }
        public Int64 TellerId { get; set; }
        public Decimal Amount { get; set; }
        public string Type { get; set; }
        public Int64 Eventid { get; set; }
    }
}
