using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StandAloneApi.Model
{
    public class Files
    {
        public IFormFile file { get; set; }
        public String filename { get; set; }
        public String iD { get; set; }
    }
}
