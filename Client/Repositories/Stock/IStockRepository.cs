using Client.Repositories.Stock.Responses;

namespace Client.Repositories.Stock
{
    public interface IStockRepository
    {
        Task<GetAnnualReturnsResponse> GetAnnualReturns(string symbol);
    }
}