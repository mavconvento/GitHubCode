using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainObject.DatabaseObject
{
    public class Company
    {
        public Int64 CompanyId { get; set; }
        public string CompanyName { get; set; }
        public bool IsOffline { get; set; }

    }
}
