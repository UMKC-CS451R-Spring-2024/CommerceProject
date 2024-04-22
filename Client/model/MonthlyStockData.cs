
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class MonthlyStockData
    {
        [JsonProperty("Meta Data")]
        public MonthlyMetaData MetaData { get; set; }

        [JsonProperty("Monthly Time Series")]
        public Dictionary<string, MonthlyTimeSeries> MonthlyTimeSeries { get; set; }
    }

    public partial class MonthlyMetaData
    {
        [JsonProperty("1. Information")]
        public string Information { get; set; }

        [JsonProperty("2. Symbol")]
        public string Symbol { get; set; }

        [JsonProperty("3. Last Refreshed")]
        public DateTimeOffset LastRefreshed { get; set; }

        [JsonProperty("4. Time Zone")]
        public string TimeZone { get; set; }
    }

    public partial class MonthlyTimeSeries
    {
        [JsonProperty("1. open")]
        public string Open { get; set; }

        [JsonProperty("2. high")]
        public string High { get; set; }

        [JsonProperty("3. low")]
        public string Low { get; set; }

        [JsonProperty("4. close")]
        public string Close { get; set; }

        [JsonProperty("5. volume")]
        public long Volume { get; set; }
    }

