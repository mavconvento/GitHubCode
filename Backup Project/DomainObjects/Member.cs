using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainObjects
{
    public class Member
    {
        public String ID { get; set; }
        public Int64 ClubID { get; set; }
        public Int64 UserID { get; set; }
        public PersonalInfo MemberDetails { get; set; }
        public List<MobileNumber> MobileNumbers { get; set; } 
        public Coordinates Distance { get; set; }
        public DateTime DateofMembership { get; set; }
        public DateTime LastRenewalDate { get; set; }
        public DateTime DateofExpiration { get; set; }
        public Boolean DeactivateMember { get; set; }
        public Boolean IsActive { get; set; }
        public string Type { get; set; }

        public Int64 BandID { get; set; }
        public string RingNumber { get; set; }
        public string RaceScheduleName { get; set; }
        public string RaceScheduleCategoryName { get; set; }
        public Int64 LocationID { get; set; }
        public String ReaderID { get; set; }
        public String ClubName { get; set; }
        public Boolean Overwrite { get; set; }
    }
}
