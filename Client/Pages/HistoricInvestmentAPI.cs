namespace Client.Pages
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Text.Json;
    using System.Threading.Tasks;

    public class AlphaVantageResponse
    {
        public MetaData? MetaData { get; set; }
        public Dictionary<string, TimeSeriesData>? MonthlyAdjustedTimeSeries { get; set; }
    }

    public class MetaData
    {
        public string? Information { get; set; }
        public string? Symbol { get; set; }
        public string? LastRefreshed { get; set; }
        public string? TimeZone { get; set; }
    }

    public class TimeSeriesData
    {
        public string? Open { get; set; }
        public string? High { get; set; }
        public string? Low { get; set; }
        public string? Close { get; set; }
        public string? AdjustedClose { get; set; }
        public string? Volume { get; set; }
        public string? DividendAmount { get; set; }
    }

    public class InvestmentData
    {
        private readonly HttpClient _httpClient;

        public InvestmentData(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<AlphaVantageResponse> GetStockDataAsync(string apiKey, string symbol)
        {
            var url = $"https://www.alphavantage.co/query?function=TIME_SERIES_MONTHLY_ADJUSTED&symbol={symbol}&apikey={apiKey}";
            var jsonString = await _httpClient.GetStringAsync(url);

            if (string.IsNullOrEmpty(jsonString))
            {
                throw new HttpRequestException("No response from Alpha Vantage API.");
            }

            var response = JsonSerializer.Deserialize<AlphaVantageResponse>(jsonString);

            if (response == null)
            {
                throw new InvalidOperationException("Failed to deserialize the Alpha Vantage API response.");
            }

            return response;
        }

    }

}
