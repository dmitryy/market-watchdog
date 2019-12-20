using Market.Common.Enums;
using Market.Watchdog.Packages;
using Microsoft.Extensions.DependencyInjection;
using Moex.Api.Models;
using Moex.Api.Services;
using System;
using System.Collections.Generic;
using System.IO;
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

        // futures candles
        // https://iss.moex.com/iss/history/engines/futures/markets/forts/securities/SIU9/candles.json?from=2019-01-01&start=10

        // bonds
        // https://iss.moex.com/iss/history/engines/stock/markets/bonds/securities.json?start=1000

        static async Task Main(string[] args)
        {
            var services = ServicesPackage.RegisterServices();
            var futuresService = services.GetService<IFuturesService>();
            var optionsService = services.GetService<IOptionsService>();
            var optionsTradeService = services.GetService<IOptionTradeService>();

            var allfutures = await futuresService.GetAllAsync(AssetCode.Si);
            var allOptions = await optionsService.GetAllAsync(AssetCode.Si);

            var basePath = $"C:\\MarketData\\{DateTime.Now.ToString("yyyy-MM-dd")}";

            foreach (var future in allfutures.Reverse())
            {
                Console.WriteLine(future.SecId);

                var futureOptions = allOptions.Where(o => o.Futures.SecId.Equals(future.SecId));

                foreach (var option in futureOptions)
                {
                    var path = $"{basePath}\\{future.SecId}\\{option.SecId}.csv";
                    var trades = await optionsTradeService.GetTrades(option.SecId);
                    if (trades.Any())
                    {
                        var strings = trades.Select(t => $"{t.TradeNo}, {t.BoardName}, {t.SecId}, {t.TradeDate}, {t.TradeTime}, {t.Price}, {t.Quantity}, {t.SysTime}");
                        Directory.CreateDirectory(Path.GetDirectoryName(path));
                        File.WriteAllLines(path, strings);
                        Console.WriteLine($"{option.SecId} {strings.Count()}");
                    }
                    else
                    {
                        Console.Write(".");
                    }

                    //System.Threading.Thread.Sleep(1000);
                }

                //Console.WriteLine(string.Join(",", allOptions
                //    .Where(o => o.Futures.SecId.Equals(future.SecId))
                //    .Select(o => o.SecId)
                //    .ToArray()));
            }

            //var trades = await optionsTradeService.GetTrades("Si65000BL9");
            //trades.ToList().ForEach(f => 
            //{
            //    Console.WriteLine($"{f.SecId} {f.Quantity} {f.Price}");
            //});

            return;


            // 2019-11-01
            //  SiZ9
            //   Si52000BK9
            //   Si52000BL9
            //   Si52000BW9
            //   Si52000BX9
            //   ...
            //   Si84000BL9
            //   Si84000BX9
            //   Si84250BL9
            //   Si84250BX9
            //
            //allOptions.ToList().ForEach(o =>
            //{
            //    Console.WriteLine($"{o.Futures.SecId} {o.SecId}");
            //});

            return;

            var asset = AssetCode.Br;
            var from = DateTime.Parse("2019-09-01"); // DateTime.Parse("2019-01-01");

            //var futures = await futuresService.GetClosest(asset);
            var futures = await futuresService.GetBySecId(asset, "BRV9");
            var candles = await futuresService.GetCandlesAsync(futures.SecId, from);

            var options = await optionsService.GetAllAsync(asset);
            var optionsSeries = options
                .Where(o => o.ExpireDays == futures.ExpireDays && o.Strike == 68);

            DumpFuturesForR(futures);

            foreach (var os in optionsSeries)
            {
                var optionCandles = await optionsService.GetCandlesAsync(os.SecId, from);
                    //.Where(c => c.Close > 0);

                Console.WriteLine();
                Console.WriteLine($"# {os.SecId} {os.Type} -  {optionCandles.Count()}");
                Console.WriteLine($"# Start: {optionCandles.First().TradeDate.ToShortDateString()} End: {optionCandles.Last().TradeDate.ToShortDateString()}");

                DumpCandlesForR(optionCandles, os.Type.ToString());
            }

            //DumpCallsForR(optionsSeries);
            //DumpPutsForR(optionsSeries);

            DumpCandlesForR(candles
                .OrderBy(c => c.TradeDate));
                //.Where(c => c.Open > 0 && c.High > 0 && c.Low > 0 && c.Close > 0))
                //.TakeLast(72));
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

            Console.WriteLine();
            Console.WriteLine($"Open = c({String.Join(',', candles.Select(c => c.Open.ToString().Replace(',', '.')))})");
            Console.WriteLine($"High = c({String.Join(',', candles.Select(c => c.High.ToString().Replace(',', '.')))})");
            Console.WriteLine($"Low = c({String.Join(',', candles.Select(c => c.Low.ToString().Replace(',', '.')))})");
            Console.WriteLine($"Close = c({String.Join(',', candles.Select(c => c.Close.ToString().Replace(',', '.')))})");
            Console.WriteLine("ohlc <- data.frame(Open, High, Low, Close)");
        }

        private static void DumpCandlesForR(IEnumerable<Option> candles, string name)
        {
            Console.WriteLine();
            Console.WriteLine($"{name}Open = c({String.Join(',', candles.Select(c => c.Open.ToString().Replace(',', '.')))})");
            Console.WriteLine($"{name}High = c({String.Join(',', candles.Select(c => c.High.ToString().Replace(',', '.')))})");
            Console.WriteLine($"{name}Low = c({String.Join(',', candles.Select(c => c.Low.ToString().Replace(',', '.')))})");
            Console.WriteLine($"{name}Close = c({String.Join(',', candles.Select(c => c.Close.ToString().Replace(',', '.')))})");
            Console.WriteLine($"{name}OHLC <- data.frame({name}Open, {name}High, {name}Low, {name}Close)");
            Console.WriteLine($"{name}Days = c({String.Join(',', candles.Select(c => (c.Expire - c.TradeDate).Days))})");
        }
    }
}
