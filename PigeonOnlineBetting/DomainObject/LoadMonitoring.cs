using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DomainObject
{
    [Table("LoadMonitoring")]
    public class LoadMonitoring
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Int64 ID { get; set; }
        public string MobileNumber { get; set; }
        public int TotalLoad { get; set; }
        public int TotalLoadUsed { get; set; }
        public int Pasaload { get; set; }
        public DateTime DateLastTransaction { get; set; }
        public string ExternalID { get; set; }
        public Guid? UserID { get; set; }
    }
}
