using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Api.DataTransferObjects.SymbolSearch
{
    public class DailyHighsSeriesResults
    {
        public DailyHighsSeriesResults()
        {
            Results = Enumerable.Empty<StockSearchResult>();
        }

        [JsonProperty("bestMatches")]
        public IEnumerable<StockSearchResult> Results { get; set; }
    }
}
