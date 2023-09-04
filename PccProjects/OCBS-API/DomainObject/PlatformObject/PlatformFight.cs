using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainObject.PlatformObject
{
    public class PlatformFight
    {
        public PlatfromFightDetails details { get; set; }
        public List<PlatformBet> bet { get; set; }
    }

    public class PlatformFightWithDeclare
    {
        public PlatfromFightDetails details { get; set; }
        public string declare { get; set; }
        public List<PlatformBet> bet { get; set; }
    }
}
