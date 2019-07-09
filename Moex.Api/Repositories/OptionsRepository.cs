using System.Threading.Tasks;
using Market.Common.Enums;
using Market.Common.Utils;
using Moex.Api.Contracts.History;
using RestSharp;

namespace Moex.Api.Repositories
{
    public class OptionsRepository : IOptionsRepository
    {
        private readonly string HISTORY_OPTIONS_URL = "https://iss.moex.com/iss/history/engines/futures/markets/options/securities.json";

        private readonly ISecurityRepository _securityRepository;

        public OptionsRepository(
            ISecurityRepository securityRepository)
        {
            _securityRepository = securityRepository;
        }

        public async Task<Securities> GetHistoryAsync(AssetCode asset, int start)
        {
            return await _securityRepository.GetAsync(GetUrl(asset, start));
        }

        private string GetUrl(AssetCode asset, int start)
        {
            return $"{HISTORY_OPTIONS_URL}?assetcode={AssetUtils.GetAssetCodeString(asset)}&start={start}";
        }
    }
}
