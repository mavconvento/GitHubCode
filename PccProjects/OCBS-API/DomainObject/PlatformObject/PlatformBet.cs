using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainObject.PlatformObject
{
    public class PlatformBet
    {
        public string bet_type { get; set; }
        public Decimal? bet { get; set; }
        public Decimal? totalBet { get; set; }
        public Decimal? odds { get; set; }
        public Decimal? winning { get; set; }

        
    }
}
