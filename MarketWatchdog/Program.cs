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
        static async Task Main(string[] args)
        {
            var services = ServicesPackage.RegisterServices();
            var options = services.GetService<IOptionsService>();

            await GetCallsAndDump(options);
        }

        private static async Task GetCallsAndDump(IOptionsService optionService)
        {
            var options = await optionService.GetAllAsync(AssetCode.Si);

            var calls = options
                .Where(o => o.Type == OptionType.Call && o.Close != 0 && o.ExpireDays == 80)
                .OrderBy(o => o.ExpireDays);

            await DumpForR(calls);
        }

        private static async Task DumpOptions(IOptionsService optionService)
        {
            var options = await optionService.GetAllAsync(AssetCode.Si);

            var calls = options
                .Where(o => o.Type == OptionType.Call && o.Close != 0)
                .OrderBy(o => o.ExpireDays);

            foreach (var option in calls)
            {
                Console.WriteLine($"{option.Type} {option.SecId}: {option.Strike} {option.Close} - Expires in {option.ExpireDays} days, Base asset {option.Futures.SecId}, expires in {option.Futures.ExpireDays}");
            }

            Console.WriteLine();

            var puts = options
                .Where(o => o.Type == OptionType.Put && o.Close != 0)
                .OrderBy(o => o.ExpireDays);

            foreach (var option in puts)
            {
                Console.WriteLine($"{option.Type} {option.SecId}: {option.Strike} {option.Close} - Expires in {option.ExpireDays} days, Base asset {option.Futures.SecId}, expires in {option.Futures.ExpireDays}");
            }

            Console.WriteLine();

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
        }

        private static async Task DumpForR(IEnumerable<Option> options)
        {
            var asset = options.First().Futures;
            Console.WriteLine($"asset: {asset.SecId}, expires in {asset.ExpireDays} days, close price: {asset.Close}");
            Console.WriteLine($"strikes = c({String.Join(',', options.Select(o => o.Strike.ToString()))})");
            Console.WriteLine($"prices = c({String.Join(',', options.Select(o => o.Close.ToString()))})");
        }
    }
}
