using Api.DataTransferObjects.SymbolSearch;
using Api.Responses;

namespace Api.Factories
{
    public interface IGetStockMatchesResponseFactory
    {
        GetStockMatchesResponse CreateStockInformation(DailyHighsSeriesResults stockSearchResults);
    }
}