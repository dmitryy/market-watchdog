using Market.Common.Enums;
using Moex.Api.Contracts.History;
using System;
using System.Threading.Tasks;

namespace Moex.Api.Repositories
{
    public interface IFuturesRepository
    {
        Task<Securities> GetHistoryAsync(AssetCode asset, int start);

        Task<Securities> GetCandlesHistoryAsync(string futuresSecId, int start, DateTime from);
    }
}
