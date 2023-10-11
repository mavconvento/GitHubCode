using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainObjects
{
    public class TopPigeonRaceCode
    {
        public String RaceCode { get; set; }
        public String ClubID { get; set; }
        public string ClubName { get; set; }
        public DateTime ReleaseDate { get; set; }
        public DateTime LastBackTime { get; set; }
        public string Action { get; set; }
    }
}
