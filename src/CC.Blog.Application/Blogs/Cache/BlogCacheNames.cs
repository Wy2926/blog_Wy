using System;
using System.Collections.Generic;
using System.Text;

namespace CC.Blog.Blogs.Cache
{
    public static class BlogCacheNames
    {

        #region CacheBlog节点

        /// <summary>
        /// 博客缓存名称
        /// </summary>
        public const string CacheBlog = "Blog";

        /// <summary>
        /// 热门文章缓存名称
        /// </summary>
        public const string CacheRecommendArticle = "RecommendArticle";

        /// <summary>
        /// 标签缓存名称
        /// </summary>
        public const string CacheArticleTag = "ArticleTag";

        /// <summary>
        /// 文章类型缓存名称
        /// </summary>
        public const string CacheArticleType = "ArticleType";

        /// <summary>
        /// 友情连接缓存名称
        /// </summary>
        public const string CacheFriendshipLink = "FriendshipLink";

        /// <summary>
        /// 友情连接缓存名称
        /// </summary>
        public const string CacheFriendshipLinkApply = "FriendshipLinkApply";

        #endregion

        /// <summary>
        /// 博客阅读记录缓存名称
        /// </summary>
        public const string CacheArticleRead = "ArticleRead";

        /// <summary>
        /// 评论记录缓存名称
        /// </summary>
        public const string CacheArticleComment = "ArticleComment";

        /// <summary>
        /// 投递建议记录缓存名称
        /// </summary>
        public const string CacheBlogProposal = "BlogProposal";

        /// <summary>
        /// 通知提示缓存名称
        /// </summary>
        public const string CacheBlogAuditNotice = "BlogAuditNotice";
    }
}
