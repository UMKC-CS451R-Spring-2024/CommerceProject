using Api.Responses;
using System.Threading.Tasks;

namespace Api.Repositories
{
    public interface IGetMonthlyHighsRepository
    {
        Task<GetMonthlyHighsResponse> GetMonthlyHighs(string symbol);
    }
}