using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainObjects
{
    public class MobileNumber
    {
        public int RegistrationID { get; set; }
        public int ClubID { get; set; }
        public String MobileNo { get; set; }
        public DateTime DateRegister { get; set; }
        public DateTime LastTransaction { get; set; }
        public DateTime DateUnregister { get; set; }
        public Boolean IsActive { get; set; }
        public Double Load { get; set; }

    }
}
