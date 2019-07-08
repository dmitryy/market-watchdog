using Market.Common.Enums;
using Moex.Api.Contracts.History;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Moex.Api.Repositories
{
    public class FuturesRepository : IFuturesRepository
    {
        private readonly string HISTORY_FUTURES_URL = "https://iss.moex.com/iss/history/engines/futures/markets/forts/securities.json";

        private readonly IRestClient _restClient;

        // futures candles
        // https://iss.moex.com/iss/history/engines/futures/markets/forts/securities/SIU9/candles.json?from=2019-01-01&start=10

        // bonds
        // https://iss.moex.com/iss/history/engines/stock/markets/bonds/securities.json?start=1000

        public FuturesRepository(
            IRestClient restClient)
        {
            _restClient = restClient;
        }

        public async Task<FuturesSecurities> GetHistoryAsync(AssetCode asset, int start)
        {
            var request = new RestRequest(GetUrl(asset, start), Method.GET, DataFormat.Json);
            var response = await _restClient.ExecuteGetTaskAsync<FuturesSecurities>(request);

            if (response.ErrorException != null)
            {
                throw response.ErrorException;
            }

            return response.Data;
        }

        private string GetUrl(AssetCode asset, int start)
        {
            var assetString = asset.ToString();

            if (asset == AssetCode.Ri)
            {
                assetString = "RTS";
            }

            return $"{HISTORY_FUTURES_URL}?assetcode={assetString}&start={start}";
        }
    }
}
