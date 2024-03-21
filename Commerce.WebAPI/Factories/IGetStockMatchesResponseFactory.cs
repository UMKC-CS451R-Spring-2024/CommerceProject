using Commerce.WebAPI.DataTransferObjects;
using Commerce.WebAPI.Responses;

namespace Commerce.WebAPI.Factories
{
    public interface IGetStockMatchesResponseFactory
    {
        GetStockMatchesResponse CreateStockInformation(StockSearchResults stockSearchResults);
    }
}