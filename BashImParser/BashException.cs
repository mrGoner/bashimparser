using System;

namespace BashImParser
{
    public class BashException : Exception
    {
        public BashException(string _message) : base(_message)
        {

        }

        public BashException(string _message, Exception _innerException) : base(_message, _innerException)
        {

        }
    }
}
