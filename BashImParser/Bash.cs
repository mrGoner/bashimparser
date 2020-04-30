using System;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace BashImParser
{
    public class Bash
    {
        private readonly HtmlWeb m_web;
        private const string DefaultAgent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/77.0.3865.120 Safari/537.36";
        private readonly BashUrlBuilder m_urlBuilder;
        private readonly BashQuotesParser m_quotesParser;
        private readonly BashPageCountParser m_pageCountParser;

        public Bash(string _userAgent)
        {
            m_web = new HtmlWeb()
            {
                UserAgent = _userAgent
            };

            m_urlBuilder = new BashUrlBuilder("https://bash.im");
            m_quotesParser = new BashQuotesParser();
            m_pageCountParser = new BashPageCountParser();
        }

        public Bash() : this(DefaultAgent)
        {

        }

        public async Task<Quote[]> GetRandomQuotesAsync()
        {
            try
            {
                var page = await m_web.LoadFromWebAsync(m_urlBuilder.BuildUrlForRandomQuotes());

                return m_quotesParser.Parse(page);
            }
            catch(Exception _ex)
            {
                throw new BashException($"Exception occured in {nameof(GetRandomQuotesAsync)}", _ex);
            }
        }

        public async Task<int> GetPagesCount()
        {
            try
            {
                var page = await m_web.LoadFromWebAsync(m_urlBuilder.BuildUrlForNewQuotes(null));

                return m_pageCountParser.GetPageCount(page);
            }
            catch(Exception _ex)
            {
                throw new BashException($"Exception occured in {nameof(GetPagesCount)}", _ex);
            }
        }

        public async Task<Quote[]> GetBestQuotesAsync(int _year, int? _month = null)
        {
            if (_year < 2000)
                throw new ArgumentException($"Invalid year {_year}");

            if (_month.HasValue && (_month < 1 || _month > 12))
                throw new ArgumentException($"Invalid month {_month}");

            try
            {
                var page = await m_web.LoadFromWebAsync(m_urlBuilder.BuildUrlForBestQuotes(_year, _month));

                return m_quotesParser.Parse(page);
            }
            catch(Exception _ex)
            {
                throw new BashException($"Exception occured in {nameof(GetBestQuotesAsync)}", _ex);
            }
        }

        public async Task<Quote[]> GetNewQuotesAsync(int? _page = null)
        {
            if (_page.HasValue && _page.Value < 1)
                throw new ArgumentException($"Invalid page {_page}");

            try
            {
                var page = await m_web.LoadFromWebAsync(m_urlBuilder.BuildUrlForNewQuotes(_page));

                return m_quotesParser.Parse(page);
            }
            catch (Exception _ex)
            {
                throw new BashException($"Exception occured in {nameof(GetNewQuotesAsync)}", _ex);
            }
        }

        public async Task<Quote[]> GetQuotesByRatingAsync(int? _page = null)
        {
            if (_page.HasValue && _page.Value < 1)
                throw new ArgumentException($"Invalid page {_page}");

            try
            {
                var page = await m_web.LoadFromWebAsync(m_urlBuilder.BuildUrlForQuotesSortedByRating(_page));

                return m_quotesParser.Parse(page);
            }
            catch (Exception _ex)
            {
                throw new BashException($"Exception occured in {nameof(GetQuotesByRatingAsync)}", _ex);
            }
        }

        public async Task<Quote[]> GetNewQuotesAbyssAsync()
        {
            try
            {
                var page = await m_web.LoadFromWebAsync(m_urlBuilder.BuildUrlForNewAbyssQuotes());

                return m_quotesParser.Parse(page);
            }
            catch(Exception _ex)
            {
                throw new BashException($"Exception occured in {nameof(GetNewQuotesAbyssAsync)}", _ex);
            }
        }

        public async Task<Quote[]> GetTopQuotesAbyssAsync()
        {
            try
            {
                var page = await m_web.LoadFromWebAsync(m_urlBuilder.BuildUrlForTopAbyssQuotes());

                return m_quotesParser.Parse(page);
            }
            catch (Exception _ex)
            {
                throw new BashException($"Exception occured in {nameof(GetTopQuotesAbyssAsync)}", _ex);
            }
        }

        public async Task<Quote[]> GetBestQuotesAbyssAsync(DateTime? _date = null)
        {
            try
            {
                var page = await m_web.LoadFromWebAsync(m_urlBuilder.BuildUrlForBestAbyssQuotes(_date));

                return m_quotesParser.Parse(page);
            }
            catch (Exception _ex)
            {
                throw new BashException($"Exception occured in {nameof(GetTopQuotesAbyssAsync)}", _ex);
            }
        }
    }
}
