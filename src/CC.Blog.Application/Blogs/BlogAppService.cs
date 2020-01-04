using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Runtime.Caching;
using Abp.Runtime.Session;
using Abp.UI;
using CC.Blog.Authorization.Users;
using CC.Blog.Blogs.DTO;
using System.Linq;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using CC.Helper.Expand;
using CC.Blog.Blogs.Cache;
using Abp.Auditing;
using Abp.Linq.Extensions;
using Abp.Authorization;
using System.IO;
using CC.Blog.Authorization;
using CC.Helper;
using CC.Blog.PublicDto.Expand;

namespace CC.Blog.Blogs
{

    public class BlogAppService : AbpServiceBase, IBlogAppService
    {
        private readonly IRepository<Article, long> _articleRepository;
        private readonly IRepository<ArticleComment, long> _articleCommentRepository;
        private readonly IRepository<ArticleTag, long> _articleTagRepository;
        private readonly IRepository<ArticleType> _articleTypeRepository;
        private readonly IRepository<BlogProposal> _blogProposalRepository;
        private readonly IRepository<FriendshipLink> _blogFriendshipLinkRepository;
        private readonly UserManager _userManager;
        private readonly ICacheManager _cacheManager;
        private readonly IClientInfoProvider _clientInfoProvider;
        private readonly IPermissionChecker _permissionChecker;
        public IAbpSession AbpSession { get; set; }
        public BlogAppService(
            IRepository<Article, long> articleRepository,
            IRepository<ArticleComment, long> articleCommentRepository,
            IRepository<ArticleTag, long> articleTagRepository,
            IRepository<ArticleType> articleTypeRepository,
            IRepository<BlogProposal> blogProposalRepository,
            IRepository<FriendshipLink> blogFriendshipLinkRepository,
            UserManager userManager,
            ICacheManager cacheManager,
            IClientInfoProvider clientInfoProvider,
            IPermissionChecker permissionChecker)
        {
            _articleRepository = articleRepository;
            _articleCommentRepository = articleCommentRepository;
            _articleTagRepository = articleTagRepository;
            _articleTypeRepository = articleTypeRepository;
            _blogProposalRepository = blogProposalRepository;
            _blogFriendshipLinkRepository = blogFriendshipLinkRepository;
            _userManager = userManager;
            _cacheManager = cacheManager;
            _clientInfoProvider = clientInfoProvider;
            _permissionChecker = permissionChecker;
            AbpSession = NullAbpSession.Instance;
        }

        #region 私有方法

        #endregion


        [AbpAuthorize(PermissionNames.Pages_Blogs)]
        public async Task ArticleCreateOrUpdateAsync(ArticleCreateOrUpdate article)
        {
            Article _article = null;
            if (article.Id.HasValue)
            {
                var article_ = await _articleRepository.FirstOrDefaultAsync(article.Id.Value);
                //只有管理有权修改他人文章
                if (article_.CreatorUserId != AbpSession.UserId && await _permissionChecker.IsGrantedAsync(PermissionNames.Pages_Blogs_Admin))
                    throw new UserFriendlyException(403, "不能修改他人文章内容");
                _article = ObjectMapper.Map(article, article_);
                await _articleTagRepository.DeleteAsync(p => p.ArticleId == article.Id);
            }
            else
                _article = ObjectMapper.Map<Article>(article);
            var articleType = await _articleTypeRepository.FirstOrDefaultAsync(p => p.Id == article.ArticleTypeId);
            if (articleType == null)
                throw new UserFriendlyException(400, "文章类型不存在");
            _article.Body = _article.Title + _article.Html.GetHtmlText();
            //添加或修改文章
            await _articleRepository.InsertOrUpdateAsync(_article);
            #region 获取创建标签
            foreach (var item in article.ArticleStrTags)
            {
                var tag = new ArticleTag() { Name = item, ArticleId = _article.Id };
                await _articleTagRepository.InsertAsync(tag);
            }
            #endregion
        }

        [AbpAuthorize(PermissionNames.Pages_Blogs)]
        public async Task DeleteArticleAsync(long articleId)
        {
            var _article = await _articleRepository.FirstOrDefaultAsync(articleId);
            //只有管理才有权删除他人文章
            if (_article.CreatorUserId != AbpSession.UserId && await _permissionChecker.IsGrantedAsync(PermissionNames.Pages_Blogs_Admin))
                throw new UserFriendlyException(403, "不能删除他人文章内容");
            await _articleRepository.DeleteAsync(_article);
        }

        public async Task<PagedResultDto<ArticleOut>> SearchArticlesAsync(string keyWord, int index = 1, int pageSize = 10)
        {
            var articles = await _articleRepository
                  .GetAll()
                  .Where(p => p.Body.Contains(keyWord))
                  .OrderByDescending(p => p.CreationTime)
                  .Page(index, pageSize)
                  .Include(p => p.ArticleType)
                  .Include(p => p.ArticleTags)
                  .ToListAsync();
            int count = await _articleRepository
                  .GetAll()
                  .Where(p => p.Body.Contains(keyWord))
                  .CountAsync();
            return new PagedResultDto<ArticleOut>(count, ObjectMapper.Map<List<ArticleOut>>(articles));
        }

        public async Task<PagedResultDto<ArticleOut>> GetArticlesByTypeIdAsync(int? typeId, int index = 1, int pageSize = 10)
        {
            var articles = await _articleRepository
                  .GetAll()
                  .WhereIfTree(typeId.HasValue, p => p.ArticleType.Id == typeId.Value)
                  .OrderByDescending(p => p.CreationTime)
                  .Page(index, pageSize)
                  .Include(p => p.ArticleType)
                  .Include(p => p.ArticleTags)
                  .ToListAsync();
            int count = await _articleRepository
                  .GetAll()
                  .WhereIfTree(typeId.HasValue, p => p.ArticleType.Id == typeId.Value)
                  .CountAsync();
            return new PagedResultDto<ArticleOut>(count, ObjectMapper.Map<List<ArticleOut>>(articles));
        }

        public async Task<PagedResultDto<ArticleOut>> GetArticlesByTagNameAsync(string tagName, int index = 1, int pageSize = 10)
        {
            var articles = await _articleRepository
                  .GetAll()
                  .Where(p => p.ArticleTags.Any(k => k.Name == tagName))
                  .OrderByDescending(p => p.CreationTime)
                  .Page(index, pageSize)
                  .Include(p => p.ArticleType)
                  .Include(p => p.ArticleTags)
                  .ToListAsync();
            int count = await _articleRepository
                  .GetAll()
                  .Where(p => p.ArticleTags.Any(k => k.Name == tagName))
                  .CountAsync();
            return new PagedResultDto<ArticleOut>(count, ObjectMapper.Map<List<ArticleOut>>(articles));
        }

        public async Task<ICollection<ArticleOut>> GetArticlesRecommendByTypeIdAsync(int? typeId = null, int index = 1, int pageSize = 10)
        {
            return await _cacheManager.GetCache(BlogCacheNames.CacheRecommendArticle)
                  .GetAsync($"{typeId ?? 0}_{index}_{pageSize}", async () =>
                     {
                         ICollection<Article> articles = await _articleRepository.GetAll()
                              .WhereIfTree(typeId.HasValue, p => p.ArticleTypeId == typeId.Value)
                              .OrderByDescending(p => p.ReadingNum)
                              .Page(index, pageSize)
                              //.Include(p => p.ArticleType)
                              //.Include(p => p.ArticleTags)
                              .ToListAsync();
                         return ObjectMapper.Map<List<ArticleOut>>(articles);
                     });
        }

        public async Task<(ArticleOut up, ArticleOut down)> GetArticlesUpDownByIdAsync(long id, int? typeId)
        {
            Article up = await _articleRepository
                 .GetAll()
                 .Where(p => p.Id < id)
                 .WhereIfTree(typeId.HasValue, p => p.ArticleType.Id == typeId)
                 .OrderByDescending(p => p.CreationTime)
                 .Include(p => p.ArticleType)
                 .Include(p => p.ArticleTags)
                 .FirstOrDefaultAsync();
            Article down = await _articleRepository
                 .GetAll()
                 .Where(p => p.Id > id)
                 .WhereIfTree(typeId.HasValue, p => p.ArticleType.Id == typeId)
                 .FirstOrDefaultAsync();
            return (ObjectMapper.Map<ArticleOut>(up), ObjectMapper.Map<ArticleOut>(down));
        }

        public async Task<ArticleDetailsOut> GetArticleDetailAsync(long articleId, bool flag = false)
        {
            var article = await _articleRepository
                .GetAll()
                .Include(p => p.ArticleTags)
                .Include(p => p.ArticleType)
                .FirstOrDefaultAsync(p => p.Id == articleId);
            if (article == null)
                throw new UserFriendlyException(404, "文章已经删除或者不存在");
            if (flag)
            {
                var read = await _cacheManager.GetCache(BlogCacheNames.CacheArticleRead)
                      .GetOrDefaultAsync($"{_clientInfoProvider.ClientIpAddress}_{articleId}");
                if (read == null)
                {
                    article.ReadingNum++;
                    await _cacheManager.GetCache(BlogCacheNames.CacheArticleRead)
                        .SetAsync($"{_clientInfoProvider.ClientIpAddress}_{articleId}", DateTime.Now.ToString());
                }
            }
            return ObjectMapper.Map<ArticleDetailsOut>(article);
        }

        public async Task<PagedResultDto<ArticleCommentOut>> GetArticleCommentByIdAsync(long articleId, int index = 1, int pageSize = 10)
        {
            var comments = await _articleCommentRepository
                  .GetAll()
                  .Where(p => p.ArticleId == articleId && p.ToExamine == ToExamine.Adopt && !p.ParArticleCommentId.HasValue)
                  .OrderByDescending(p => p.CreationTime)
                  .Page(index, pageSize)
                  .ToListAsync();
            int count = await _articleCommentRepository
                  .GetAll()
                  .Where(p => p.ArticleId == articleId && p.ToExamine == ToExamine.Adopt)
                  .CountAsync();
            foreach (var item in comments)
            {
                item.SubComments = await _articleCommentRepository
                    .GetAll()
                    .Where(p => p.ParArticleCommentId.HasValue && p.ParArticleCommentId.Value == item.Id && p.ToExamine == ToExamine.Adopt)
                    .ToListAsync();
            }
            return new PagedResultDto<ArticleCommentOut>(count, ObjectMapper.Map<List<ArticleCommentOut>>(comments));
        }

        public async Task<PagedResultDto<ArticleCommentOut>> GetArticleCommentsAsync(int index = 1, int pageSize = 10)
        {
            var comments = await _articleCommentRepository
                 .GetAll()
                 .OrderByDescending(p => p.CreationTime)
                 .Page(index, pageSize)
                 .ToListAsync();
            int count = await _articleCommentRepository
                  .GetAll()
                  .CountAsync();
            return new PagedResultDto<ArticleCommentOut>(count, ObjectMapper.Map<List<ArticleCommentOut>>(comments));
        }

        /// <summary>
        /// 评论条件查询（后台API）
        /// </summary>
        /// <param name="selectDto"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<ArticleCommentOut>> GetAdminArticleCommentsAsync(CommentSelectDto selectDto)
        {
            var query = _articleCommentRepository
                .GetAll()
                .WhereTimeFrame(selectDto)
                .WhereIf(selectDto.Ip != null, p => p.Ip.Contains(selectDto.Ip))
                .WhereIf(selectDto.ToExamine.HasValue, p => p.ToExamine == selectDto.ToExamine);
            var comments = await query
                 .OrderByDescending(p => p.CreationTime)
                 .WherePaging(selectDto)
                 .ToListAsync();
            int count = await query.CountAsync();
            return new PagedResultDto<ArticleCommentOut>(count, ObjectMapper.Map<List<ArticleCommentOut>>(comments));
        }

        public async Task CreateArticleCommentAsync(ArticleCommentInput articleComment)
        {
            var commentLast = await _cacheManager.GetCache(BlogCacheNames.CacheArticleComment)
                  .GetOrDefaultAsync(_clientInfoProvider.ClientIpAddress);
            if (commentLast != null)
                throw new UserFriendlyException(401, $"上次评论时间为{commentLast},必须间隔1分钟");
            var _articleComment = ObjectMapper.Map<ArticleComment>(articleComment);
            var article = await _articleRepository.FirstOrDefaultAsync(_articleComment.ArticleId);
            if (article == null)
                throw new UserFriendlyException(404, "找不到文章信息");
            if (_articleComment.ParArticleCommentId.HasValue)
            {
                var _comment = await _articleCommentRepository.FirstOrDefaultAsync(_articleComment.ParArticleCommentId.Value);
                //只开放二级评论
                var flag = await _articleCommentRepository.GetAll().AnyAsync(p => p.Id == _comment.ParArticleCommentId && p.ParArticleCommentId.HasValue);
                if (flag)
                    throw new UserFriendlyException(500, "目前只开放二级评论");
                _articleComment.ReplyNickName = _comment.NickName;
                _articleComment.ParArticleCommentId = _comment.ParArticleCommentId ?? _articleComment.ParArticleCommentId;
            }
            else
            {
                //如何是评论，楼层数加1，并加上楼层
                article.FloorNum++;
                _articleComment.Floor = article.FloorNum;
            }
            //如果是管理无需审核
            if (await _permissionChecker.IsGrantedAsync(PermissionNames.Pages_Blogs_Admin))
                _articleComment.ToExamine = ToExamine.Adopt;
            //获取评论的IP
            _articleComment.Ip = _clientInfoProvider.ClientIpAddress;
            await _articleCommentRepository.InsertAsync(_articleComment);
            article.CommentNum++;
            await _articleRepository.UpdateAsync(article);
            await _cacheManager.GetCache(BlogCacheNames.CacheArticleComment)
                .SetAsync(_clientInfoProvider.ClientIpAddress, DateTime.Now.ToString());
        }

        [AbpAuthorize(PermissionNames.Pages_Blogs_Admin)]
        public async Task ArticleCommentToExamineAsync(long commentId, ToExamine toExamine)
        {
            var comment = await _articleCommentRepository.FirstOrDefaultAsync(commentId);
            if (toExamine == ToExamine.Unaudited || comment.ToExamine == toExamine)
                throw new UserFriendlyException(500, "审核状态有问题");
            comment.ToExamine = toExamine;
            await _articleCommentRepository.UpdateAsync(comment);
        }

        [AbpAuthorize(PermissionNames.Pages_Blogs)]
        public async Task DeleteArticleCommentAsync(long articleCommentId)
        {
            var articleComment = await _articleCommentRepository.FirstOrDefaultAsync(articleCommentId);
            //只有管理才有权删除他人评论
            if (articleComment.CreatorUserId != AbpSession.UserId && await _permissionChecker.IsGrantedAsync(PermissionNames.Pages_Blogs_Admin))
                throw new UserFriendlyException(403, "不能删除别人的评论");
            await _articleCommentRepository.DeleteAsync(articleComment);
        }

        [AbpAuthorize(PermissionNames.Pages_Blogs)]
        public async Task ArticleTypeCreateOrUpdateAsync(ArticleTypeInput articleType)
        {
            ArticleType _articleType = null;
            if (articleType.Id.HasValue)
            {
                var articleType_ = await _articleTypeRepository.FirstOrDefaultAsync(articleType.Id.Value);
                if (articleType_ == null)
                    throw new UserFriendlyException(404, "该类型不存在");
                _articleType = ObjectMapper.Map(articleType, articleType_);
            }
            else
                _articleType = ObjectMapper.Map<ArticleType>(articleType);
            await _articleTypeRepository.InsertOrUpdateAsync(_articleType);
        }

        public async Task<ListResultDto<ArticleTypeOut>> GetArticleTypesAsync()
        {
            var _types = await _cacheManager
                  .GetCache(BlogCacheNames.CacheBlog)
                  .GetAsync(BlogCacheNames.CacheArticleType, async () =>
                   {
                       //获取所有类型
                       var types = await _articleTypeRepository
                              .GetAll()
                              .OrderBy(p => p.Order)
                              .ToListAsync();
                       return new ListResultDto<ArticleTypeOut>(ObjectMapper.Map<List<ArticleTypeOut>>(types));
                   });
            return _types;
        }

        public async Task<ListResultDto<ArticleTypeOut>> GetArticleTypeMenusAsync()
        {
            var types = (await GetArticleTypesAsync()).Items
                .Where(p => p.IsMenu)
                .OrderBy(p => p.Order)
                .ToList();
            return new ListResultDto<ArticleTypeOut>(types);
        }

        public async Task<ArticleTypeOut> GetArticleTypeByIdAsync(int id)
        {
            var type = (await GetArticleTypesAsync()).Items.FirstOrDefault(p => p.Id == id);
            if (type == null)
                return null;
            return type;
        }

        [AbpAuthorize(PermissionNames.Pages_Blogs_Admin)]
        public async Task DeleteArticleTypeAsync(int articleTypeId)
        {
            await _articleTypeRepository.DeleteAsync(articleTypeId);
        }

        public async Task<ArticleTagOut> GetArticleTagByIdAsync(int id)
        {
            var tag = await _articleTagRepository.FirstOrDefaultAsync(id);
            if (tag == null)
                return null;
            return ObjectMapper.Map<ArticleTagOut>(tag);
        }

        public async Task<ListResultDto<ArticleTagOut>> GetArticleTagCloudAsync()
        {
            //从缓存中获取投票详情
            var _tags = await _cacheManager
                .GetCache(BlogCacheNames.CacheBlog)
                .GetAsync(BlogCacheNames.CacheArticleTag, async () =>
                {
                    var tags = await _articleTagRepository
                        .GetAll()
                        .GroupBy(p => p.Name)
                        .Select(p => new ArticleTagOut()
                        {
                            Name = p.Key,
                            Count = p.Count()
                        })
                        .OrderByDescending(p => p.Count)
                        .ToListAsync();
                    return new ListResultDto<ArticleTagOut>(tags);
                });
            return _tags;
        }

        public async Task<ArticleTagOut> GetArticleTagByNameAsync(string name)
        {
            var tag = await _articleTagRepository.FirstOrDefaultAsync(p => p.Name == name);
            if (tag == null)
                return null;
            return ObjectMapper.Map<ArticleTagOut>(tag);
        }

        /// <summary>
        /// 添加建议
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task CreateProposalAsync(CreateProposalInput input)
        {
            var _default = await _cacheManager.GetCache(BlogCacheNames.CacheBlogProposal)
                 .GetOrDefaultAsync($"{_clientInfoProvider.ClientIpAddress}");
            if (_default != null)
                throw new UserFriendlyException(401, "请等待十分钟后再投递");
            var proposal = ObjectMapper.Map<BlogProposal>(input);
            proposal.Ip = _clientInfoProvider.ClientIpAddress;
            await _blogProposalRepository.InsertAsync(proposal);
            await _cacheManager.GetCache(BlogCacheNames.CacheBlogProposal)
                .SetAsync(_clientInfoProvider.ClientIpAddress, DateTime.Now.ToString());
        }

        /// <summary>
        /// 获取建议
        /// </summary>
        /// <param name="index"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [AbpAuthorize(PermissionNames.Pages_Blogs_Admin)]
        public async Task<PagedResultDto<ProposalOut>> GetProposalAsync(int index = 1, int pageSize = 10)
        {
            var proposals = await _blogProposalRepository
                   .GetAll()
                   .OrderByDescending(p => p.CreationTime)
                   .Page(index, pageSize)
                   .ToListAsync();
            int count = await _blogProposalRepository
                   .CountAsync();
            return new PagedResultDto<ProposalOut>(count, ObjectMapper.Map<List<ProposalOut>>(proposals));
        }

        /// <summary>
        /// 删除建议
        /// </summary>
        /// <param name="proposalId"></param>
        /// <returns></returns>
        [AbpAuthorize(PermissionNames.Pages_Blogs_Admin)]
        public async Task DeleteProposalAsync(int proposalId)
        {
            await _blogProposalRepository.DeleteAsync(proposalId);
        }


        /// <summary>
        /// 创建或者修改友情链接
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(PermissionNames.Pages_Sites)]
        public async Task FriendshipLinkCreateOrUpdate(FriendshipLinkCreateOrUpdate input)
        {
            FriendshipLink link = null;
            if (input.Id.HasValue)
            {
                var _link = await _blogFriendshipLinkRepository.FirstOrDefaultAsync(input.Id.Value);
                if (_link == null)
                    throw new UserFriendlyException(404, "该友情链接不存在");
                link = ObjectMapper.Map(input, _link);
            }
            else
            {
                link = ObjectMapper.Map<FriendshipLink>(input);
                //此接口为管理员接口，添加默认审核通过，修改不更改审核状态
                link.ToExamine = ToExamine.Adopt;
                link.Ip = _clientInfoProvider.ClientIpAddress;
            }
            await _blogFriendshipLinkRepository.InsertOrUpdateAsync(link);
        }

        /// <summary>
        /// 申请友情链接
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task FriendshipLinkApply(FriendshipLinkCreateOrUpdate input)
        {
            var _default = await _cacheManager.GetCache(BlogCacheNames.CacheFriendshipLinkApply)
                 .GetOrDefaultAsync($"{_clientInfoProvider.ClientIpAddress}");
            if (_default != null)
                throw new UserFriendlyException(401, "请等待十分钟后再申请");
            input.Id = null;
            FriendshipLink link = ObjectMapper.Map<FriendshipLink>(input);
            link.Ip = _clientInfoProvider.ClientIpAddress;
            link.Order = 999;
            await _blogFriendshipLinkRepository.InsertAsync(link);
            await _cacheManager.GetCache(BlogCacheNames.CacheFriendshipLinkApply)
                .SetAsync(_clientInfoProvider.ClientIpAddress, DateTime.Now.ToString());
        }

        /// <summary>
        /// 审核友链
        /// </summary>
        /// <param name="linkId"></param>
        /// <param name="toExamine"></param>
        /// <returns></returns>
        public async Task FriendshipLinkToExamineAsync(int linkId, ToExamine toExamine)
        {
            var link = await _blogFriendshipLinkRepository.FirstOrDefaultAsync(linkId);
            if (toExamine == ToExamine.Unaudited || link.ToExamine == toExamine)
                throw new UserFriendlyException(500, "审核状态有问题");
            link.ToExamine = toExamine;
            await _blogFriendshipLinkRepository.UpdateAsync(link);
        }

        /// <summary>
        /// 获取友情链接
        /// </summary>
        /// <param name="index"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<ListResultDto<FriendshipLinkOut>> GetFriendshipLinksAsync()
        {
            var links = await _blogFriendshipLinkRepository
                  .GetAll()
                  .OrderBy(p => p.Order)
                  .ToListAsync();
            return new ListResultDto<FriendshipLinkOut>(ObjectMapper.Map<List<FriendshipLinkOut>>(links));
        }

        /// <summary>
        /// 获取友情链接
        /// </summary>
        /// <param name="linkId"></param>
        /// <returns></returns>
        public async Task<FriendshipLinkOut> GetFriendshipLinkByIdAsync(int linkId)
        {
            var link = await _blogFriendshipLinkRepository.FirstOrDefaultAsync(linkId);
            if (link == null)
                throw new UserFriendlyException(404, "该友情链接不存在");
            return ObjectMapper.Map<FriendshipLinkOut>(link);
        }

        /// <summary>
        /// 删除友链
        /// </summary>
        /// <param name="linkId"></param>
        /// <returns></returns>
        [AbpAuthorize(PermissionNames.Pages_Sites)]
        public async Task DeleteFriendshipLinkAsync(int linkId)
        {
            await _blogFriendshipLinkRepository.DeleteAsync(linkId);
        }

        /// <summary>
        /// 修改站点配置
        /// </summary>
        /// <param name="blogSite"></param>
        /// <returns></returns>
        [AbpAuthorize(PermissionNames.Pages_Sites)]
        public async Task UpdateSiteConfig(BlogSiteConfig blogSite)
        {
            await JsonConfig<BlogSiteConfig>.CreateOrUpdateSiteConfig(blogSite);
        }

        /// <summary>
        /// 获取审核通知
        /// </summary>
        /// <returns></returns>
        [AbpAuthorize(PermissionNames.Pages_Blogs_Admin)]
        public async Task<List<(string, string)>> GetAuditNotice()
        {
            var _default = await _cacheManager.GetCache(BlogCacheNames.CacheBlogAuditNotice)
                    .GetOrDefaultAsync(_clientInfoProvider.ClientIpAddress);
            if (_default != null)
                return null;
            List<(string, string)> list = new List<(string, string)>();
            int commentCount = await _articleCommentRepository.CountAsync(p => p.ToExamine == ToExamine.Unaudited);
            if (commentCount > 0)
                list.Add(("评论通知", $"目前还有{commentCount}条评论暂未审核"));
            int linkCount = await _blogFriendshipLinkRepository.CountAsync(p => p.ToExamine == ToExamine.Unaudited);
            if (linkCount > 0)
                list.Add(("友链通知", $"目前还有{linkCount}条友链暂未审核"));
            await _cacheManager.GetCache(BlogCacheNames.CacheBlogAuditNotice).SetAsync(_clientInfoProvider.ClientIpAddress, DateTime.Now.ToString());
            return list;
        }

        /// <summary>
        /// 清理博客全部缓存
        /// </summary>
        /// <returns></returns>
        [AbpAuthorize(PermissionNames.Pages_Sites)]
        public async Task ClearCache()
        {
            await _cacheManager.GetCache(BlogCacheNames.CacheBlog).ClearAsync();
            await _cacheManager.GetCache(BlogCacheNames.CacheArticleRead).ClearAsync();
            await _cacheManager.GetCache(BlogCacheNames.CacheArticleComment).ClearAsync();
            await _cacheManager.GetCache(BlogCacheNames.CacheFriendshipLink).ClearAsync();
            await _cacheManager.GetCache(BlogCacheNames.CacheBlogProposal).ClearAsync();
            await _cacheManager.GetCache(BlogCacheNames.CacheBlogAuditNotice).ClearAsync();
            await _cacheManager.GetCache(Sitemaps.Cache.SitemapCacheNames.CacheSitemap).ClearAsync();
        }
    }
}
