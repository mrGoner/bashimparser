using System;

namespace BashImParser
{
    public class BashParserNotFoundException : Exception
    {
        public BashParserNotFoundException(string _element, string _data) : base($"Attribute or class {_element} not found in {_data}")
        {

        }

        public BashParserNotFoundException(string _message, Exception _innerException) : base(_message, _innerException)
        {
        }
    }
}
