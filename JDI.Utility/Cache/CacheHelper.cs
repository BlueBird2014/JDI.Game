using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;

namespace JDI.Utility
{
    public class CacheHelper
    {
        /// <summary>
        ///     获取cache对象 
        /// </summary>
        /// <param name="cacheKey">键</param>
        /// <returns></returns>
        public static object GetCache(string cacheKey)
        {
            Cache cache = HttpRuntime.Cache;
            return cache[cacheKey];
        }

        /// <summary>
        ///     设置cache缓存 带过期时间设置
        /// </summary>
        /// <param name="cacheKey">键</param>
        /// <param name="cacheValue">值</param>
        /// <param name="absoluteExpiration">绝对到期时间，用夏时制，DateTime.UtcNow 不采用DateTime.Now</param>
        /// <param name="slidingExpiration">最后一次访问时间间隔</param>
        public static void SetCache(string cacheKey, object cacheValue, DateTime absoluteExpiration, TimeSpan slidingExpiration)
        {
            Cache cache = HttpRuntime.Cache;
            cache.Insert(cacheKey, cacheValue, null, absoluteExpiration, slidingExpiration);
        }

        /// <summary>
        ///     设置cahce缓存 带间隔过期时间
        /// </summary>
        /// <param name="cacheKey">键</param>
        /// <param name="cacheValue">值</param>
        /// <param name="timeout">过期时间间隔</param>
        public static void SetCache(string cacheKey, object cacheValue, TimeSpan timeout)
        {
            SetCache(cacheKey, cacheKey, Cache.NoAbsoluteExpiration, timeout);
        }

        /// <summary>
        ///     设置cahce缓存
        /// </summary>
        /// <param name="cacheKey">键</param>
        /// <param name="cacheValue">值</param>
        public static void SetCache(string cacheKey, object cacheValue)
        {
            Cache cache = HttpRuntime.Cache;
            cache.Insert(cacheKey, cacheValue);
        }

        /// <summary>
        ///     移除缓存
        /// </summary>
        /// <param name="cacheKey">键</param>
        public static void RemoveCache(string cacheKey)
        {
            Cache cache = HttpRuntime.Cache;
            cache.Remove(cacheKey);
        }

        /// <summary>
        ///     移除所有缓存
        /// </summary>
        public static void RemoveAllCache()
        {
            RemoveAllCache("");
        }

        /// <summary>
        ///     移除缓存 指定项除外
        /// </summary>
        /// <param name="excludeCahce">不移除的项</param>
        public static void RemoveAllCache(string excludeCahce)
        {
            Cache cache = HttpRuntime.Cache;
            IDictionaryEnumerator cacheEnum = cache.GetEnumerator();
            while (cacheEnum.MoveNext())
            {
                if (!string.IsNullOrEmpty(excludeCahce) && cacheEnum.Key.ToString().Trim().Equals(excludeCahce))
                    continue;

                cache.Remove(cacheEnum.Key.ToString());
            }
        }

        /// <summary>
        ///     获得所有缓存键
        /// </summary>
        /// <returns></returns>
        public static ArrayList GetAllCacheKey()
        {
            ArrayList lst = new ArrayList();
            Cache cache = HttpRuntime.Cache;
            IDictionaryEnumerator cacheEnum = cache.GetEnumerator();
            while (cacheEnum.MoveNext())
            {
                lst.Add(cacheEnum.Key);
            }
            return lst;
        }

    }
}
