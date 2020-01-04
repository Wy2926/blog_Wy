using Abp.Application.Services;
using Abp.Dependency;
using CC.Blog.Sitemaps.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CC.Blog.Sitemaps
{
    public interface ISitemapAppService : IApplicationService, ITransientDependency
    {
        /// <summary>
        /// 获取sitemap内容
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        Task<Urlset> GetUrlsetAsync(string url);
    }
}
