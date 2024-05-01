using System.Collections.Generic;

namespace API.Responses
{
    public class GetStockMatchesResponse
    {
        public GetStockMatchesResponse()
        {
            StockMatches = Enumerable.Empty<StockMatch>();
        }

        public IEnumerable<StockMatch> StockMatches { get; set; }
    }
}
