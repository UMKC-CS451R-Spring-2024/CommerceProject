using API.DataTransferObjects;
using System.Threading.Tasks;

namespace API.Repositories
{
    public interface IGetStockMatchesRepository
    {
        Task<StockSearchResults> GetStockMatches(string symbol);
    }
}