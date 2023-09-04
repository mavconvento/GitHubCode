using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainObject
{
    public class ApiCommonResponse
    {
        public bool isSucess { get; set; }
        public bool isError { get; set; }
        public string message { get; set; }
        public string errorNumber { get; set; }
    }
}
