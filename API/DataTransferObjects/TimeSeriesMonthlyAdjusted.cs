using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace API.DataTransferObjects
{
    public class TimeSeriesMonthlyAdjusted
    {
        [JsonProperty("Meta Data")]
        public MetaData MetaData { get; set; }

        [JsonProperty("Monthly Adjusted Time Series")]
        public Dictionary<DateOnly, MonthlyAdjustedData> MonthlyAdjustedTimeSeries { get; set; }

    }
}
