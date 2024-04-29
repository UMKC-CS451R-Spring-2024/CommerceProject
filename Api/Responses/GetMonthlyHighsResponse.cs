using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Responses
{
    public class GetMonthlyHighsResponse
    {
        public GetMonthlyHighsResponse()
        {
            MonthlyHighs = new Dictionary<DateOnly, double>();
        }

        public string Symbol { get; set; }
        public IDictionary<DateOnly, double> MonthlyHighs { get; set; }
    }
}
