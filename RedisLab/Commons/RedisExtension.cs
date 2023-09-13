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
            var json = JsonConvert.SerializeObject(o, Formatting.None);
            cache.SetString(key, json);
        }

        public static void SetObject(this IDistributedCache cache, string key, object o, DistributedCacheEntryOptions options)
        {
            var json = JsonConvert.SerializeObject(o, Formatting.None);
            cache.SetString(key, json, options);
        }

        public static void SetObject(this IDistributedCache cache, string key, object o, TimeSpan absoluteExpiration)
        {
            var options = new DistributedCacheEntryOptions();
            options.SetAbsoluteExpiration(absoluteExpiration);
            cache.SetObject(key, o, options);
        }

        public static void SetObject(this IDistributedCache cache, string key, object o, uint segundosDuracao)
        {
            cache.SetObject(key, o, TimeSpan.FromSeconds(segundosDuracao));
        }

        public static T GetObject<T>(this IDistributedCache cache, string key)
        {
            var value = cache.GetString(key);
            if (value == null)
                return default(T);

            return JsonConvert.DeserializeObject<T>(value);
        }

        public static async Task SetObjectAsync(this IDistributedCache cache, string key, object o)
        {
            var json = JsonConvert.SerializeObject(o, Formatting.None);
            await cache.SetStringAsync(key, json);
        }

        public static async Task SetObjectAsync(this IDistributedCache cache, string key, object o, DistributedCacheEntryOptions options)
        {
            var json = JsonConvert.SerializeObject(o, Formatting.None);
            await cache.SetStringAsync(key, json, options);
        }

        public static async Task SetObjectAsync(this IDistributedCache cache, string key, object o, TimeSpan absoluteExpiration)
        {
            var options = new DistributedCacheEntryOptions();
            options.SetAbsoluteExpiration(absoluteExpiration);
            await cache.SetObjectAsync(key, o, options);
        }

        public static async Task SetObjectAsync(this IDistributedCache cache, string key, object o, uint segundosDuracao)
        {
            await cache.SetObjectAsync(key, o, TimeSpan.FromSeconds(segundosDuracao));
        }

        public static async Task<T> GetObjectAsync<T>(this IDistributedCache cache, string key)
        {
            var value = await cache.GetStringAsync(key);
            if (value == null)
                return default(T);

            return JsonConvert.DeserializeObject<T>(value);
        }

        public static T GetSetObject<T>(this IDistributedCache cache, string key, Func<T> fn)
        {
            var valor = cache.GetObject<T>(key);
            if (valor == null)
            {
                valor = fn();
                cache.SetObject(key, valor);
            }

            return valor;
        }

        public static T GetSetObject<T>(this IDistributedCache cache, string key, DistributedCacheEntryOptions options, Func<T> fn)
        {
            var valor = cache.GetObject<T>(key);
            if (valor == null)
            {
                valor = fn();
                cache.SetObject(key, valor, options);
            }

            return valor;
        }

        public static T GetSetObject<T>(this IDistributedCache cache, string key, TimeSpan absoluteExpiration, Func<T> fn)
        {
            var valor = cache.GetObject<T>(key);
            if (valor == null)
            {
                valor = fn();
                cache.SetObject(key, valor, absoluteExpiration);
            }

            return valor;
        }

        public static T GetSetObject<T>(this IDistributedCache cache, string key, uint segundosDuracao, Func<T> fn)
        {
            var valor = cache.GetObject<T>(key);
            if (valor == null)
            {
                valor = fn();
                cache.SetObject(key, valor, TimeSpan.FromSeconds(segundosDuracao));
            }

            return valor;
        }

        public static async Task<T> GetSetObjectAsync<T>(this IDistributedCache cache, string key, Func<T> fn)
        {
            var valor = await cache.GetObjectAsync<T>(key);
            if (valor == null)
            {
                valor = fn();
                await cache.SetObjectAsync(key, valor);
            }

            return valor;
        }

        public static async Task<T> GetSetObjectAsync<T>(this IDistributedCache cache, string key, DistributedCacheEntryOptions options, Func<T> fn)
        {
            var valor = await cache.GetObjectAsync<T>(key);
            if (valor == null)
            {
                valor = fn();
                await cache.SetObjectAsync(key, valor, options);
            }

            return valor;
        }

        public static async Task<T> GetSetObjectAsync<T>(this IDistributedCache cache, string key, TimeSpan absoluteExpiration, Func<T> fn)
        {
            var valor = await cache.GetObjectAsync<T>(key);
            if (valor == null)
            {
                valor = fn();
                await cache.SetObjectAsync(key, valor, absoluteExpiration);
            }

            return valor;
        }

        public static async Task<T> GetSetObjectAsync<T>(this IDistributedCache cache, string key, uint segundosDuracao, Func<T> fn)
        {
            var valor = await cache.GetObjectAsync<T>(key);
            if (valor == null)
            {
                valor = fn();
                await cache.SetObjectAsync(key, valor, TimeSpan.FromSeconds(segundosDuracao));
            }

            return valor;
        }

        public static string GetSetString(this IDistributedCache cache, string key, uint segundosDuracao, Func<string> fn)
        {
            var valor = cache.GetString(key);
            if (string.IsNullOrWhiteSpace(valor))
            {
                valor = fn();
                cache.SetString(key, valor, new DistributedCacheEntryOptions { SlidingExpiration = TimeSpan.FromSeconds(segundosDuracao) });
            }

            return valor;
        }

        public static async Task<string> GetSetStringAsync(this IDistributedCache cache, string key, uint segundosDuracao, Func<string> fn)
        {
            var valor = await cache.GetStringAsync(key);
            if (string.IsNullOrWhiteSpace(valor))
            {
                valor = fn();
                await cache.SetStringAsync(key, valor, new DistributedCacheEntryOptions { SlidingExpiration = TimeSpan.FromSeconds(segundosDuracao) });
            }

            return valor;
        }
    }
}
