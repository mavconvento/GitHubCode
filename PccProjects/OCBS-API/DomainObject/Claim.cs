using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainObject
{
    public class Claim
    {
        public string Win { get; set; }
        public string BetAmount { get; set; }
        public string Odds { get; set; }
        public string WinningSide { get; set; }
        public string FightNo { get; set; }
        public string MeronOdds { get; set; }
        public string WalaOdds { get; set; }
        public string DrawOdds { get; set; }
        public string Status { get; set; }
        public Int64 BettingId { get; set; }
    }
}
