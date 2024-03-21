﻿using Commerce.WebAPI.DataTransferObjects;
using Commerce.WebAPI.Responses;

namespace Commerce.WebAPI.Factories
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
