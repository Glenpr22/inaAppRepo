using System;
using System.Collections.Generic;
using System.Text;

namespace inaApp.Common.Exception
{
    public class InvalidStockException : System.Exception
    {
        public InvalidStockException()
        {
        }

        public InvalidStockException(string message)
        : base(message)
        {
        }

    }
}
