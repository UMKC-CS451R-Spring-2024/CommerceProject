using Commerce.WebAPI.Factories;
using Commerce.WebAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

AddRepositories(builder);
AddFactories(builder);
builder.Services.AddMemoryCache();
builder.Services.AddHttpClient("alphavantage", httpClient =>
{
    httpClient.BaseAddress = new Uri("https://www.alphavantage.co/");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

static void AddRepositories(WebApplicationBuilder builder)
{
    builder.Services.AddSingleton<IGetAnnualReturnsRepository, GetAnnualReturnsRepository>();
    builder.Services.AddSingleton<IGetStockMatchesRepository, GetStockMatchesRepository>();
}
static void AddFactories(WebApplicationBuilder builder)
{
    builder.Services.AddSingleton<IGetStockMatchesResponseFactory, GetStockMatchesResponseFactory>();
}