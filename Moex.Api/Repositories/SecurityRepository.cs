using System.Threading.Tasks;
using Moex.Api.Contracts.History;
using RestSharp;

namespace Moex.Api.Repositories
{
    public class SecurityRepository : ISecurityRepository
    {
        private readonly IRestClient _restClient;

        public SecurityRepository(
            IRestClient restClient)
        {
            _restClient = restClient;
        }

        public async Task<Securities> GetAsync(string url)
        {
            var request = new RestRequest(url, Method.GET, DataFormat.Json);
            var response = await _restClient.ExecuteGetTaskAsync<Securities>(request);

            if (response.ErrorException != null)
            {
                throw response.ErrorException;
            }

            return response.Data;
        }
    }
}
