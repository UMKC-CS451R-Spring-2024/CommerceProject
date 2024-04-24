using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace API.DataTransferObjects
{
    public class StockSearchResults
    {
        public StockSearchResults()
        {
            Results = Enumerable.Empty<StockSearchResult>();
        }

        [JsonProperty("bestMatches")]
        public IEnumerable<StockSearchResult> Results { get; set; }
    }
}
