using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainObject.DatabaseObject
{
    public class PointsHistory
    {
        public Decimal PointsAmount { get; set; }
        public string Type { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime DateRequested { get; set; }
    }
}
