using Market.Common.Enums;
using Market.Watchdog.Packages;
using Microsoft.Extensions.DependencyInjection;
using Moex.Api.Models;
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
            var options = await optionsService.GetAllAsync(asset);

            var optionsSeries = options
                .Where(o => o.ExpireDays == futures.ExpireDays);

            DumpFuturesForR(futures);
            DumpCallsForR(optionsSeries);
            DumpPutsForR(optionsSeries);
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
    }
}
