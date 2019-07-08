using Market.Common.Enums;
using Market.Watchdog.Packages;
using Microsoft.Extensions.DependencyInjection;
using Moex.Api.Models;
using Moex.Api.Repositories;
using Moex.Api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Market.Watchdog
{
    class Program
    {
        // TODO:
        // - create classes to get asset prices
        // - modify option logic to include base asset
        // - match current asset price with option series

        // features - all available features
        // features SiH9 - info
        // options - all available options
        // options SiH9 - options for specified features
        // options SiH9 22 - options for specified expire date
        // options list SiH9 - get list of options for specified features

        static async Task Main(string[] args)
        {
            var services = ServicesPackage.RegisterServices();
            var futuresService = services.GetService<IFuturesService>();
            var optionsService = services.GetService<IOptionsService>();

            var asset = AssetCode.Ri;

            var futures = await futuresService.GetClosest(asset);
            var candles = await futuresService.GetCandlesAsync(futures.SecId);

            DumpCandlesForR(candles
                .OrderBy(c => c.TradeDate)
                .Where(c => c.Open > 0 && c.High > 0 && c.Low > 0 && c.Close > 0));

            //var options = await optionsService.GetAllAsync(asset);
            //var optionsSeries = options
            //    .Where(o => o.ExpireDays == futures.ExpireDays);
            //DumpFuturesForR(futures);
            //DumpCallsForR(optionsSeries);
            //DumpPutsForR(optionsSeries);
        }

        private static void DumpFuturesForR(Futures futures)
        {
            Console.WriteLine($"# asset: {futures.SecId}, expires in {futures.ExpireDays} days, price: {futures.Close}, Trade date: {futures.TradeDate}");
            Console.WriteLine($"asset = {futures.Close.ToString().Replace(',', '.')}");
            Console.WriteLine($"expiry = 1 / 252 * {futures.ExpireDays}");
        }

        private static void DumpCallsForR(IEnumerable<Option> options)
        {
            var calls = options
                .Where(o => o.Type == OptionType.Call && o.Close != 0)
                .OrderBy(o => o.Strike);

            Console.WriteLine($"callStrikes = c({String.Join(',', calls.Select(o => o.Strike.ToString()))})");
            Console.WriteLine($"callPrices = c({String.Join(',', calls.Select(o => o.Close.ToString().Replace(',', '.')))})");
        }

        private static void DumpPutsForR(IEnumerable<Option> options)
        {
            var puts = options
                .Where(o => o.Type == OptionType.Put && o.Close != 0)
                .OrderBy(o => o.Strike);

            Console.WriteLine($"putStrikes = c({String.Join(',', puts.Select(o => o.Strike.ToString()))})");
            Console.WriteLine($"putPrices = c({String.Join(',', puts.Select(o => o.Close.ToString().Replace(',', '.')))})");
        }

        private static void DumpCandlesForR(IEnumerable<Futures> candles)
        {
            //Open  = c(1, 2, 3, 1, 2, 3, 1, 2, 3, 4)
            //High  = c(2, 3, 4, 2, 3, 4, 2, 3, 4, 5)
            //Low   = c(1, 2, 3, 2, 3, 4, 5, 2, 1, 4)
            //Close = c(3, 2, 1, 3, 4, 2, 1, 4, 2, 3)
            //ohlc <- data.frame(Open, High, Low, Close)

            Console.WriteLine($"Open = c({String.Join(',', candles.Select(c => c.Open.ToString().Replace(',', '.')))})");
            Console.WriteLine($"High = c({String.Join(',', candles.Select(c => c.High.ToString().Replace(',', '.')))})");
            Console.WriteLine($"Low = c({String.Join(',', candles.Select(c => c.Low.ToString().Replace(',', '.')))})");
            Console.WriteLine($"Close = c({String.Join(',', candles.Select(c => c.Close.ToString().Replace(',', '.')))})");
            Console.WriteLine("ohlc <- data.frame(Open, High, Low, Close)");
        }
    }
}
