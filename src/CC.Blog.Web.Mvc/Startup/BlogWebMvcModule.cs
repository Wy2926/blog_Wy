using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using CC.Blog.Configuration;
using Abp.Runtime.Caching.Redis;
using System;
using CC.Blog.Blogs.Cache;
using CC.Blog.Sitemaps.Cache;
using CC.Blog.Users.Cache;

namespace CC.Blog.Web.Startup
{
    [DependsOn(typeof(BlogWebCoreModule),
        typeof(AbpRedisCacheModule))]
    public class BlogWebMvcModule : AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public BlogWebMvcModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void PreInitialize()
        {
            Configuration.Navigation.Providers.Add<BlogNavigationProvider>();
            //配置使用Redis缓存
            //Configuration.Caching.UseRedis(options =>
            //{
            //    options.ConnectionString = ConfigHelper.GetAppSetting("App", "RedisCache:ConnectionString");
            //    options.DatabaseId = DatabaseId;
            //});

            //配置阅读记录缓存过期时间为12小时
            Configuration.Caching.Configure(BlogCacheNames.CacheArticleRead, cache =>
            {
                cache.DefaultSlidingExpireTime = TimeSpan.FromHours(12);
            });

            //配置评论记录缓存过期时间为1小时
            Configuration.Caching.Configure(BlogCacheNames.CacheArticleComment, cache =>
            {
                cache.DefaultSlidingExpireTime = TimeSpan.FromHours(1);
            });

            //配置热门文章缓存过期时间为1小时
            Configuration.Caching.Configure(BlogCacheNames.CacheRecommendArticle, cache =>
            {
                cache.DefaultSlidingExpireTime = TimeSpan.FromHours(1);
            });

            //配置友链申请缓存过期时间为1小时
            Configuration.Caching.Configure(BlogCacheNames.CacheFriendshipLinkApply, cache =>
            {
                cache.DefaultSlidingExpireTime = TimeSpan.FromMinutes(10);
            });

            //建议缓存过期时间为1小时
            Configuration.Caching.Configure(BlogCacheNames.CacheBlogProposal, cache =>
            {
                cache.DefaultSlidingExpireTime = TimeSpan.FromMinutes(10);
            });

            //审核通知缓存过期时间为1小时
            Configuration.Caching.Configure(BlogCacheNames.CacheBlogProposal, cache =>
            {
                cache.DefaultSlidingExpireTime = TimeSpan.FromHours(1);
            });
            //站点缓存过期时间为12小时
            Configuration.Caching.Configure(SitemapCacheNames.CacheSitemap, cache =>
            {
                cache.DefaultSlidingExpireTime = TimeSpan.FromHours(12);
            });

            //用户注册邮箱验证码缓存过期时间为15分钟
            Configuration.Caching.Configure(UserCacheNames.CacheRegisterEmailCode, cache =>
            {
                cache.DefaultSlidingExpireTime = TimeSpan.FromMinutes(15);
            });
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(BlogWebMvcModule).GetAssembly());
        }
    }
}
