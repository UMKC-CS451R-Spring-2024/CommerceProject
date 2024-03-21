using Commerce.WebAPI.DataTransferObjects;

namespace Commerce.WebAPI.Repositories
{
    public interface IGetStockMatchesRepository
    {
        Task<StockSearchResults> GetStockMatches(string symbol);
    }
}