
using API.Responses;
using Client.Repositories.Stock.Responses;
using Newtonsoft.Json;

namespace Client.Repositories.Stock
{
    public class StaticValuesStockRepository : StockRepository, IStockRepository
    {
        public StaticValuesStockRepository(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }

        public override async Task<GetAnnualReturnsResponse> GetAnnualReturns(string symbol)
        {
            var response = new GetAnnualReturnsResponse();
            response.Symbol = "IBM";
            response.AnnualReturns.Add(1999, 0);
            response.AnnualReturns.Add(2000, -20.839185237004);
            response.AnnualReturns.Add(2001, 43.0008634801849);
            response.AnnualReturns.Add(2002, -35.470530331382);
            response.AnnualReturns.Add(2003, 20.4958939493627);
            response.AnnualReturns.Add(2004, 7.19140752185634);
            response.AnnualReturns.Add(2005, -15.831476551725);
            response.AnnualReturns.Add(2006, 19.7690786907734);
            response.AnnualReturns.Add(2007, 12.8402215147823);
            response.AnnualReturns.Add(2008, -20.764468121453);
            response.AnnualReturns.Add(2009, 58.6077189398284);
            response.AnnualReturns.Add(2010, 14.2831879195959);
            response.AnnualReturns.Add(2011, 27.4205208152895);
            response.AnnualReturns.Add(2012, 5.93475790240694);
            response.AnnualReturns.Add(2013, -0.1835086860778);
            response.AnnualReturns.Add(2014, -12.394567070385);
            response.AnnualReturns.Add(2015, -11.411813623578);
            response.AnnualReturns.Add(2016, 25.1898988604964);
            response.AnnualReturns.Add(2017, -3.9833376265468);
            response.AnnualReturns.Add(2018, -22.538399769206);
            response.AnnualReturns.Add(2019, 23.5715319307417);
            response.AnnualReturns.Add(2020, -1.1906830134022);
            response.AnnualReturns.Add(2021, 16.7003421297528);
            response.AnnualReturns.Add(2022, 10.7038480779587);
            response.AnnualReturns.Add(2023, 21.8776880831072);
            response.AnnualReturns.Add(2024, 4.20721757289910);

            return response;
        }

        public override async Task<GetMonthlyHighsResponse> GetMonthlyHighs(string symbol)
        {
            var response = new GetMonthlyHighsResponse();
            response.Symbol = "IBM";
            response.MonthlyHighs .Add(new DateOnly(2023, 01, 20), 0);
            response.MonthlyHighs .Add(new DateOnly(2023, 02, 20), -20.839185237004);
            response.MonthlyHighs .Add(new DateOnly(2023, 03, 20), 43.0008634801849);
            response.MonthlyHighs .Add(new DateOnly(2023, 04, 20), -35.470530331382);
            response.MonthlyHighs .Add(new DateOnly(2023, 05, 20), 20.4958939493627);
            response.MonthlyHighs .Add(new DateOnly(2023, 06, 20), 7.19140752185634);
            response.MonthlyHighs .Add(new DateOnly(2023, 07, 20), -15.831476551725);
            response.MonthlyHighs .Add(new DateOnly(2023, 08, 20), 19.7690786907734);
            response.MonthlyHighs .Add(new DateOnly(2023, 09, 20), 12.8402215147823);
            response.MonthlyHighs .Add(new DateOnly(2023, 10, 20), -20.764468121453);
            response.MonthlyHighs .Add(new DateOnly(2023, 11, 20), 58.6077189398284);
            response.MonthlyHighs .Add(new DateOnly(2023, 12, 20), 14.2831879195959);
          

            return response;
        }

        public override async Task<GetStockMatchesResponse> GetStockMatches(string symbol)
        {
            return new GetStockMatchesResponse
            {
                StockMatches = new List<StockMatch>()
                {
                    new StockMatch
                    {
                        Symbol = "IBM",
                        Name = "sadkfja",
                        Type = "asjdkf",
                        Region = "United States",
                        MarketOpen = "09:30",
                        MarketClose = "16:00",
                        Timezone = "UTC-04",
                        Currency = "USD",
                        MatchScore = "1.0"
                    }
                }
            };
        }
    }
}
