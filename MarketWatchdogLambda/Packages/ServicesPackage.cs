using MarketDataStorage.Services;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Moex.Api.Repositories;
using Moex.Api.Services;
using RestSharp;

namespace MarketWatchdogLambda.Packages
{
    public class ServicesPackage
    {
        public static ServiceProvider RegisterServices()
        {
            return new ServiceCollection()
                .AddSingleton<ICacheService, CacheService>()
                .AddSingleton<IFuturesRepository, FuturesRepository>()
                .AddSingleton<IFuturesService, FuturesService>()
                .AddSingleton<IMemoryCache, MemoryCache>()
                .AddSingleton<IOptions<MemoryCacheOptions>, MemoryCacheOptions>()
                .AddSingleton<IOptionsRepository, OptionsRepository>()
                .AddSingleton<IOptionTradeRepository, OptionTradeRepository>()
                .AddSingleton<IOptionsService, OptionsService>()
                .AddSingleton<IOptionTradeService, OptionTradeService>()
                .AddSingleton<IRestClient, RestClient>()
                .AddSingleton<ISecurityRepository, SecurityRepository>()
                .AddSingleton<ITradeRepository, TradeRepository>()
                .AddSingleton<IStorageService, S3StorageService>()
                .BuildServiceProvider();
        }
    }
}
