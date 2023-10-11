using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainObjects
{

    public class ApiResponse
    {
        public string clubno { get; set; }
        public string raceno { get; set; }
        public string ringno { get; set; }
        public string pring_no { get; set; }
        public string backtime { get; set; }
        public string deviceno { get; set; }
    }
    public class TopPigeonAPIResponce
    {
        public List<ApiResponse> Data { get; set; }
    }
}
