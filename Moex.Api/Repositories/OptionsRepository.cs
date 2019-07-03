using System.Threading.Tasks;
using Market.Common.Enums;
using Moex.Api.Contracts.History;
using RestSharp;

namespace Moex.Api.Repositories
{
    public class OptionsRepository : IOptionsRepository
    {
        private readonly string HISTORY_OPTIONS_URL = "https://iss.moex.com/iss/history/engines/futures/markets/options/securities.json";

        private readonly IRestClient _restClient;

        public OptionsRepository(
            IRestClient restClient)
        {
            _restClient = restClient;
        }

        public async Task<OptionSecurities> GetHistoryAsync(AssetCode asset, int start)
        {
            var request = new RestRequest(GetUrl(asset, start), Method.GET, DataFormat.Json);
            var response = await _restClient.ExecuteGetTaskAsync<OptionSecurities>(request);

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

            return $"{HISTORY_OPTIONS_URL}?assetcode={assetString}&start={start}";
        }
    }
}
