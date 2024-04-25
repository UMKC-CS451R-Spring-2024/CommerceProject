using Newtonsoft.Json;

namespace API.DataTransferObjects.TimeSeriesMonthlyAdjusted
{
    public class MonthlyAdjustedData
    {
        [JsonProperty("1. open")]
        public double Open { get; set; }

        [JsonProperty("2. high")]
        public double High { get; set; }

        [JsonProperty("3. low")]
        public double Low { get; set; }

        [JsonProperty("4. close")]
        public double Close { get; set; }

        [JsonProperty("5. adjusted close")]
        public double AdjustedClose { get; set; }

        [JsonProperty("6. volume")]
        public double Volume { get; set; }

        [JsonProperty("7. dividend amount")]
        public double DividendAmount { get; set; }
    }
}
