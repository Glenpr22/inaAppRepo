using System;
using System.Collections.Generic;
using System.Text;

namespace inaApp.Common.Exception
{
    public class InvalidIdException : System.Exception
    {
        public InvalidIdException()
        {
        }
        public InvalidIdException(string message) : base(message)
        {
        }

    }//end class

}//end namespace
