using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainObject.PlatformObject
{
    public class PlatfromPlayerBet
    {
        //platform parameters
        public int Event { get; set; }
        public int fightId { get; set; }
        public int fightNumber { get; set; }
        public Decimal amount { get; set; }
        public string bet { get; set; }
    }
}
