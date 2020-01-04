using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using CC.Blog.Authorization;
using CC.Blog.Blogs;
using CC.Blog.Blogs.DTO;
using System.Linq;

namespace CC.Blog
{
    [DependsOn(
        typeof(BlogCoreModule),
        typeof(AbpAutoMapperModule))]
    public class BlogApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<BlogAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(BlogApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
            Configuration.Modules.AbpAutoMapper().Configurators.Add(config =>
            {
                config.CreateMap<Article, ArticleOut>()
                      .ForMember(u => u.ArticleStrTags, options => options.MapFrom(input => input.ArticleTags.Select(p => p.Name)))
                      .ForMember(u => u.TypeId, options => options.MapFrom(input => input.ArticleType.Id))
                      .ForMember(u => u.Type, options => options.MapFrom(input => input.ArticleType.Name));
            });

            Configuration.Modules.AbpAutoMapper().Configurators.Add(config =>
            {
                config.CreateMap<Article, ArticleDetailsOut>()
                      .ForMember(u => u.ArticleStrTags, options => options.MapFrom(input => input.ArticleTags.Select(p => p.Name)))
                      .ForMember(u => u.TypeId, options => options.MapFrom(input => input.ArticleType.Id))
                      .ForMember(u => u.Type, options => options.MapFrom(input => input.ArticleType.Name));
            });

            Configuration.Modules.AbpAutoMapper().Configurators.Add(config =>
            {
                config.CreateMap<ArticleComment, ArticleCommentOut>()
                      .ForMember(u => u.UserId, options => options.MapFrom(input => input.CreatorUserId));
            });

        }
    }
}
