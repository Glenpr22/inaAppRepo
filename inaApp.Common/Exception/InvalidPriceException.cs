using System;
using System.Collections.Generic;
using System.Text;

namespace inaApp.Common.Exception
{
    public class InvalidPriceException : System.Exception
    {

        public InvalidPriceException()
        {
        }

        public InvalidPriceException(string message)
        : base(message)
        {
        }
    }
}
