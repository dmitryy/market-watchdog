using Market.Common.Enums;
using Moex.Api.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moex.Api.Services
{
    public interface IFuturesService
    {
        Task<IEnumerable<Futures>> GetAllAsync(AssetCode asset);

        Task<IEnumerable<Futures>> GetCandlesAsync(string futuresSecId, DateTime from);

        Task<Futures> GetClosest(AssetCode asset);

        Task<Futures> GetBySecId(AssetCode asset, string secId);
    }
}
