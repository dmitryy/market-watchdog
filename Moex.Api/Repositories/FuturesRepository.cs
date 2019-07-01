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
            return $"{HISTORY_FUTURES_URL}?assetcode={asset}&start={start}";
        }
    }
}
