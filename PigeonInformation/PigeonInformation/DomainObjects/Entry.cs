using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainObjects
{
    public class Entry
    {
        public String Clubname { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string MemberIDNo { get; set; }
        public string RingNumber { get; set; }
        public String RFID { get; set; }
        public string RaceCategoryName { get; set; }
        public string RaceCategoryGroupName { get; set; }
        public String MobileNumber { get; set; }
    }
}
