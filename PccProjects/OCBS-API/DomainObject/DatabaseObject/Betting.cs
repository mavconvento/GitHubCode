using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainObject.DatabaseObject
{
    public class Betting
    {
        public Int64 BettingId { get; set; }
        public string ReferenceId { get; set; }
        public string Amount { get; set; }
        public string FightNo { get; set; }
        public string BetType { get; set; }
        public string Status { get; set; }
        public string EventId { get; set; }
        public string PlatformRefId { get; set; }

        public string Message { get; set; }
        public string MeronTotalBet { get; set; }
        public string WalaTotalBet { get; set; }
        public string DrawTotalBet { get; set; }
        public Decimal CurrentPoints { get; set; }
    }
}
