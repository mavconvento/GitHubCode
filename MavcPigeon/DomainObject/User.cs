using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DomainObject
{
    [Table("mavcpigeon_user")]
    public class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid? UserID { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64? DisplayID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime DateCreated { get; set; }

        [NotMapped]
        public string Token { get; set; }
    }
}
