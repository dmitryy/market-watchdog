using Moex.Api.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Moex.Api.Services
{
    public interface IOptionTradeService
    {
        /// <summary>
        /// Returns today trades for specify option by secId
        /// </summary>
        Task<IEnumerable<OptionTrade>> GetTrades(string secId);
    }
}
