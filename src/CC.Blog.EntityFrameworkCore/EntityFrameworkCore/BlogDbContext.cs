using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using CC.Blog.Authorization.Roles;
using CC.Blog.Authorization.Users;
using CC.Blog.MultiTenancy;
using CC.Blog.Blogs;
using CC.Blog.Spiders;

namespace CC.Blog.EntityFrameworkCore
{
    public class BlogDbContext : AbpZeroDbContext<Tenant, Role, User, BlogDbContext>
    {
        /* Define a DbSet for each entity of the application */
        #region 博客

        public DbSet<Article> Articles { get; set; }
        public DbSet<ArticleComment> ArticleComments { get; set; }
        public DbSet<ArticleTag> ArticleTags { get; set; }
        public DbSet<ArticleType> ArticleTypes { get; set; }
        public DbSet<BlogProposal> BlogProposals { get; set; }
        public DbSet<FriendshipLink> FriendshipLinks { get; set; }

        #endregion

        #region 蜘蛛

        public DbSet<Spider> Spiders { get; set; }
        public DbSet<SearchSubmitHistory> SearchSubmitHistorys { get; set; }

        #endregion
        static BlogDbContext()
        {
            var builder = new BlogDbContextFactory();
            var db = builder.CreateDbContext(null);
            db.Database.Migrate();
        }

        public BlogDbContext(DbContextOptions<BlogDbContext> options)
            : base(options)
        {
        }
    }
}
