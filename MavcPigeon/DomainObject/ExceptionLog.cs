using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DomainObject
{
    [Table("ExceptionLog")]
    public class ExceptionLog
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string ServiceName { get; set; }
        public string InnerException { get; set; }
        public string UserId { get; set; }
        public string JsonObject { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
