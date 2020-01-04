using CC.Helper.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace CC.Blog.Spiders.Dto
{
    /// <summary>
    /// 搜索引擎提交链接
    /// </summary>
    public class SubmitSpiderConfig: IJsonConfig
    {
        /// <summary>
        /// 百度Key
        /// </summary>
        public string BaiduKey { get; set; }

        /// <summary>
        /// 360Key
        /// </summary>
        public string YahuKey { get; set; }

        /// <summary>
        /// 必应Key
        /// </summary>
        public string BiYingKey { get; set; }

        public string FilePath => ConfigPath;

        /// <summary>
        /// 提交配置路径
        /// </summary>
        private static string ConfigPath = $"{Directory.GetParent(Assembly.GetEntryAssembly().Location).FullName}/SubmitConfig.json";
    }
}
