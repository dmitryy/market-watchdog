using Moex.Api.Contracts;
using System.Threading.Tasks;

namespace Moex.Api.Repositories
{
    public interface ITradeRepository
    {
        Task<Trades> GetAsync(string url);
    }
}
