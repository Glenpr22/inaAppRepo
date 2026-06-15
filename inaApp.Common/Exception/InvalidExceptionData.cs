using System;
using System.Collections.Generic;
using System.Text;

namespace inaApp.Common.Exception
{
    public class InvalidExceptionData : System.Exception
    {
        public InvalidExceptionData()
        {
        }

        public InvalidExceptionData(string? message) : base(message)
        {
        }
    }//end class
}//end namespace
