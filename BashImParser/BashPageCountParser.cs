using System;
using System.Linq;
using HtmlAgilityPack;

namespace BashImParser
{
    internal class BashPageCountParser
    {
        private const string DataPageAttribute = "data-page";
        private const string QuotesClass = "quotes";

        public int GetPageCount(HtmlDocument _document)
        {
            if (_document == null)
                throw new ArgumentNullException(nameof(_document));

            try
            {
                var quotesRaw = _document.DocumentNode.SelectNodes(string.Format("//section[contains(@class,'{0}')]", QuotesClass)).FirstOrDefault();

                if (quotesRaw == null)
                    throw new BashParserNotFoundException(QuotesClass, _document.ToString());

                var countRaw = quotesRaw.GetAttributeValue(DataPageAttribute, -1);

                if (countRaw == -1)
                    throw new BashParserNotFoundException(DataPageAttribute, quotesRaw.ToString());

                return countRaw;
            }
            catch (Exception _ex)
            {
                throw new BashParserException("Failed to parse pages count", _ex);
            }
        }
    }
}
