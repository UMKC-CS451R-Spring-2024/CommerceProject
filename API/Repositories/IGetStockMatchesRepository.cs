using API.DataTransferObjects.SymbolSearch;
using System.Threading.Tasks;

namespace API.Repositories
{
    public interface IGetStockMatchesRepository
    {
        Task<DailyHighsSeriesResults> GetStockMatches(string symbol);
    }
}