using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainObject.DatabaseObject
{
    public class UnClaimed
    {
        public string ReferenceId { get; set; }
        public string FightNo { get; set; }
        public string BetAmount { get; set; }
        public string Odds { get; set; }
        public string Payout { get; set; }
        public string Teller { get; set; }
    }
}
