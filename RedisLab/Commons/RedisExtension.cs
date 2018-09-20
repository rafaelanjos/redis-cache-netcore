using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedisLab.Commons
{
    public static class RedisExtension
    {
        public static void SetObject(this IDistributedCache cache, string key, object o, DistributedCacheEntryOptions options = null)
        {
            var json = JsonConvert.SerializeObject(o);
            cache.SetString(key, json, options);
        }

        public static void SetObject(this IDistributedCache cache, string key, object o, TimeSpan absoluteExpiration)
        {
            var options = new DistributedCacheEntryOptions();
            options.SetAbsoluteExpiration(absoluteExpiration);
            var json = JsonConvert.SerializeObject(o);
            cache.SetString(key, json, options);
        }

        public static T GetObject<T>(this IDistributedCache cache, string key)
        {
            var value = cache.GetString(key);
            if (value == null)
                return default(T);

            return JsonConvert.DeserializeObject<T>(value);
        }
    }
}
