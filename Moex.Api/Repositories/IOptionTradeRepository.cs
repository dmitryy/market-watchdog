using Moex.Api.Contracts;
using System.Threading.Tasks;

namespace Moex.Api.Repositories
{
    public interface IOptionTradeRepository
    {
        Task<Trades> GetTrades(string secId);
    }
}
