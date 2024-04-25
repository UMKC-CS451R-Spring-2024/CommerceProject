using API.Configuration;
using API.Factories;
using API.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

[assembly: FunctionsStartup(typeof(API.Startup))]

namespace API
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddHttpClient("alphavantage", httpClient =>
            {
                httpClient.BaseAddress = new Uri("https://www.alphavantage.co/");
            });

            builder.Services.AddOptions<StockServiceOptions>()
                .Configure<IConfiguration>((settings, configuration) =>
                {
                    configuration.GetSection("StockServiceOptions").Bind(settings);
                });
            builder.Services.AddMemoryCache();
            builder.Services.AddSingleton<IGetStockMatchesResponseFactory, GetStockMatchesResponseFactory>();
            builder.Services.AddSingleton<IGetStockMatchesRepository, GetStockMatchesRepository>();
            builder.Services.AddSingleton<IGetAnnualReturnsRepository, GetAnnualReturnsRepository>();
            builder.Services.AddSingleton<IGetDailyHighsRepository,  GetDailyHighsRepository>();

            //builder.Services.AddSingleton<ILoggerProvider, MyLoggerProvider>();
        }
    }
}
