using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Moex.Api.Mappers;
using Moex.Api.Models;
using Moex.Api.Repositories;

namespace Moex.Api.Services
{
    public class OptionTradeService : IOptionTradeService
    {
        private readonly IOptionTradeRepository _optionTradeRepository;

        public OptionTradeService(
            IOptionTradeRepository optionTradeRepository)
        {
            _optionTradeRepository = optionTradeRepository;
        }

        public async Task<IEnumerable<OptionTrade>> GetTrades(string secId)
        {
            var trades = await _optionTradeRepository.GetTrades(secId);

            return trades.ExtractOptionTrades();
        }
    }
}
