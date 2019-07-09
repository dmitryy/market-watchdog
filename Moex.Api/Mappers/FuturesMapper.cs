using Moex.Api.Contracts.History;
using Moex.Api.Models;
using Moex.Api.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moex.Api.Mappers
{
    public static class FuturesMapper
    {
        public static IEnumerable<Futures> ExtractFutures(this Securities futuresSecurities)
        {
            if (futuresSecurities == null)
            {
                throw new ArgumentNullException(nameof(futuresSecurities));
            }

            var futures = new List<Futures>();

            if (futuresSecurities.History != null
                && futuresSecurities.History.Data != null
                && futuresSecurities.History.Data.Count > 0)
            {
                futuresSecurities.History.Data.ForEach(future =>
                {
                    var (assetCode, expire) = FuturesSecurityParser.Parse(future[2]);

                    futures.Add(new Futures()
                    {
                        Asset = assetCode,

                        Expire = expire,

                        // BOARDID
                        BoardId = future[0],

                        // TRADEDATE
                        TradeDate = Convert.ToDateTime(future[1]),

                        // SECID
                        SecId = future[2],

                        // OPEN
                        Open = Convert.ToDecimal(future[3]),

                        // LOW
                        Low = Convert.ToDecimal(future[4]),

                        // HIGH
                        High = Convert.ToDecimal(future[5]),

                        // CLOSE
                        Close = Convert.ToDecimal(future[6]),

                        // OPENPOSITIONVALUE
                        OpenPositionValue = Convert.ToDecimal(future[7]),

                        // VALUE
                        Value = Convert.ToDecimal(future[8]),

                        // VOLUME
                        Volume = Convert.ToDecimal(future[9]),

                        // OPENPOSITION
                        OpenPosition = Convert.ToDecimal(future[10])
                    });
                });
            }

            return futures;
        }
    }
}
