using Client;
using Client.Repositories.Stock;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

AddRepositories(builder);



builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddHttpClient("Stock", httpClient =>
{
    httpClient.BaseAddress = new Uri("https://localhost:7283/");
});


builder.Services.AddBlazorBootstrap();

builder.Services.AddOidcAuthentication(options =>
{
    // Configure your authentication provider options here.
    // For more information, see https://aka.ms/blazor-standalone-auth
    builder.Configuration.Bind("Local", options.ProviderOptions);
});

await builder.Build().RunAsync();

void AddRepositories(WebAssemblyHostBuilder builder)
{
    builder.Services.AddSingleton<IStockRepository, StockStaticARRepository>();
}