using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericCache
{
    public class FunctionCache<TKey, TResult>
    {
        private Dictionary<TKey, CacheItem> cache = new Dictionary<TKey, CacheItem>();

        public TResult GetOrAdd(TKey key, Func<TKey, TResult> function, TimeSpan expirationTime)
        {
            if (cache.TryGetValue(key, out var cacheItem) && !cacheItem.IsExpired(expirationTime))
            {
                Console.WriteLine($"Значення з ключем {key} з кешу.");
                return cacheItem.Value;
            }

            Console.WriteLine($"Виконання функції для ключа {key}.");
            var result = function(key);

            cache[key] = new CacheItem(result, DateTime.Now);
            return result;
        }

        private class CacheItem
        {
            public TResult Value { get; }
            public DateTime CachedTime { get; }

            public CacheItem(TResult value, DateTime cachedTime)
            {
                Value = value;
                CachedTime = cachedTime;
            }

            public bool IsExpired(TimeSpan expirationTime)
            {
                return DateTime.Now - CachedTime > expirationTime;
            }
        }
    }

    class Program
    {
        static void Main()
        {
            FunctionCache<int, string> cache = new FunctionCache<int, string>();

            Func<int, string> expensiveFunction = key =>
            {
                Console.WriteLine($"Виклик дорогої функції для ключа {key}.");
                return $"Result for {key}";
            };

            string result1 = cache.GetOrAdd(1, expensiveFunction, TimeSpan.FromSeconds(10));
            Console.WriteLine($"Результат 1: {result1}");

            string result2 = cache.GetOrAdd(1, expensiveFunction, TimeSpan.FromSeconds(10));
            Console.WriteLine($"Результат 2: {result2}");

            Console.ReadLine();
        }
    }

}
