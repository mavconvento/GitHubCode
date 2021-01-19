using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DomainObject
{
    public class Profile
    {
        public Guid UserID { get; set; }
        public IFormFile Image { get; set; }
        public String LoftName { get; set; }
    }
}
