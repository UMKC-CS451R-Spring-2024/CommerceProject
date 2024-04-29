﻿using System;
using System.Collections.Generic;

namespace Api.Responses
{
    public class GetDailyHighsResponse
    {
        public GetDailyHighsResponse()
        {
            DailyHighs = new Dictionary<DateOnly, double>();
        }

        public string Symbol { get; set; }
        public IDictionary<DateOnly, double> DailyHighs { get; set; }
    }
}
