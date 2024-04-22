using API.Responses;
using System.Threading.Tasks;

namespace API.Repositories
{
    public interface IGetAnnualReturnsRepository
    {
        Task<GetAnnualReturnsResponse> GetAnnualReturns(string symbol);
    }
}