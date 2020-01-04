using System;
using System.Collections.Generic;
using System.Text;
using Abp;
using Abp.Domain.Repositories;
using Abp.Runtime.Caching;
using CC.Blog.Blogs;
using CC.Blog.Sitemaps.Dto;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CC.Blog.Sitemaps.Cache;

namespace CC.Blog.Sitemaps
{
    public class SitemapAppService : AbpServiceBase, ISitemapAppService
    {
        private readonly IRepository<Article, long> _articleRepository;
        private readonly IRepository<ArticleTag, long> _articleTagRepository;
        private readonly IRepository<ArticleType> _articleTypeRepository;
        private readonly ICacheManager _cacheManager;
        public SitemapAppService(
            IRepository<Article, long> articleRepository,
            IRepository<ArticleTag, long> articleTagRepository,
            IRepository<ArticleType> articleTypeRepository,
            ICacheManager cacheManager
            )
        {
            _articleRepository = articleRepository;
            _articleTagRepository = articleTagRepository;
            _articleTypeRepository = articleTypeRepository;
            _cacheManager = cacheManager;
        }
        public async Task<Urlset> GetUrlsetAsync(string url)
        {
            return await _cacheManager.GetCache(SitemapCacheNames.CacheSitemap)
                  .GetAsync("Admin", async k =>
                  {
                      var articleIds = await _articleRepository
                      .GetAll()
                      .OrderByDescending(p => p.CreationTime)
                      .Select(p => p.Id)
                      .ToListAsync();
                      var typeIds = await _articleTypeRepository
                          .GetAll()
                          .Select(p => p.Id)
                          .ToListAsync();
                      var tagNames = await _articleTagRepository
                          .GetAll()
                          .Select(p => p.Name)
                          .ToListAsync();
                      Urlset urlset = new Urlset();
                      urlset.Urls.Add(new UrlDto() { loc = url, lastmod = DateTime.Now.ToShortDateString(), changefreq = "weekly", priority = "1.00" });
                      foreach (var item in typeIds)
                      {
                          urlset.Urls.Add(new UrlDto() { loc = $"{url}/Article/Type_{item}.html", changefreq = "weekly", priority = "0.80" });
                      }
                      foreach (var item in articleIds)
                      {
                          urlset.Urls.Add(new UrlDto() { loc = $"{url}/Article/Details_{item}.html", changefreq = "daily", priority = "0.90" });
                      }
                      foreach (var item in tagNames)
                      {
                          urlset.Urls.Add(new UrlDto() { loc = $"{url}/Article/Tag_{item}.html", changefreq = "daily", priority = "0.70" });
                      }
                      return urlset;
                  }) as Urlset;
        }
    }
}
