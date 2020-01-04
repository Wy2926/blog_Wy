using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Dependency;
using CC.Blog.Blogs.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CC.Blog.Blogs
{
    /// <summary>
    /// 博客应用层接口
    /// </summary>
    public interface IBlogAppService : IApplicationService, ITransientDependency
    {
        /// <summary>
        /// 创建或者修改文章
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        Task ArticleCreateOrUpdateAsync(ArticleCreateOrUpdate article);

        /// <summary>
        /// 删除文章
        /// </summary>
        /// <param name="articleId">文章ID</param>
        /// <returns></returns>
        Task DeleteArticleAsync(long articleId);

        /// <summary>
        /// 搜索文章
        /// </summary>
        /// <param name="keyWord">关键词</param>
        /// <param name="index"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<PagedResultDto<ArticleOut>> SearchArticlesAsync(string keyWord, int index = 1, int pageSize = 10);

        /// <summary>
        /// 根据类型获取文章列表
        /// </summary>
        /// <param name="typeId">类型ID</param>
        /// <param name="index">第index页</param>
        /// <param name="pageSize">一页pageSize篇文章</param>
        /// <returns></returns>
        Task<PagedResultDto<ArticleOut>> GetArticlesByTypeIdAsync(int? typeId, int index = 1, int pageSize = 10);

        /// <summary>
        /// 根据标签获取文章列表
        /// </summary>
        /// <param name="typeId">标签名称</param>
        /// <param name="index">第index页</param>
        /// <param name="pageSize">一页pageSize篇文章</param>
        /// <returns></returns>
        Task<PagedResultDto<ArticleOut>> GetArticlesByTagNameAsync(string tagName, int index = 1, int pageSize = 10);

        /// <summary>
        /// 获取根据文章（可根据类型）【根据发布时间在两个月内的文章阅读量排行】
        /// </summary>
        /// <param name="typeId"></param>
        /// <param name="index"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<ICollection<ArticleOut>> GetArticlesRecommendByTypeIdAsync(int? typeId = null, int index = 1, int pageSize = 10);

        /// <summary>
        /// 根据文章ID查询上一篇下一盘(可加分类限制)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="typeId"></param>
        /// <returns></returns>
        Task<(ArticleOut up, ArticleOut down)> GetArticlesUpDownByIdAsync(long id, int? typeId);

        /// <summary>
        /// 获取指定文章详情
        /// </summary>
        /// <param name="articleId"></param>
        /// <param name="flag">是否增加浏览量</param>
        /// <returns></returns>
        Task<ArticleDetailsOut> GetArticleDetailAsync(long articleId, bool flag = false);

        /// <summary>
        /// 获取指定文章的评论
        /// </summary>
        /// <param name="articleId"></param>
        /// <param name="index"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<PagedResultDto<ArticleCommentOut>> GetArticleCommentByIdAsync(long articleId, int index = 1, int pageSize = 10);

        /// <summary>
        /// 获取最新的文章评论
        /// </summary>
        /// <param name="index"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<PagedResultDto<ArticleCommentOut>> GetArticleCommentsAsync(int index = 1, int pageSize = 10);

        /// <summary>
        /// 评论条件查询（后台API）
        /// </summary>
        /// <param name="selectDto"></param>
        /// <returns></returns>
        Task<PagedResultDto<ArticleCommentOut>> GetAdminArticleCommentsAsync(CommentSelectDto selectDto);

        /// <summary>
        /// 创建评论
        /// </summary>
        /// <param name="articleComment"></param>
        /// <returns></returns>
        Task CreateArticleCommentAsync(ArticleCommentInput articleComment);

        /// <summary>
        /// 审核评论
        /// </summary>
        /// <param name="commentId"></param>
        /// <param name="toExamine"></param>
        /// <returns></returns>
        Task ArticleCommentToExamineAsync(long commentId, ToExamine toExamine);

        /// <summary>
        /// 删除评论
        /// </summary>
        /// <param name="articleCommentId"></param>
        /// <returns></returns>
        Task DeleteArticleCommentAsync(long articleCommentId);

        /// <summary>
        /// 创建或者修改类型
        /// </summary>
        /// <param name="articleComment"></param>
        /// <returns></returns>
        Task ArticleTypeCreateOrUpdateAsync(ArticleTypeInput articleType);

        /// <summary>
        /// 获取文章类型
        /// </summary>
        /// <returns></returns>
        Task<ListResultDto<ArticleTypeOut>> GetArticleTypesAsync();

        /// <summary>
        /// 获取菜单类型
        /// </summary>
        /// <returns></returns>
        Task<ListResultDto<ArticleTypeOut>> GetArticleTypeMenusAsync();

        /// <summary>
        /// 获取指定类型
        /// </summary>
        /// <returns></returns>
        Task<ArticleTypeOut> GetArticleTypeByIdAsync(int id);

        /// <summary>
        /// 删除类型
        /// </summary>
        /// <param name="articleTypeId"></param>
        /// <returns></returns>
        Task DeleteArticleTypeAsync(int articleTypeId);

        /// <summary>
        /// 获取指定标签
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ArticleTagOut> GetArticleTagByIdAsync(int id);

        /// <summary>
        /// 获取标签云
        /// </summary>
        /// <param name="typeId"></param>
        /// <returns></returns>
        Task<ListResultDto<ArticleTagOut>> GetArticleTagCloudAsync();

        /// <summary>
        /// 获取指定标签
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<ArticleTagOut> GetArticleTagByNameAsync(string name);

        /// <summary>
        /// 添加建议
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateProposalAsync(CreateProposalInput input);

        /// <summary>
        /// 获取建议
        /// </summary>
        /// <param name="index"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<PagedResultDto<ProposalOut>> GetProposalAsync(int index = 1, int pageSize = 10);

        /// <summary>
        /// 删除建议
        /// </summary>
        /// <param name="proposalId"></param>
        /// <returns></returns>
        Task DeleteProposalAsync(int proposalId);

        /// <summary>
        /// 创建或者修改友情链接
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task FriendshipLinkCreateOrUpdate(FriendshipLinkCreateOrUpdate input);

        /// <summary>
        /// 申请友情链接
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task FriendshipLinkApply(FriendshipLinkCreateOrUpdate input);

        /// <summary>
        /// 审核友链
        /// </summary>
        /// <param name="linkId"></param>
        /// <param name="toExamine"></param>
        /// <returns></returns>
        Task FriendshipLinkToExamineAsync(int linkId, ToExamine toExamine);

        /// <summary>
        /// 获取友情链接
        /// </summary>
        /// <param name="index"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<ListResultDto<FriendshipLinkOut>> GetFriendshipLinksAsync();

        /// <summary>
        /// 获取友情链接
        /// </summary>
        /// <param name="linkId"></param>
        /// <returns></returns>
        Task<FriendshipLinkOut> GetFriendshipLinkByIdAsync(int linkId);

        /// <summary>
        /// 删除建议
        /// </summary>
        /// <param name="proposalId"></param>
        /// <returns></returns>
        Task DeleteFriendshipLinkAsync(int linkId);

        /// <summary>
        /// 修改站点配置
        /// </summary>
        /// <param name="blogSite"></param>
        /// <returns></returns>
        Task UpdateSiteConfig(BlogSiteConfig blogSite);

        /// <summary>
        /// 获取审核通知
        /// </summary>
        /// <returns></returns>
        Task<List<(string, string)>> GetAuditNotice();

        /// <summary>
        /// 清理全部缓存
        /// </summary>
        /// <returns></returns>
        Task ClearCache();
    }
}
