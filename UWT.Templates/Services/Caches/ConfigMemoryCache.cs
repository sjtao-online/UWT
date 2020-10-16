using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWT.Templates.Services.Caches
{
    /// <summary>
    /// 缓存信息
    /// </summary>
    public static class ConfigMemoryCache
    {
        static Dictionary<string, string> ConfigMemoryCacheMap = new Dictionary<string, string>();

        /// <summary>
        /// 现有所有Key
        /// </summary>
        public static IEnumerable<string> Keys => ConfigMemoryCacheMap.Keys;

        /// <summary>
        /// 获得
        /// </summary>
        /// <param name="key">键(key)</param>
        /// <param name="defaultValue">无指定key时的默认值</param>
        /// <returns>返回key的值</returns>
        public static string Get(string key, string defaultValue = null)
        {
            if (ConfigMemoryCacheMap.ContainsKey(key))
            {
                return ConfigMemoryCacheMap[key];
            }
            else
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// 设置值
        /// </summary>
        /// <param name="key">键(key)</param>
        /// <param name="value">值(value)</param>
        /// <param name="replace">存在是否强制替换</param>
        /// <returns></returns>
        public static bool Set(string key, string value, bool replace = false)
        {
            if (replace)
            {
                ConfigMemoryCacheMap[key] = value;
            }
            else
            {
                if (ConfigMemoryCacheMap.ContainsKey(key))
                {
                    return false;
                }
                ConfigMemoryCacheMap.Add(key, value);
            }
            return true;
        }
    }
}
