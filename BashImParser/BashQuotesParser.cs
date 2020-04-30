using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HtmlAgilityPack;

namespace BashImParser
{
    internal class BashQuotesParser
    {
        private const string QuoteClass = "quote";
        private const string QuoteFrameClass = "quote__frame";
        private const string QuoteDateClass = "quote__header_date";
        private const string QuoteBodyClass = "quote__body";
        private const string QuoteIdAttribute = "data-quote";
        private const string QuoteHeaderClass = "quote__header";
        private const string QuoteStripsClass = "quote__strips";
        private const string QuoteStripElemetClass = "href";

        public Quote[] Parse(HtmlDocument _document)
        {
            if (_document == null)
                throw new System.ArgumentNullException(nameof(_document));

            try
            {
                var quotesRaw = _document.DocumentNode.SelectNodes(string.Format("//article[contains(@class,'{0}')]", QuoteClass));

                if (!quotesRaw.Any())
                    return new Quote[0];

                var parsedQuotes = new List<Quote>();

                foreach (var quoteRaw in quotesRaw)
                {
                    var quote = new Quote();

                    var quoteId = quoteRaw.GetAttributeValue(QuoteIdAttribute, -1);

                    if (quoteId == -1)
                        throw new BashParserNotFoundException(QuoteIdAttribute, quoteRaw.ToString());

                    quote.Id = quoteId;

                    var quoteFrame = quoteRaw.ChildNodes.FirstOrDefault(_x => _x.HasClass(QuoteFrameClass));

                    if (quoteFrame == null)
                        throw new BashParserNotFoundException(QuoteFrameClass, quoteRaw.ToString());

                    var quoteHeader = quoteFrame.ChildNodes.FirstOrDefault(_x => _x.HasClass(QuoteHeaderClass));

                    if (quoteHeader == null)
                        throw new BashParserNotFoundException(QuoteHeaderClass, quoteRaw.ToString());

                    var dateRaw = quoteHeader.ChildNodes.FirstOrDefault(_x => _x.HasClass(QuoteDateClass));

                    if (dateRaw == null)
                        throw new BashParserNotFoundException(QuoteDateClass, quoteRaw.ToString());

                    quote.Date = ParseQuoteDateTime(dateRaw.InnerText);

                    var quoteBody = quoteFrame.ChildNodes.FirstOrDefault(_x => _x.HasClass(QuoteBodyClass));

                    if (quoteBody == null)
                        throw new BashParserNotFoundException(QuoteBodyClass, quoteRaw.ToString());

                    var quoteStrips = quoteBody.ChildNodes.FirstOrDefault(_x => _x.HasClass(QuoteStripsClass));

                    if (quoteStrips != null)
                    {
                        var links = quoteStrips.SelectNodes(string.Format(".//*[contains(@class,'{0}')]", "quote__strips_link"));

                        quote.Strips = links.Select(_x => _x.GetAttributeValue("href", null)).ToArray();

                        quoteBody.RemoveChild(quoteStrips);
                    }
                    else
                        quote.Strips = new string[0];

                    quote.Content = HttpUtility.HtmlDecode(quoteBody.InnerHtml).Trim().Replace("<br>", "\n");


                    parsedQuotes.Add(quote);
                }

                return parsedQuotes.ToArray();
            }
            catch (Exception _ex)
            {
                throw new BashParserException("Failed to parse qoutes", _ex);
            }
        }

        private DateTime ParseQuoteDateTime(string _dateTimeRaw)
        {
            var dateTimeRaw = _dateTimeRaw.Replace(" ", "").Replace("\n", "").Replace("в", " ");

            return DateTime.ParseExact(dateTimeRaw, "dd.MM.yyyy H:mm", null);
        }
    }
}
