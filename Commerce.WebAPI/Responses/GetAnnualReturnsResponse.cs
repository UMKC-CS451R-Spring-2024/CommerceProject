using Newtonsoft.Json;

namespace Commerce.WebAPI.Responses
{
    public class GetAnnualReturnsResponse
    {
        public GetAnnualReturnsResponse()
        {
            AnnualReturns = new Dictionary<int, double>();
        }

        public string Symbol {  get; set; }
        public Dictionary<int, double> AnnualReturns { get; set; }
    }
}
