using DomainObject.DatabaseObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainObject
{

    public class Bet : User
    {
        public string eventId { get; set; }
        public string fightId { get; set; }
        public string fightNo { get; set; }
        public string betAmount { get; set; }
        public string betType { get; set; }
        public string platformRefId { get; set; }

        public string betOffline { get; set; }

        //value:{confirmed,failed}
        public string betstatus { get; set; }
        public string message { get; set; }

    }
}
