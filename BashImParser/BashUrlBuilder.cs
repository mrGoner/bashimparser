using System;

namespace BashImParser
{
    internal class BashUrlBuilder
    {
        private readonly string m_baseUrl;

        public BashUrlBuilder(string _baseUrl)
        {
            if (string.IsNullOrWhiteSpace(_baseUrl))
                throw new ArgumentException("Base url can not be null or empty", nameof(_baseUrl));

            m_baseUrl = _baseUrl;
        }

        public string BuildUrlForRandomQuotes()
        {
            return $"{m_baseUrl}/random";
        }

        public string BuildUrlForBestQuotes(int _year, int? _month)
        {
            if (_month == null)
                return $"{m_baseUrl}/best/{_year}/{_month}";

            return $"{m_baseUrl}/best/{_year}/{_month.Value}";
        }

        public string BuildUrlForNewQuotes(int? _page)
        {
            if (_page == null)
                return $"{m_baseUrl}";

            return $"{m_baseUrl}/index/{_page.Value}";
        }

        public string BuildUrlForQuotesSortedByRating(int? _page)
        {
            if (_page == null)
                return $"{m_baseUrl}/byrating";

            return $"{m_baseUrl}/byrating/{_page.Value}";
        }

        public string BuildUrlForNewAbyssQuotes()
        {
            return $"{m_baseUrl}/abyss";
        }

        public string BuildUrlForTopAbyssQuotes()
        {
            return $"{m_baseUrl}/abysstop";
        }

        public string BuildUrlForBestAbyssQuotes(DateTime? _date = null)
        {
            if (_date == null)
                return $"{m_baseUrl}/abyssbest";

            return $"{m_baseUrl}/abyssbest/{_date.Value:yyyyMMdd}";
        }
    }
}
