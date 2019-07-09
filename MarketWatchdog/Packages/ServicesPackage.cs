﻿using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Moex.Api.Repositories;
using Moex.Api.Services;
using RestSharp;

namespace Market.Watchdog.Packages
{
    public class ServicesPackage
    {
        public static ServiceProvider RegisterServices()
        {
            ServiceProvider serviceProvider = new ServiceCollection()
                .AddSingleton<ICacheService, CacheService>()
                .AddSingleton<IFuturesRepository, FuturesRepository>()
                .AddSingleton<IFuturesService, FuturesService>()
                .AddSingleton<IMemoryCache, MemoryCache>()
                .AddSingleton<IOptions<MemoryCacheOptions>, MemoryCacheOptions>()
                .AddSingleton<IOptionsRepository, OptionsRepository>()
                .AddSingleton<IOptionsService, OptionsService>()
                .AddSingleton<IRestClient, RestClient>()
                .AddSingleton<ISecurityRepository, SecurityRepository>()
                .BuildServiceProvider();

            return serviceProvider;
        }
    }
}
