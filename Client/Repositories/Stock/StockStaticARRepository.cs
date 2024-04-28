
using Client.Repositories.Stock.Responses;
using Newtonsoft.Json;

namespace Client.Repositories.Stock
{
    public class StockStaticARRepository : StockRepository, IStockRepository
    {
        public StockStaticARRepository(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
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

        private const string responseData =
            @"""annualReturns"": {
    ""1999"": 0,
    ""2000"": -20.839185237004322,
    ""2001"": 43.000863480184925,
    ""2002"": -35.47053033138242,
    ""2003"": 20.495893949362774,
    ""2004"": 7.191407521856341,
    ""2005"": -15.831476551725443,
    ""2006"": 19.769078690773444,
    ""2007"": 12.840221514782325,
    ""2008"": -20.76446812145377,
    ""2009"": 58.60771893982844,
    ""2010"": 14.283187919595953,
    ""2011"": 27.420520815289564,
    ""2012"": 5.93475790240694,
    ""2013"": -0.18350868607780457,
    ""2014"": -12.394567070385175,
    ""2015"": -11.411813623578338,
    ""2016"": 25.189898860496406,
    ""2017"": -3.983337626546879,
    ""2018"": -22.53839976920633,
    ""2019"": 23.571531930741795,
    ""2020"": -1.1906830134022606,
    ""2021"": 16.700342129752865,
    ""2022"": 10.703848077958789,
    ""2023"": 21.877688083107238,
    ""2024"": 4.207217572899102
  }";
    }
}
