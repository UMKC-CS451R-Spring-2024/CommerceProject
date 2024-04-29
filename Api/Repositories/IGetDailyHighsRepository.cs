using Api.Responses;
using System.Threading.Tasks;

namespace Api.Repositories
{
    public interface IGetDailyHighsRepository
    {
        Task<GetDailyHighsResponse> GetDailyHighs(string symbol);
    }
}