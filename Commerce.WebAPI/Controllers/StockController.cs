using Commerce.WebAPI.Factories;
using Commerce.WebAPI.Repositories;
using Commerce.WebAPI.Requests;
using Commerce.WebAPI.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;


namespace Commerce.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StockController : ControllerBase
    {
        private readonly IMemoryCache cache;
        private readonly ILogger<StockController> _logger;
        private readonly IGetAnnualReturnsRepository getAnnualReturnsRepository;
        private readonly IGetStockMatchesRepository getStockInformationRepository;
        private readonly IGetStockMatchesResponseFactory getStockInformationResponseFactory;

        public StockController(
            IMemoryCache cache,
            ILogger<StockController> logger,
            IGetAnnualReturnsRepository getAnnualReturnsRepository,
            IGetStockMatchesRepository getStockInformationRepository,
            IGetStockMatchesResponseFactory getStockInformationResponseFactory)
        {
            _logger = logger;
            this.cache = cache;
            this.getAnnualReturnsRepository = getAnnualReturnsRepository;
            this.getStockInformationRepository = getStockInformationRepository;
            this.getStockInformationResponseFactory = getStockInformationResponseFactory;
        }

        [HttpGet("~/GetAnnualReturns")]
        public async Task<GetAnnualReturnsResponse> GetAnnualReturns(string symbol)
        {
            if (cache.TryGetValue($"GetAnnualReturns-{symbol}", out var cachedReturns)
                && cachedReturns is GetAnnualReturnsResponse)
                return cachedReturns as GetAnnualReturnsResponse;
            var returns = await getAnnualReturnsRepository.GetAnnualReturns(symbol);
            if (returns.AnnualReturns.Any())
            {
                cache.Set($"GetAnnualReturns-{symbol}", returns, TimeSpan.FromMinutes(5));
            }
            return returns;
        }

        [HttpGet("~/GetStockMatches")]
        public async Task<GetStockMatchesResponse> GetStockMatches(string symbol)
        {
            if (cache.TryGetValue($"GetStockMatches-{symbol}", out var cachedMatches)
                && cachedMatches is GetStockMatchesResponse)
                return cachedMatches as GetStockMatchesResponse;
            var results =  await getStockInformationRepository.GetStockMatches(symbol);
            var response = getStockInformationResponseFactory.CreateStockInformation(results);
            if (response.StockMatches.Any())
            {
                cache.Set($"GetStockMatches-{symbol}", response, TimeSpan.FromMinutes(5));
            }
            return response;
        }
    }
}