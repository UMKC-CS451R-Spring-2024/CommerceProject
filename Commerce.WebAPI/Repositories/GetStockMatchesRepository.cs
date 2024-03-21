using Commerce.WebAPI.DataTransferObjects;
using Commerce.WebAPI.Responses;
using Newtonsoft.Json;
using System.Net.Http;

namespace Commerce.WebAPI.Repositories
{
    public class GetStockMatchesRepository : IGetStockMatchesRepository
    {
        private IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration configuration;

        public GetStockMatchesRepository(
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            this.configuration = configuration;
        }

        public async Task<StockSearchResults> GetStockMatches(string symbol)
        {
            var client = _httpClientFactory.CreateClient("alphavantage");
            var avConfig = configuration.GetSection("AlphaVantage");
            var dataResponse = await client.GetAsync($"query?function=Symbol_Search&keywords={symbol}&apikey={avConfig["ApiKey"]}");
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
