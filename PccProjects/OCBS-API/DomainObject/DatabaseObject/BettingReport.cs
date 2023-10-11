using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainObject.DatabaseObject
{
    public class BettingReport
    {
        public string EventId { get; set; }
        public string FightNo { get; set; }
        public string Meron { get; set; }
        public string Wala { get; set; }
        public string TotalAmount { get; set; }
        public string Status { get; set; }
        public string Declare { get; set; }
        public string PayoutOdd { get; set; }
        public string Commission { get; set; }

    }
}
