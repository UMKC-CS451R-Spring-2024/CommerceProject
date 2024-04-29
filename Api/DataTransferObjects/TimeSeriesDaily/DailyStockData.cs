using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Api.DataTransferObjects.TimeSeriesDaily
{
    public class DailyStockData
    {
        [JsonProperty("Meta Data")]
        public MetaData MetaData { get; set; }

        [JsonProperty("Time Series (Daily)")]
        public Dictionary<DateOnly, TimeSeriesDaily> TimeSeriesDaily { get; set; }
    }
}