using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Caching.Memory;
using Api.Responses;
using System;
using System.Linq;
using Api.Repositories;

namespace Api
{
    public class GetAnnualReturnsAPI
    {
        private readonly IMemoryCache _cache;
        private readonly ILogger<GetAnnualReturnsAPI> _logger;
        private readonly IGetAnnualReturnsRepository _getAnnualReturnsRepository;

        public GetAnnualReturnsAPI(
            IMemoryCache cache,
            ILogger<GetAnnualReturnsAPI> log,
            IGetAnnualReturnsRepository getAnnualReturnsRepository)
        {
            _cache = cache;
            _logger = log;
            _getAnnualReturnsRepository = getAnnualReturnsRepository;
        }

        [FunctionName("GetAnnualReturns")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Stock Data" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiParameter(name: "symbol", In = ParameterLocation.Query, Required = true,
            Type = typeof(string), Description = "The Stock Symbol you want returns calculated for")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "text/plain",
            bodyType: typeof(string), Description = "Invalid or missing symbol")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.NotFound, contentType: "text/plain",
            bodyType: typeof(string), Description = "No returns could be found for this symbol")]
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
            if (_cache.TryGetValue($"GetAnnualReturns-{symbol}", out var cachedReturns)
                && cachedReturns is GetAnnualReturnsResponse)
                return new OkObjectResult(cachedReturns);

            var returns = await _getAnnualReturnsRepository.GetAnnualReturns(symbol);
            if (returns.AnnualReturns.Any())
            {
                _cache.Set($"GetAnnualReturns-{symbol}", returns, TimeSpan.FromMinutes(5));
                return new OkObjectResult(returns);
            }
            else
            {
                return new NotFoundObjectResult($"No results found for symbol: {symbol}");
            }
        }
    }
}

