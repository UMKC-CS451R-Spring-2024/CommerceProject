using Api.Responses;
using System.Threading.Tasks;

namespace Api.Repositories
{
    public interface IGetAnnualReturnsRepository
    {
        Task<GetAnnualReturnsResponse> GetAnnualReturns(string symbol);
    }
}