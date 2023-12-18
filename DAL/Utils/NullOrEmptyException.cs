using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class NullOrEmptyException : Exception
    {
        public NullOrEmptyException() : base() { }
        public NullOrEmptyException(string message) : base(message) { }
        public NullOrEmptyException(string message, Exception innerException) :
            base(message, innerException) { }
    }
}