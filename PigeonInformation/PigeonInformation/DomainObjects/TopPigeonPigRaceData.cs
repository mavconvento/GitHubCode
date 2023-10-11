using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainObjects
{
    public class TopPigeonPigRaceData
    {
        public string ClubID { get; set; }
        public string LiberCode { get; set; }
        public DateTime LiberDate { get; set; }
        public string PRingNo { get; set; }
        public string BackTime { get; set; }
        public string FlyTime { get; set; }
        public decimal FlySpeed { get; set; }
        public int? Dist { get; set; }
        public int RandomCode { get; set; }
        public string ClockID { get; set; }
        public string UID_Real { get; set; }
        public int? TimeVari { get; set; }
        public DateTime MarkedTime { get; set; }
        public string RealRandom { get; set; }
        public string BackLon { get; set; }
        public string BackLat { get; set; }

    }
}
