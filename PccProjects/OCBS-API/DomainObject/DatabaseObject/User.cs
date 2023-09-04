using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainObject.DatabaseObject
{
    public class User
    {
        public Int64 userId { get; set; }
        public string userName { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public Int64 companyId { get; set; }
        public string companyName { get; set; }
        public string roleDescription { get; set; }
        public string platformBearerToken { get; set; }
        public string platformUserId { get; set; }
        public bool IsOffline { get; set; }
        public string Status { get; set; }

        //additional parameters
        public Int64 RoleId { get; set; }
        public bool IsActive { get; set; }
        public string Password { get; set; }
        public Int64 Id { get; set; }

    }
}
