using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainObject
{

    public class Odd : Fight
    {
        public string Winner { get; set; }
        public string WalaOdds { get; set; }
        public string WalaTotal { get; set; }
        public string MeronOdds { get; set; }
        public string MeronTotal { get; set; }
        public string DrawTotal { get; set; }
    }
}
