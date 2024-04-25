using API.Responses;
using System.Threading.Tasks;

namespace API.Repositories
{
    public interface IGetDailyHighsRepository
    {
        Task<GetDailyHighsResponse> GetDailyHighs(string symbol);
    }
}