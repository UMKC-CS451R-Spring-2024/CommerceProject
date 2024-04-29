using System.Collections.Generic;

namespace Api.Responses
{
    public class GetStockMatchesResponse
    {
        public IEnumerable<StockMatch> StockMatches { get; set; }
    }
}
