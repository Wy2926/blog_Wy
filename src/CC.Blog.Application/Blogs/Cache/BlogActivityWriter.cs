using Abp.Dependency;
using Abp.Events.Bus.Entities;
using Abp.Events.Bus.Handlers;
using Abp.Runtime.Caching;
using System;
using System.Collections.Generic;
using System.Text;

namespace CC.Blog.Blogs.Cache
{
    public class BlogActivityWriter :
        IEventHandler<EntityEventData<ArticleType>>,
        IEventHandler<EntityEventData<ArticleTag>>,
        IEventHandler<EntityEventData<Article>>,
        IEventHandler<EntityEventData<BlogProposal>>,
        IEventHandler<EntityEventData<FriendshipLink>>,
        IEventHandler<EntityEventData<ArticleComment>>,
        ITransientDependency
    {

        private readonly ICacheManager _cacheManager;

        public BlogActivityWriter(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }
        public void HandleEvent(EntityEventData<ArticleComment> eventData)
        {
            //当有未审核的评论时需要通知
            if (eventData.Entity.ToExamine == ToExamine.Unaudited)
            {
                _cacheManager
                .GetCache(BlogCacheNames.CacheBlogAuditNotice)
                .Clear();
            }
        }


        public void HandleEvent(EntityEventData<ArticleType> eventData)
        {
            //当ArticleType实体发生修改，立即清空它在Redis中的缓存
            _cacheManager
                .GetCache(BlogCacheNames.CacheBlog)
                .Remove(BlogCacheNames.CacheArticleType);
        }

        public void HandleEvent(EntityEventData<ArticleTag> eventData)
        {
            //当FArticleTag实体发生修改，立即清空它在Redis中的缓存
            _cacheManager
                .GetCache(BlogCacheNames.CacheBlog)
                .Remove(BlogCacheNames.CacheArticleTag);

        }

        public void HandleEvent(EntityEventData<Article> eventData)
        {
            //throw new NotImplementedException();
        }

        public void HandleEvent(EntityEventData<BlogProposal> eventData)
        {
            //throw new NotImplementedException();
        }

        public void HandleEvent(EntityEventData<FriendshipLink> eventData)
        {
            //当FriendshipLink实体发生修改，立即清空它在Redis中的缓存
            _cacheManager
                .GetCache(BlogCacheNames.CacheBlog)
                .Remove(BlogCacheNames.CacheFriendshipLink);
            if (eventData.Entity.ToExamine == ToExamine.Unaudited)
            {
                _cacheManager
                .GetCache(BlogCacheNames.CacheBlogAuditNotice)
                .Clear();
            }
        }
    }
}
