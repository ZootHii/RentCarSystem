﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Core.Utilities.IoC;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace Core.CrossCuttingConcerns.Caching.Microsoft
{
    public class MemoryCacheManager : ICacheManager
    {
        private readonly IMemoryCache _memoryCache;

        public MemoryCacheManager()
        {
            _memoryCache = ServiceTool.ServiceProvider.GetService<IMemoryCache>();
        }

        public void AddDataToCache(string key, object value, int duration)
        {
            _memoryCache.Set(key, value, TimeSpan.FromMinutes(duration));
        }

        public object GetDataFromCache(string key)
        {
            return _memoryCache.Get(key);
        }

        public T GetDataFromCache<T>(string key)
        {
            return _memoryCache.Get<T>(key);
        }

        public bool IsDataInCache(string key)
        {
            return _memoryCache.TryGetValue(key, out _);
        }

        public void RemoveDataFromCache(string key)
        {
            _memoryCache.Remove(key);
        }

        public void RemoveDataFromCacheByPattern(string pattern)
        {
            /*var cacheEntriesCollectionInfo = typeof(MemoryCache).GetProperty("EntriesCollection", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var cacheEntriesCollection = cacheEntriesCollectionInfo?.GetValue(_memoryCache) as dynamic;
            
            var cacheCollectionValues = new List<ICacheEntry>();

            if (cacheEntriesCollection != null)
            {
                foreach (var cacheItem in cacheEntriesCollection)
                {
                    ICacheEntry cacheItemValue = cacheItem.GetType().GetProperty("Value").GetValue(cacheItem, null);
                    cacheCollectionValues.Add(cacheItemValue);
                }
            }
            
            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var keysToRemove = cacheCollectionValues.Where(cacheEntry => regex.IsMatch(cacheEntry.ToString()!)).Select(cacheEntry => cacheEntry.Key).ToList();

            Console.WriteLine(pattern+"--------------------------------");
            Console.WriteLine(pattern);
            Console.WriteLine(pattern);
            Console.WriteLine(pattern);*/
            
            var memoryCacheEntriesCollectionInfo = typeof(MemoryCache).GetProperty("EntriesCollection", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var memoryCacheEntriesCollection = memoryCacheEntriesCollectionInfo?.GetValue(_memoryCache) as dynamic;
            
            var memoryCacheCollectionValues = new List<ICacheEntry>();

            foreach (var cacheItem in memoryCacheEntriesCollection)
            {
                ICacheEntry cacheItemValue = cacheItem.GetType().GetProperty("Value").GetValue(cacheItem, null);
                memoryCacheCollectionValues.Add(cacheItemValue);
            }

            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var keysToRemove = memoryCacheCollectionValues.Where(cacheEntry => regex.IsMatch(cacheEntry.Key.ToString()!)).Select(cacheEntry => cacheEntry.Key).ToList();

            
            foreach (var key in keysToRemove)
            {
                _memoryCache.Remove(key);
            }
        }
    }
}