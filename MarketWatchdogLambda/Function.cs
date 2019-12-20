using System;
using System.Linq;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Market.Common.Utils;
using MarketDataStorage.Services;
using MarketWatchdogLambda.Packages;
using Microsoft.Extensions.DependencyInjection;
using Moex.Api.Services;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace MarketWatchdogLambda
{
    public class Function
    {
        private readonly IFuturesService _futuresService;
        private readonly IOptionsService _optionsService;
        private readonly IOptionTradeService _optionsTradeService;
        private readonly IStorageService _storageService;

        public Function()
        {
            var services = ServicesPackage.RegisterServices();

            _futuresService = services.GetService<IFuturesService>();
            _optionsService = services.GetService<IOptionsService>();
            _optionsTradeService = services.GetService<IOptionTradeService>();
            _storageService = services.GetService<IStorageService>();
        }

        public async Task FunctionHandler(string asset, ILambdaContext context)
        {
            context.Logger.Log($"Handler started with parameter: {asset}");

            var assetCode = AssetUtils.GetAssetCode(asset.ToUpper());
            var allfutures = await _futuresService.GetAllAsync(assetCode);
            var allOptions = await _optionsService.GetAllAsync(assetCode);

            long filesCount = 0;
            long tradesCount = 0;

            var future = allfutures.Reverse().First();
            //foreach (var future in allfutures.Reverse())
            {
                var futureOptions = allOptions.Where(o => o.Futures.SecId.Equals(future.SecId));

                foreach (var option in futureOptions)
                {
                    var trades = await _optionsTradeService.GetTrades(option.SecId);
                    if (trades.Any())
                    {
                        try
                        {
                            await _storageService.SaveAsync(future, option, trades);

                            filesCount++;
                            tradesCount += trades.Sum(t => t.Quantity);
                        }
                        catch (Exception e)
                        {
                            context.Logger.Log(e.Message);
                        }
                    }
                }

                context.Logger.Log($"Saved trades for {future.SecId}");
            }

            context.Logger.Log($"Data stored, {filesCount} files created with {tradesCount} trades.");
        }
    }
}
