using System;
using System.Collections.Generic;
using BashImParser;
using System.Linq;

namespace BashTestConsoleApp
{
    class Program
    {
        static void Main(string[] _args)
        {
            var bash = new Bash();

            var quotes = bash.GetBestQuotesAbyssAsync(new DateTime(2020, 1, 2)).Result;

            OutputQuotes(quotes);

            Console.ReadKey();
        }

        static void OutputQuotes(IEnumerable<Quote> _quotes)
        {
            foreach (var quote in _quotes)
            {
                Console.WriteLine($"{quote.Id} {quote.Date} {quote.Content} {quote.Strips.FirstOrDefault() ?? "Strips empty"}");
            }
        }
    }
}
