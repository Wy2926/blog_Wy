using CC.Helper.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace CC.Blog.Blogs.DTO
{
    /// <summary>
    /// 博客站点配置
    /// </summary>
    public class BlogSiteConfig : ICloneable, IJsonConfig
    {
        /// <summary>
        /// 站点配置路径
        /// </summary>
        private static string SiteConfig = $"{Directory.GetParent(Assembly.GetEntryAssembly().Location).FullName}/siteconfig.json";

        /// <summary>
        /// 站点名称
        /// </summary>
        public string SiteName { get; set; }

        /// <summary>
        /// 站点首页标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 站点关键词
        /// </summary>
        public string KeyWords { get; set; }

        /// <summary>
        /// 站点简介
        /// </summary>

        public string Desc { get; set; }

        /// <summary>
        /// 座右铭
        /// </summary>
        public string Motto { get; set; }

        /// <summary>
        /// 座右铭-昵称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Head的HTML代码
        /// </summary>
        public string HeadHtml { get; set; }

        /// <summary>
        /// 全局JS代码
        /// </summary>

        public string StaticJavaScrpit { get; set; }

        /// <summary>
        /// 备案号
        /// </summary>
        public string RecordNumber { get; set; }

        /// <summary>
        /// 微信公众号图片链接
        /// </summary>
        public string WeChat { get; set; }

        /// <summary>
        /// 右侧广告代码
        /// </summary>
        public string RightAdvertisement { get; set; }

        public string FilePath => SiteConfig;

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
