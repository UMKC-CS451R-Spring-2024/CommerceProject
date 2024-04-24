using API.DataTransferObjects;
using API.Responses;

namespace API.Factories
{
    public interface IGetStockMatchesResponseFactory
    {
        GetStockMatchesResponse CreateStockInformation(StockSearchResults stockSearchResults);
    }
}