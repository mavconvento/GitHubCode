using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainObjects
{
    public class PersonalInfo
    {
        public Int64 SystemID { get; set; }
        public String IDNo { get; set; }
        public String Name { get; set; }
        public String LastName { get; set; }
        public String FirstName { get; set; }
        public String MiddleName { get; set; }
        public String ExtensionName { get; set; }
        public String LoftName { get; set; }
        public String Address { get; set; }
        public DateTime DateofBirth
        {
            get; set;
        }
    }
}
