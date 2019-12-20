using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Market.Common.Enums;
using Moex.Api.Mappers;
using Moex.Api.Models;
using Moex.Api.Repositories;

namespace Moex.Api.Services
{
    public class OptionsService : IOptionsService
    {
        private readonly IOptionsRepository _optionsRepository;
        private readonly IFuturesService _futuresService;

        public OptionsService(
            IOptionsRepository optionsRepository,
            IFuturesService futuresService)
        {
            _optionsRepository = optionsRepository;
            _futuresService = futuresService;
        }

        public async Task<IEnumerable<Option>> GetAllAsync(AssetCode asset)
        {
            var futures = await _futuresService.GetAllAsync(asset);

            var start = 0;
            var total = 100;
            var options = new List<Option>();

            while (start < total)
            {
                var history = await _optionsRepository.GetHistoryAsync(asset, start);
                var historyOptions = history
                    .ExtractOptions(futures);

                options.AddRange(historyOptions);

                start += history.Cursor.Size;
                total = history.Cursor.Total;
            }

            return options;
        }

        public async Task<IEnumerable<Option>> GetCandlesAsync(string secId, DateTime from)
        {
            var options = new List<Option>();

            var start = 0;
            var total = 100;

            while (start < total)
            {
                var candles = await _optionsRepository.GetCandlesHistoryAsync(secId, start, from);
                var candlesOptions = candles
                    .ExtractOptions();

                options.AddRange(candlesOptions);

                start += candles.Cursor.Size;
                total = candles.Cursor.Total;
            }

            return options;

        }
    }
}
