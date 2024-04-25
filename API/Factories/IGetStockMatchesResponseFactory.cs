using API.DataTransferObjects.SymbolSearch;
using API.Responses;

namespace API.Factories
{
    public interface IGetStockMatchesResponseFactory
    {
        GetStockMatchesResponse CreateStockInformation(DailyHighsSeriesResults stockSearchResults);
    }
}