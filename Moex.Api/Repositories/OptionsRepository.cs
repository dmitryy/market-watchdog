using System;
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
        private readonly string HISTORY_OPTIONS_CANDLES_URL = "https://iss.moex.com/iss/history/engines/futures/markets/options/securities/{0}/candles.json";

        // TODO:
        // BR prices
        //https://iss.moex.com/iss/history/engines/futures/markets/forts/securities/BRU9/candles.json
        // Options prices
        //https://iss.moex.com/iss/history/engines/futures/markets/options/securities/BR68BU9/candles.json?from=2019-09-01&start=0

        private readonly ISecurityRepository _securityRepository;

        public OptionsRepository(
            ISecurityRepository securityRepository)
        {
            _securityRepository = securityRepository;
        }

        public async Task<Securities> GetCandlesHistoryAsync(string secId, int start, DateTime from)
        {
            return await _securityRepository.GetAsync(GetCandlesUrl(secId, start, from));
        }

        public async Task<Securities> GetHistoryAsync(AssetCode asset, int start)
        {
            return await _securityRepository.GetAsync(GetUrl(asset, start));
        }

        private string GetCandlesUrl(string secId, int start, DateTime from)
        {
            return $"{string.Format(HISTORY_OPTIONS_CANDLES_URL, secId)}?from={from.ToString("yyyy-MM-dd")}&start={start}";
        }

        private string GetUrl(AssetCode asset, int start)
        {
            return $"{HISTORY_OPTIONS_URL}?assetcode={AssetUtils.GetAssetCodeString(asset)}&start={start}";
        }
    }
}
