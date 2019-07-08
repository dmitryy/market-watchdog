using Market.Common.Enums;
using Moex.Api.Contracts.History;
using System.Threading.Tasks;

namespace Moex.Api.Repositories
{
    public interface IFuturesRepository
    {
        Task<FuturesSecurities> GetHistoryAsync(AssetCode asset, int start);

        Task<FuturesSecurities> GetCandlesHistoryAsync(string futuresSecId, int start);
    }
}
