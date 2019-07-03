using Market.Common.Enums;
using Moex.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moex.Api.Services
{
    public interface IFuturesService
    {
        Task<IEnumerable<Futures>> GetAllAsync(AssetCode asset);

        Task<Futures> GetClosest(AssetCode asset);
    }
}
