using Moex.Api.Contracts;
using System.Threading.Tasks;

namespace Moex.Api.Repositories
{
    public class OptionTradeRepository : IOptionTradeRepository
    {
        private readonly string TRADE_OPTIONS_URL = "https://iss.moex.com/iss/engines/futures/markets/options/securities/{0}/trades.json";

        // https://iss.moex.com/iss/engines/futures/markets/options/securities/Si65000BL9/trades.json

        private readonly ITradeRepository _tradeRepository;

        public OptionTradeRepository(
            ITradeRepository tradeRepository)
        {
            _tradeRepository = tradeRepository;
        }

        public async Task<Trades> GetTrades(string secId)
        {
            return await _tradeRepository.GetAsync(GetUrl(secId));
        }

        private string GetUrl(string secId)
        {
            return string.Format(TRADE_OPTIONS_URL, secId);
        }
    }
}
