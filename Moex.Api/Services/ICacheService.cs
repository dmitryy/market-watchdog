using System;
using System.Collections.Generic;
using System.Text;

namespace Moex.Api.Services
{
    public interface ICacheService
    {
        T Get<T>(string key);

        T Set<T>(string key, T value);
    }
}
