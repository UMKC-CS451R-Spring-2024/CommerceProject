using API.Configuration;
using API.DataTransferObjects;
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
        private readonly StockServiceOptions _settings;

        public GetStockMatchesRepository(
            IHttpClientFactory httpClientFactory,
            IOptions<StockServiceOptions> options)
        {
            _httpClientFactory = httpClientFactory;
            _settings = options.Value;
        }

        public async Task<StockSearchResults> GetStockMatches(string symbol)
        {
            var client = _httpClientFactory.CreateClient("alphavantage");
            var dataResponse = await client.GetAsync($"query?function=Symbol_Search&keywords={symbol}&apikey=DHS96ZUQ1B4IVPO2");
            if (dataResponse.IsSuccessStatusCode)
            {
                var results = await dataResponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<StockSearchResults>(results)
                    ?? new StockSearchResults();
            }
            else return new StockSearchResults();
        }
    }
}
