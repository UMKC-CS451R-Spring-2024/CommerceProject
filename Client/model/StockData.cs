﻿namespace Client
{
    public class StockData
    {
        public string Timestamp { get; set; }

        public string Symbol { get; set; }

        public double Open { get; set; }

        public double High { get; set; }

        public double Low { get; set; }

        public double Close { get; set; }

        public int Volume { get; set; }
    }
}
