using System;

namespace OCBS_API.Helper
{
    public class CustomError
    {
        public String Message { get; set; }
        public CustomError(string message)
        {
            Message = message;
            if (message.Contains("network-related or instance-specific error"))
            {
                Message = "Connection Error.";
            }
        }
    }
}
