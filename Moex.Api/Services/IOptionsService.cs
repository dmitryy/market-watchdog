using Market.Common.Enums;
using Moex.Api.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moex.Api.Services
{
    public interface IOptionsService
    {
        /// <summary>
        /// Returs candles for specific option security from specific date
        /// </summary>
        /// <param name="secId"></param>
        /// <param name="from"></param>
        /// <returns></returns>
        Task<IEnumerable<Option>> GetCandlesAsync(string secId, DateTime from);

        /// <summary>
        /// Returns latest historical data for specified asset and option type
        /// </summary>
        /// <param name="asset"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        Task<IEnumerable<Option>> GetAllAsync(AssetCode asset);
    }
}
