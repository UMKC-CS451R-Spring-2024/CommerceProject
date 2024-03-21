using Commerce.WebAPI.DataTransferObjects;
using Commerce.WebAPI.Responses;
using Newtonsoft.Json;

namespace Commerce.WebAPI.Repositories
{
    public class GetAnnualReturnsRepository : IGetAnnualReturnsRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration configuration;

        public GetAnnualReturnsRepository(
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            this.configuration = configuration;
        }

        public async Task<GetAnnualReturnsResponse> GetAnnualReturns(string symbol)
        {
            var response = new GetAnnualReturnsResponse() { Symbol = symbol };

            var dataResult = await GetStockData(symbol);
            if (dataResult == null || !dataResult.Any()) 
                return new GetAnnualReturnsResponse();

            response.AnnualReturns = 
                CalculateAnnualReturns(
                    ExtractEndOfYearMonths(dataResult));

            return response;
        }

        private static Dictionary<int, double> CalculateAnnualReturns(List<(DateOnly, MonthlyAdjustedData)> monthsToCalculate)
        {
            var annualReturns = new Dictionary<int, double>();
            for (var i = 1; i < monthsToCalculate.Count; i++)
            {
                annualReturns.Add(monthsToCalculate[i].Item1.Year,
                    100 * ((monthsToCalculate[i].Item2.AdjustedClose / monthsToCalculate[i - 1].Item2.AdjustedClose) - 1));
            }
            return annualReturns;
        }

        private async Task<IOrderedEnumerable<KeyValuePair<DateOnly, MonthlyAdjustedData>>?> GetStockData(string symbol)
        {
            var client = _httpClientFactory.CreateClient("alphavantage");
            var avConfig = configuration.GetSection("AlphaVantage");
            var dataResponse = await client.GetAsync($"query?function=TIME_SERIES_MONTHLY_ADJUSTED&symbol={symbol}&apikey={avConfig["apiKey"]}");
            if (dataResponse.IsSuccessStatusCode)
            {
                var dataResult = JsonConvert.DeserializeObject<TimeSeriesMonthlyAdjusted>(
                    await dataResponse.Content.ReadAsStringAsync())?.MonthlyAdjustedTimeSeries.OrderBy(x => x.Key);
                return dataResult;
            }
            else return Enumerable.Empty<KeyValuePair<DateOnly, MonthlyAdjustedData>>().OrderBy(x => 1);
        }

        private static List<(DateOnly, MonthlyAdjustedData)> ExtractEndOfYearMonths(IOrderedEnumerable<KeyValuePair<DateOnly, MonthlyAdjustedData>> dataResult)
        {
            var currentYear = 0;
            var enumerator = dataResult.GetEnumerator();
            var lastValue = dataResult.Last();
            var monthsToCalculate = new List<(DateOnly, MonthlyAdjustedData)>();
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
                if (currentYear.Equals(0))
                {
                    currentYear = current.Key.Year;
                    monthsToCalculate.Add((current.Key, current.Value));

                }
                if (enumerator.MoveNext())
                {
                    var next = enumerator.Current;
                    if (!next.Key.Year.Equals(currentYear))
                    {
                        monthsToCalculate.Add((current.Key, current.Value));
                        currentYear = next.Key.Year;
                    }

                    if (next.Equals(lastValue))
                        monthsToCalculate.Add((next.Key, next.Value));
                }
                else
                {
                    monthsToCalculate.Add((current.Key, current.Value));
                }
            }

            return monthsToCalculate;
        }
    }
}
