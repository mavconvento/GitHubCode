using DomainObject.DatabaseObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainObject
{
    public class Payout : User
    {
        public Int64 bettingId { get; set; }
        public Int64 eventId { get; set; }
        public string referenceId { get; set; }
        public string payoutAmount { get; set; }
        public string winningSide { get; set; }
        public string odds { get; set; }
    }
}
