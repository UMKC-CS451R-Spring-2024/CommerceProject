using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Api.DataTransferObjects.TimeSeries.Monthly
{
    public class TimeSeriesMonthlyAdjustedDTO
    {
        [JsonProperty("Meta Data")]
        public MetaData MetaData { get; set; }

        [JsonProperty("Monthly Adjusted Time Series")]
        public Dictionary<DateOnly, MonthlyAdjustedData> MonthlyAdjustedTimeSeries { get; set; }

    }
}
