using API.Responses;
using Client.Repositories.Stock.Responses;

namespace Client.Repositories.Stock
{
    public interface IStockRepository
    {
        Task<GetAnnualReturnsResponse> GetAnnualReturns(string symbol);
        Task<GetDailyHighsResponse> GetDailyHighs(string symbol);
        Task<GetStockMatchesResponse> GetStockMatches(string symbol);
    }
}