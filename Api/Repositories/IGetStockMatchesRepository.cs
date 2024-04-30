using Api.DataTransferObjects.SymbolSearch;
using System.Threading.Tasks;

namespace Api.Repositories
{
    public interface IGetStockMatchesRepository
    {
        Task<StockSearchResults> GetStockMatches(string symbol);
    }
}