using System.Collections.Generic;

namespace API.Responses
{
    public class GetStockMatchesResponse
    {
        public IEnumerable<StockMatch> StockMatches { get; set; }
    }
}
