using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.DataTransferObjects.TimeSeries.Monthly
{
    public class MonthlyData
    {
        [JsonProperty("1. open")]
        public double Open { get; set; }

        [JsonProperty("2. high")]
        public double High { get; set; }

        [JsonProperty("3. low")]
        public double Low { get; set; }

        [JsonProperty("4. close")]
        public double Close { get; set; }

        [JsonProperty("5. volume")]
        public double Volume { get; set; }

    }
}

