using Commerce.WebAPI.Responses;

namespace Commerce.WebAPI.Repositories
{
    public interface IGetAnnualReturnsRepository
    {
        Task<GetAnnualReturnsResponse> GetAnnualReturns(string symbol);
    }
}