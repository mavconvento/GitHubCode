using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Helper
{
    public class CustomMesageError
    {
        public String Message { get; set; }
        public CustomMesageError(string message)
        {
            Message = message;
            if (message.Contains("network-related or instance-specific error"))
            {
                Message = "Connection Error.";
            }
           
        }
    }
}
