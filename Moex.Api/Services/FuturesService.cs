using Market.Common.Enums;
using Moex.Api.Mappers;
using Moex.Api.Models;
using Moex.Api.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moex.Api.Services
{
    public class FuturesService : IFuturesService
    {
        private readonly string CacheKey = "futures_history";

        private readonly ICacheService _cacheService;
        private readonly IFuturesRepository _futuresRepository;

        public FuturesService(
            ICacheService cacheService,
            IFuturesRepository futuresRepository)
        {
            _cacheService = cacheService;
            _futuresRepository = futuresRepository;
        }

        public async Task<IEnumerable<Futures>> GetAllAsync(AssetCode asset)
        {
            
            var cache = _cacheService.Get<IEnumerable<Futures>>(CacheKey);

            if (cache != null)
            {
                return cache;
            }

            var futures = new List<Futures>();

            var start = 0;
            var total = 100;

            while (start < total)
            {
                var history = await _futuresRepository.GetHistoryAsync(asset, start);
                var historyFutures = history
                    .ExtractFutures();

                futures.AddRange(historyFutures);

                start += history.Cursor.Size;
                total = history.Cursor.Total;
            }

            if (futures.Count > 0)
            {
                _cacheService.Set<IEnumerable<Futures>>(CacheKey, futures);
            }

            return futures;
        }
    }
}
