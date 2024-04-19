using API.DataTransferObjects;
using API.Responses;
using System.Linq;

namespace API.Factories
{
    public class GetStockMatchesResponseFactory : IGetStockMatchesResponseFactory
    {
        public GetStockMatchesResponse CreateStockInformation(StockSearchResults stockSearchResults)
        {
            return new GetStockMatchesResponse
            {
                StockMatches = stockSearchResults.Results.Select(
                    x => new StockMatch
                    {
                        Currency = x.Currency,
                        MarketClose = x.MarketClose,
                        MarketOpen = x.MarketOpen,
                        MatchScore = x.MatchScore,
                        Name = x.Name,
                        Region = x.Region,
                        Symbol = x.Symbol,
                        Timezone = x.Timezone,
                        Type = x.Type
                    })
            };
        }
    }
}
