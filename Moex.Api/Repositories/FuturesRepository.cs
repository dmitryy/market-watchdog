using Market.Common.Enums;
using Market.Common.Utils;
using Moex.Api.Contracts.History;
using System;
using System.Threading.Tasks;

namespace Moex.Api.Repositories
{
    public class FuturesRepository : IFuturesRepository
    {
        // TODO: move constants and url workers to separate place

        private readonly string HISTORY_FUTURES_URL = "https://iss.moex.com/iss/history/engines/futures/markets/forts/securities.json";
        private readonly string HISTORY_FUTURES_CANDLES_URL = "https://iss.moex.com/iss/history/engines/futures/markets/forts/securities/{0}/candles.json";

        private readonly ISecurityRepository _securityRepository;

        public FuturesRepository(
            ISecurityRepository securityRepository)
        {
            _securityRepository = securityRepository;
        }

        public async Task<Securities> GetCandlesHistoryAsync(string futuresSecId, int start, DateTime from)
        {
            return await _securityRepository.GetAsync(GetCandlesUrl(futuresSecId, start, from));
        }

        public async Task<Securities> GetHistoryAsync(AssetCode asset, int start)
        {
            return await _securityRepository.GetAsync(GetHistoryUrl(asset, start));
        }

        private string GetCandlesUrl(string futuresSecId, int start, DateTime from)
        {
            return $"{string.Format(HISTORY_FUTURES_CANDLES_URL, futuresSecId)}?from={from.ToString("yyyy-MM-dd")}&start={start}";
        }

        private string GetHistoryUrl(AssetCode asset, int start)
        {
            return $"{HISTORY_FUTURES_URL}?assetcode={AssetUtils.GetAssetCodeString(asset)}&start={start}";
        }
    }
}
