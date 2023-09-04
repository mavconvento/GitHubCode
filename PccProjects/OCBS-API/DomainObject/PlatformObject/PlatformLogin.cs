using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainObject.PlatformObject
{
    public class PlatformLogin
    {
        public int success { get; set; }
        public Platformuser data { get; set; }
        public string token { get; set; }
        public string message { get; set; }
    }

    public class Platformuser
    {
        public Int64 userid { get; set; }
        public string role { get; set; }

    }
}
