using System;
using System.Collections.Generic;
using System.Text;

namespace DomainObject.AppServices
{
    public class CustomException: Exception
    {
        public String ErrorMessage { get; set; }
        public CustomException() { }
        public CustomException(String message)
        {
            ErrorMessage = message;
        }
        public override string Message
        {
            get
            {
                return ErrorMessage;
            }
        }

    }
}
