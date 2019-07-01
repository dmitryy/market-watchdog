using Market.Common.Enums;
using Moex.Api.Contracts.History;
using System.Threading.Tasks;

namespace Moex.Api.Repositories
{
    public interface IOptionsRepository
    {
        Task<OptionSecurities> GetHistoryAsync(AssetCode asset, int start);
    }
}
