using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DomainObject
{
    public class TopigeonTraining
    {
        public string UserID { get; set; }
        public string Liberation { get; set; }
        public string DateRelease { get; set; }
        public string ReleaseTime { get; set; }
        public string EclockId { get; set; }
        public string ClubName { get; set; }
        public string Source { get; set; }
        public string LatDeg { get; set; }
        public string LatMin { get; set; }
        public string LatSec { get; set; }
        public string LatSign { get; set; }
        public string LongDeg { get; set; }
        public string LongMin { get; set; }
        public string LongSec { get; set; }
        public string LongSign { get; set; } 

    }
}
