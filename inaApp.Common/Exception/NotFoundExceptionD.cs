using System;
using System.Collections.Generic;
using System.Text;

namespace inaApp.Common.Exception
{
    public class NotFoundExceptionD : System.Exception
    {
        public NotFoundExceptionD(string message) : base(message)
        {
        }

        public NotFoundExceptionD()
        {
        }
    }
}
