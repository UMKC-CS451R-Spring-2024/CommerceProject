using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.DataTransferObjects.TimeSeries.Monthly
{
    public class TimeSeriesMonthlyDTO
    {
        public MetaData MetaData { get; set; }

        [JsonProperty("Monthly Time Series")]
        public Dictionary<DateOnly, MonthlyData> MonthlyTimeSeries { get; set; }
    }
}
