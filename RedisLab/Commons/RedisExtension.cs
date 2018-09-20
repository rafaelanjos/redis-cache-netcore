using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;

namespace RedisLab.Commons
{
    public static class RedisExtension
    {
        /// <summary>
        /// Cria uma chave e seu valor no REDIS
        /// </summary>
        /// <param name="key"></param>
        /// <param name="o">Value do redis suporta 500mb</param>
        public static void SetObject(this IDistributedCache cache, string key, object o)
        {
            var json = JsonConvert.SerializeObject(o);
            cache.SetString(key, json);
        }

        public static void SetObject(this IDistributedCache cache, string key, object o, DistributedCacheEntryOptions options)
        {
            var json = JsonConvert.SerializeObject(o);
            cache.SetString(key, json, options);
        }

        public static void SetObject(this IDistributedCache cache, string key, object o, TimeSpan absoluteExpiration)
        {
            var options = new DistributedCacheEntryOptions();
            options.SetAbsoluteExpiration(absoluteExpiration);
            cache.SetObject(key, o, options);
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
