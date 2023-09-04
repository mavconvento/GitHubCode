using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainObject.PlatformObject
{
    public class PlatformBetResponse
    {
        public int success { get; set; }
        public string message { get; set; }
        public Decimal current_points { get; set; }
        public Int64 inserId { get; set; }
    }
}
