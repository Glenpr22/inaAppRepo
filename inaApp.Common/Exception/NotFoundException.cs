using System;
using System.Collections.Generic;
using System.Text;

namespace inaApp.Common.Exception
{
    public class NotFoundException : System.Exception
    {
        public NotFoundException(string message) : base(message)
        {
        }

        public NotFoundException()
        {
        }
    }
}
