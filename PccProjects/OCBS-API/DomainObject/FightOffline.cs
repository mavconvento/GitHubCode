using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainObject
{
    public class FightOffline
    {
        public string eventid { get; set; }
        public string fightno { get; set; }
        public string userid { get; set; }
        public string status { get; set; }
        public string declare { get; set; }
        public bool lastCall { get; set; }
    }
}
