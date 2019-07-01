using Moex.Api.Contracts.History;
using Moex.Api.Models;
using Moex.Api.Utils;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Moex.Api.Mappers
{
    public static class OptionMapper
    {
        public static IEnumerable<Option> ExtractOptions(this OptionSecurities optionSecurities, IEnumerable<Futures> futuresList)
        {
            if (optionSecurities == null)
            {
                throw new ArgumentNullException(nameof(optionSecurities));
            }

            var options = new List<Option>();

            if (optionSecurities.History != null
                && optionSecurities.History.Data != null
                && optionSecurities.History.Data.Count > 0)
            {
                optionSecurities.History.Data.ForEach(opt =>
                {
                    var (assetCode, optionType, strike, expire) = OptionSecurityParser.Parse(opt[2]);
                    var futures = GetFuturesByExpirationDate(futuresList, expire);

                    options.Add(new Option()
                    {
                        Asset = assetCode,

                        Type = optionType,

                        Strike = strike,

                        Expire = expire,

                        Futures = futures,

                        // BOARDID - ROPD
                        BoardId = opt[0],

                        // TRADEDATE - 2019-06-24
                        TradeDate = Convert.ToDateTime(opt[1]),

                        // SECID - Si65000BT9
                        SecId = opt[2],

                        // OPEN - 1215.00000
                        Open = Convert.ToDecimal(opt[3]),

                        // LOW - 1215.00000
                        Low = Convert.ToDecimal(opt[4]),

                        // HIGH - 1600.00000
                        High = Convert.ToDecimal(opt[5]),

                        // CLOSE - 1600.00000
                        Close = Convert.ToDecimal(opt[6]),

                        // OPENPOSITIONVALUE - 350870000.00
                        OpenPositionValue = Convert.ToDecimal(opt[7]),

                        // VALUE - 5915000.00
                        Value = Convert.ToDecimal(opt[8]),

                        // VOLUME - 91
                        Volume = Convert.ToDecimal(opt[9]),

                        // OPENPOSITION - 5398
                        OpenPosition = Convert.ToDecimal(opt[10])
                    });
                });
            }

            return options;
        }

        private static Futures GetFuturesByExpirationDate(IEnumerable<Futures> futuresList, DateTime expire)
        {
            var optionFutures = futuresList
                .SingleOrDefault(f => f.Expire == expire);

            if (optionFutures == null)
            {
                optionFutures = futuresList
                    .OrderBy(f => f.ExpireDays)
                    .First(f => f.Expire > expire);
            }

            return optionFutures;
        }
    }
}
