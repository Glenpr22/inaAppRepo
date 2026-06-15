using System;
using System.Collections.Generic;
using System.Text;

namespace inaApp.Common.Exception
{
    public class DuplicateProductNameException : System.Exception
    {
        public DuplicateProductNameException()
        {
        }

        public DuplicateProductNameException(string message)
        : base(message)
        {
        }
    }
}
