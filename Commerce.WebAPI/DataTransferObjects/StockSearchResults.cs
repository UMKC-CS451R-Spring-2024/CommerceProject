using Newtonsoft.Json;

namespace Commerce.WebAPI.DataTransferObjects
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
