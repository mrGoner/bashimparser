using System;

namespace BashImParser
{
    public class BashParserException : Exception
    {
        public BashParserException(string _message) : base(_message)
        {
        }

        public BashParserException(string _message, Exception _innerException) : base(_message, _innerException)
        {
        }
    }
}
