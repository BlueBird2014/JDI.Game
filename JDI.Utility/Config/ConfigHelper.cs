/// <summary>
///     类说明：配置文件操作类
/// </summary>
using System;
using System.Configuration;

namespace JDI.Utility
{
    public class ConfigHelper
    {
        /// <summary>
        ///     得到配置文件字符串信息，先去缓存中取值，如果没有去配置文件中取并放入缓存中
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public static string GetString(string key)
        {
            string cacheKey = "AppSettings-" + key;
            object cacheValue = CacheHelper.GetCache(cacheKey);

            if (cacheValue == null)
            {
                try
                {
                    cacheValue = ConfigurationManager.AppSettings[key];
                    if (cacheValue != null)
                    {
                        CacheHelper.SetCache(cacheKey, cacheValue, DateTime.UtcNow.AddMinutes(10), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return cacheValue+"";
        }


        /// <summary>
        ///     获取AppSettings中的配置Bool信息
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public static bool GetBool(string key)
        {
            bool result = false;
            string configValue = GetString(key);
            if (!string.IsNullOrEmpty(configValue))
            {
                if (configValue.Equals("1") || configValue.ToLower().Equals("true"))
                {
                    result = true;
                }
            }

            return result;
        }

        /// <summary>
        ///     获取AppSetting中酣置Int信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static int GetInt(string key)
        {
            int result = 0;
            string configValue = GetString(key);

            if (!string.IsNullOrEmpty(configValue))
            {
                try
                {
                    result = ConvertHelper.ToInt(configValue);
                }
                catch
                { }
            }
            return result;
        }

        /// <summary>
        ///     获取AppSetting中配置Decimal信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static decimal GetDecimal(string key)
        {
            decimal result = 0;
            string configValue = GetString(key);

            if (!string.IsNullOrEmpty(configValue))
            {
                result = decimal.Parse(configValue);
            }
            return result;
        }
    }
}
