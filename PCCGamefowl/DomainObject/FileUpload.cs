using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DomainObject
{
    [Table("FileUpload")]
    public class FileUpload
    {
        [Key]
        public Guid FileUploadID { get; set; }
        public byte[] Data { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }

        [NotMapped]
        public IFormFile File { get; set; }
    }   
}
