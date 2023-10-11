using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainObjects
{
    public class TopPigeonPigData
    {
        public string ClockId { get; set; }
        public string LoftName { get; set; }
        public string LoftNo { get; set; }
        public string PRingNo { get; set; }
        public string RCountry { get; set; }
        public string RYear { get; set; }
        public string RRegLetter { get; set; }
        public string RRegNumber { get; set; }
        public string Sex { get; set; }
        public string E_Ring { get; set; }
        public string ColorType { get; set; }
        public string Comment { get; set; }
        public int ActiveStat { get; set; }
        public DateTime Updatetime { get; set; }
        public int SynchFlag { get; set; }
        public int RandomCode { get; set; }
        public string UID { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime AssignDate { get; set; }
        public int OtherClub { get; set; }
        public string Source { get; set; }
        public DateTime BatchDatetime { get; set; }

    }
}
