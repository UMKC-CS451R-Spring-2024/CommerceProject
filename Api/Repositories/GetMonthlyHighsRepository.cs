using Api.DataTransferObjects.TimeSeries.Daily;
using Api.DataTransferObjects.TimeSeries.Monthly;
using Api.Responses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Api.Repositories
{
    public class GetMonthlyHighsRepository : IGetMonthlyHighsRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public GetMonthlyHighsRepository(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<GetMonthlyHighsResponse> GetMonthlyHighs(string symbol)
        {
            var response = new GetMonthlyHighsResponse() { Symbol = symbol };

            var dataResult = await GetStockData(symbol);
            if (dataResult == null || !dataResult.Any())
                return new GetMonthlyHighsResponse();

            response.MonthlyHighs = ExtractMonthlyHighs(dataResult);

            return response;
        }

        private async Task<IOrderedEnumerable<KeyValuePair<DateOnly, MonthlyData>>> GetStockData(string symbol)
        {
            var client = _httpClientFactory.CreateClient("alphavantage");
            //var avConfig = stockServiceOptions.GetSection("AlphaVantage");
            var dataResponse = await client.GetAsync($"query?function=TIME_SERIES_MONTHLY&symbol={symbol}&apikey=DHS96ZUQ1B4IVPO2");
            if (dataResponse.IsSuccessStatusCode)
            {
                var dataResult = JsonConvert.DeserializeObject<TimeSeriesMonthlyDTO>(
                    await dataResponse.Content.ReadAsStringAsync())?.MonthlyTimeSeries.OrderBy(x => x.Key);
                return dataResult;
            }
            else return Enumerable.Empty<KeyValuePair<DateOnly, MonthlyData>>().OrderBy(x => 1);
        }

        private IDictionary<DateOnly, double> ExtractMonthlyHighs(IOrderedEnumerable<KeyValuePair<DateOnly, MonthlyData>> monthlyData)
        {
            var dictionary = new Dictionary<DateOnly, double>();
            foreach (var kvp in monthlyData)
            {
                dictionary.Add(kvp.Key, kvp.Value.High);
            }
            return dictionary;
        }
    }
}
