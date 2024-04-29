using Api.DataTransferObjects.SymbolSearch;
using System.Threading.Tasks;

namespace Api.Repositories
{
    public interface IGetStockMatchesRepository
    {
        Task<DailyHighsSeriesResults> GetStockMatches(string symbol);
    }
}