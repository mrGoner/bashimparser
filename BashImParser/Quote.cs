using System;

namespace BashImParser
{
    public class Quote
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public string[] Strips { get; set; }
    }
}
