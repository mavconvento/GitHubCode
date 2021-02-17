using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DomainObject
{
    [Table("OtpCodes")]
    public class OTPCodes
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Int64 ID { get; set; }
        public string OTPCode { get; set; }
        public string MobileNumber { get; set; }
        public DateTime DateGenerated { get; set; }
        public Int64 ReferenceID { get; set; }
        public Int64 Duration { get; set; }
        public bool IsUsed { get; set; }

    }
}
