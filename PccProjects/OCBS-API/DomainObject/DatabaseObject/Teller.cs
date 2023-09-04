using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainObject.DatabaseObject
{
    public class Teller
    {
        public Int64 Userid { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public string CompanyName { get; set; }
        public string Percent { get; set; }
        public Decimal CurrentPoints { get; set; }
        public Decimal CashAdvance { get; set; }
        public Decimal CashOnhand { get; set; }
        public Decimal Payout { get; set; }
        public Decimal TotalBetRunning { get; set; }
        public Decimal Commision { get; set; }
    }
}
