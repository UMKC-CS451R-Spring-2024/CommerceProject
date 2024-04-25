using API.DataTransferObjects.TimeSeriesDaily;
using API.DataTransferObjects.TimeSeriesMonthlyAdjusted;
using API.Responses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace API.Repositories
{
    public class GetDailyHighsRepository : IGetDailyHighsRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public GetDailyHighsRepository(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<GetDailyHighsResponse> GetDailyHighs(string symbol)
        {
            var response = new GetDailyHighsResponse() { Symbol = symbol };

            var dataResult = await GetStockData(symbol);
            if (dataResult == null || !dataResult.Any())
                return new GetDailyHighsResponse();

            response.DailyHighs = ExtractDailyHighs(dataResult);

            return response;
        }

        private async Task<IOrderedEnumerable<KeyValuePair<DateOnly, TimeSeriesDaily>>> GetStockData(string symbol)
        {
            var client = _httpClientFactory.CreateClient("alphavantage");
            //var avConfig = stockServiceOptions.GetSection("AlphaVantage");
            var dataResponse = await client.GetAsync($"query?function=Time_Series_Daily&symbol={symbol}&apikey=DHS96ZUQ1B4IVPO2");
            if (dataResponse.IsSuccessStatusCode)
            {
                var dataResult = JsonConvert.DeserializeObject<DailyStockData>(
                    await dataResponse.Content.ReadAsStringAsync())?.TimeSeriesDaily.OrderBy(x => x.Key);
                return dataResult;
            }
            else return Enumerable.Empty<KeyValuePair<DateOnly, TimeSeriesDaily>>().OrderBy(x => 1);
        }

        private IDictionary<DateOnly, double> ExtractDailyHighs(IOrderedEnumerable<KeyValuePair<DateOnly, TimeSeriesDaily>> data)
        {
            var dictionary = new Dictionary<DateOnly, double>();
            foreach ( var kvp in data)
            {
                dictionary.Add(kvp.Key, kvp.Value.High);
            }
            return dictionary;
        }
    }
}
