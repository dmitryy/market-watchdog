using Market.Common.Enums;
using Moex.Api.Contracts.History;
using System;
using System.Threading.Tasks;

namespace Moex.Api.Repositories
{
    public interface IOptionsRepository
    {
        Task<Securities> GetCandlesHistoryAsync(string secId, int start, DateTime from);

        Task<Securities> GetHistoryAsync(AssetCode asset, int start);
    }
}
