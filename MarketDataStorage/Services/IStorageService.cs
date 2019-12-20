using Moex.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarketDataStorage.Services
{
    public interface IStorageService
    {
        Task SaveAsync(Futures future, Option option, IEnumerable<OptionTrade> trades);
    }
}
