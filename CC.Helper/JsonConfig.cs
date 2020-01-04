using CC.Helper.Expand;
using CC.Helper.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CC.Helper
{
    public class JsonConfig<T> where T : IJsonConfig, new()
    {
        public static T Config { get; private set; }
        /// <summary>
        /// 创建或者修改配置
        /// </summary>
        /// <param name="siteConfig"></param>
        /// <returns></returns>
        public static async Task CreateOrUpdateSiteConfig(T siteConfig)
        {
            await siteConfig.FilePath.SaveJsonAsync(siteConfig);
            Config = siteConfig;
        }


        /// <summary>
        /// 获取配置
        /// </summary>
        /// <returns></returns>
        public static async Task<T> GetSiteConfigAsync()
        {
            if (Config != null)
                return Config;
            T t = new T();
            if (!File.Exists(t.FilePath))
                return t;
            return Config = await t.FilePath.GetJsonEntityAsync<T>();
        }

        /// <summary>
        /// 获取配置
        /// </summary>
        /// <returns></returns>
        public static T GetSiteConfig()
        {
            if (Config != null)
                return Config;
            T t = new T();
            if (!File.Exists(t.FilePath))
                return t;
            return Config = t.FilePath.GetJsonEntity<T>();
        }
    }
}
