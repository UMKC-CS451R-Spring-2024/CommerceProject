using Commerce.WebAPI.Repositories;
using Commerce.WebAPI.Requests;
using Commerce.WebAPI.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;


namespace Commerce.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StockController : ControllerBase
    {
        private readonly ILogger<StockController> _logger;
        private readonly IGetAnnualReturnsRepository getAnnualReturnsRepository;

        public StockController(
            ILogger<StockController> logger,
            IGetAnnualReturnsRepository getAnnualReturnsRepository)
        {
            _logger = logger;
            this.getAnnualReturnsRepository = getAnnualReturnsRepository;
        }

        [HttpGet(Name = "GetTimeSeriesMonthlyAdjusted/symbol")]
        public async Task<GetAnnualReturnsResponse> Get(string symbol)
        {
            return await getAnnualReturnsRepository.GetAnnualReturns(symbol);
        }
    }
}