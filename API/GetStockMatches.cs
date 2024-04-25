using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using API.Factories;
using API.Repositories;
using API.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace API
{
    public class GetStockMatches
    {
        private readonly IMemoryCache _cache;
        private readonly ILogger<GetStockMatches> _logger;
        private readonly IGetStockMatchesRepository _repository;
        private readonly IGetStockMatchesResponseFactory _getStockMatchesResponseFactory;

        public GetStockMatches(
            IMemoryCache cache,
            ILogger<GetStockMatches> log,
            IGetStockMatchesRepository repository,
            IGetStockMatchesResponseFactory getStockMatchesResponseFactory)
        {
            _cache = cache;
            _logger = log;
            _repository = repository;
            _getStockMatchesResponseFactory = getStockMatchesResponseFactory;
        }

        [FunctionName("GetStockMatches")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Stock Data" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiParameter(name: "symbol", In = ParameterLocation.Query, Required = true,
            Type = typeof(string), Description = "The Stock Symbol you want to match")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "text/plain",
            bodyType: typeof(string), Description = "Invalid or missing symbol")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.NotFound, contentType: "text/plain",
            bodyType: typeof(string), Description = "No matches could be found for this symbol")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            string symbol = req.Query["symbol"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            symbol = symbol ?? data?.symbol;
            if (string.IsNullOrWhiteSpace(symbol))
            {
                return new BadRequestObjectResult($"Invalid or missing symbol");
            }

            if (_cache.TryGetValue($"GetStockMatches-{symbol}", out var cachedMatches)
                && cachedMatches is GetStockMatchesResponse)
                return new OkObjectResult(cachedMatches);

            var results = await _repository.GetStockMatches(symbol);
            var response = _getStockMatchesResponseFactory.CreateStockInformation(results);
            if (response.StockMatches.Any())
            {
                _cache.Set($"GetStockMatches-{symbol}", response, TimeSpan.FromMinutes(5));
                return new OkObjectResult(response);
            }
            else
            {
                return new NotFoundObjectResult($"No matches found for symbol: {symbol}");
            }
        }
    }
}

