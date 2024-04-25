using API.Configuration;
using API.DataTransferObjects.SymbolSearch;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace API.Repositories
{
    public class GetStockMatchesRepository : IGetStockMatchesRepository
    {
        private IHttpClientFactory _httpClientFactory;
        //private readonly StockServiceOptions _settings;

        public GetStockMatchesRepository(
            //IOptions<StockServiceOptions> options,
            IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            //_settings = options.Value;
        }

        public async Task<DailyHighsSeriesResults> GetStockMatches(string symbol)
        {
            var client = _httpClientFactory.CreateClient("alphavantage");
            var dataResponse = await client.GetAsync($"query?function=Symbol_Search&keywords={symbol}&apikey=DHS96ZUQ1B4IVPO2");
            if (dataResponse.IsSuccessStatusCode)
            {
                var results = await dataResponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<DailyHighsSeriesResults>(results)
                    ?? new DailyHighsSeriesResults();
            }
            else return new DailyHighsSeriesResults();
        }
    }
}
