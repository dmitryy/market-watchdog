using System;
using System.Threading.Tasks;
using Moex.Api.Contracts;
using RestSharp;

namespace Moex.Api.Repositories
{
    public class TradeRepository : ITradeRepository
    {
        private readonly IRestClient _restClient;

        public TradeRepository(
            IRestClient restClient)
        {
            _restClient = restClient;
        }

        public async Task<Trades> GetAsync(string url)
        {
            var tries = 3;

            var request = new RestRequest(url, Method.GET, DataFormat.Json);
            var response = await _restClient.ExecuteGetTaskAsync<Trades>(request);

            while (tries > 0)
            {
                if (response.ErrorException != null)
                {
                    System.Threading.Thread.Sleep(1000); // wait a little and try again

                    tries--;

                    if (tries == 0)
                    {
                        throw response.ErrorException;
                    }

                    response = await _restClient.ExecuteGetTaskAsync<Trades>(request);
                }
                else
                {
                    return response.Data;
                }
            }

            return response.Data;
        }
    }
}
