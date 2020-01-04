using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Controllers;
using Abp.Collections.Extensions;
using CC.Blog.Authorization;
using CC.Blog.Blogs;
using CC.Blog.Blogs.DTO;
using CC.Blog.Controllers;
using CC.Blog.Web.Filters;
using CC.Helper.Expand;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CC.Blog.Web.Controllers
{
    [ServiceFilter(typeof(SpiderActionFilterAttribute))]
    public class ArticleController : BlogControllerBase
    {
        public readonly IBlogAppService _blogAppService;
        public BlogSiteConfig blogSite { get; set; } = Helper.JsonConfig<BlogSiteConfig>.GetSiteConfig().Clone() as BlogSiteConfig;

        public ArticleController(IBlogAppService blogAppService)
        {
            _blogAppService = blogAppService;
        }

        /// <summary>
        /// 首页
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 10;
            ViewBag.Articles = await _blogAppService.GetArticlesByTypeIdAsync(null, page, pageSize);
            ViewBag.Page = page;
            int maxCount = ViewBag.Articles.TotalCount / pageSize;
            if (ViewBag.Articles.TotalCount % pageSize > 0)
                maxCount++;
            ViewBag.MaxPage = maxCount;
            return await ViewAsync("Articles", true);
        }

        /// <summary>
        /// 搜索列表
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[controller]/[action]/{key?}")]
        public async Task<IActionResult> Search(string key, int page = 1)
        {
            int pageSize = 10;
            if (!string.IsNullOrEmpty(key))
            {
                blogSite.Title = $"{key}-关键字搜索-{blogSite.SiteName}";
                ViewBag.Articles = await _blogAppService.SearchArticlesAsync(key, page, pageSize);
            }
            else
            {
                ViewBag.Articles = await _blogAppService.GetArticlesByTypeIdAsync(null, page, pageSize);
            }
            ViewBag.Page = page;
            int maxCount = ViewBag.Articles.TotalCount / pageSize;
            if (ViewBag.Articles.TotalCount % pageSize > 0)
                maxCount++;
            ViewBag.MaxPage = maxCount;
            return await ViewAsync("Articles");
        }

        /// <summary>
        /// 文章分类页面
        /// </summary>
        /// <param name="id"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[controller]/[action]_{id}.html")]
        public async Task<IActionResult> Type(int id, int page = 1)
        {
            //获取类型
            var type = await _blogAppService.GetArticleTypeByIdAsync(id);
            //如果没有分类信息就不需要查询文章
            if (type != null)
            {
                //传递类型ID
                ViewBag.ArticleTypeId = type.Id;
                //获取文章详情
                int pageSize = 10;
                ViewBag.Articles = await _blogAppService.GetArticlesByTypeIdAsync(id, page, pageSize);
                ViewBag.Page = page;
                int maxCount = ViewBag.Articles.TotalCount / pageSize;
                if (ViewBag.Articles.TotalCount % pageSize > 0)
                    maxCount++;
                ViewBag.MaxPage = maxCount;
                blogSite.Title = $"{type.Name}-文章分类-{blogSite.SiteName}";
            }
            ViewBag.MessBox = "暂无此分类信息";
            return await ViewAsync("Articles");
        }

        /// <summary>
        /// 文章标签页面
        /// </summary>
        /// <param name="tagName"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[controller]/[action]_{tagName}.html")]
        public async Task<IActionResult> Tag(string tagName, int page = 1)
        {
            //获取标签
            var tag = await _blogAppService.GetArticleTagByNameAsync(tagName);
            //如果没有标签信息就不需要查询文章
            if (tag != null)
            {
                int pageSize = 10;
                ViewBag.Articles = await _blogAppService.GetArticlesByTagNameAsync(tag.Name, page, pageSize);
                ViewBag.Page = page;
                int maxCount = ViewBag.Articles.TotalCount / pageSize;
                if (ViewBag.Articles.TotalCount % pageSize > 0)
                    maxCount++;
                ViewBag.MaxPage = maxCount;
            }
            blogSite.Title = $"{tagName}-文章标签-{blogSite.SiteName}";
            ViewBag.MessBox = "暂无此标签信息";
            return await ViewAsync("Articles");
        }

        /// <summary>
        /// 文章详情页面
        /// </summary>
        /// <param name="id"></param>
        /// <param name="typeId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[controller]/[action]_{id}.html")]
        public async Task<IActionResult> Details(long id, int? typeId)
        {
            var article = await _blogAppService.GetArticleDetailAsync(id, true);
            ViewBag.Article = article;
            if (ViewBag.Article != null)
            {
                (ArticleOut up, ArticleOut down) = await _blogAppService.GetArticlesUpDownByIdAsync(id, typeId);
                ViewBag.ArticleTypeId = typeId;
                ViewBag.UpArticle = up;
                ViewBag.DownArticle = down;
                ViewBag.Comments = (await _blogAppService.GetArticleCommentByIdAsync(id)).Items;
                //站点信息
                blogSite.Title = $"{article.Title}-{blogSite.SiteName}";
                blogSite.KeyWords = string.Join(",", article.ArticleStrTags);
                blogSite.Desc = article.Desc;
            }
            ViewBag.ArticleRecommend = await _blogAppService.GetArticlesRecommendByTypeIdAsync(typeId);
            return await ViewAsync();
        }

        /// <summary>
        /// 返回母版视图
        /// </summary>
        /// <param name="name">视图名称</param>
        /// <param name="flag">是否是首页（默认不是）</param>
        /// <returns></returns>
        private async Task<IActionResult> ViewAsync(string name = null, bool flag = false)
        {
            //获取热门文章
            ViewBag.Recommend = await _blogAppService.GetArticlesRecommendByTypeIdAsync(pageSize: 5);
            //获取类型菜单
            ViewBag.Menu = (await _blogAppService.GetArticleTypeMenusAsync()).Items;
            //获取标签云
            ViewBag.Tags = (await _blogAppService.GetArticleTagCloudAsync()).Items;
            //获取友情链接
            ViewBag.Links = (await _blogAppService.GetFriendshipLinksAsync())
                .Items
                .Where(p => p.ToExamine == ToExamine.Adopt)
                .WhereIf(!flag, p => p.LinkType == LinkType.TotalStation)
                .ToList();
            //.Where(p => p.OfflineTime == null || p.OfflineTime > DateTime.Now)
            //.ToList();
            //获取审核通知(必须是管理员才行)
            if (await IsGrantedAsync(PermissionNames.Pages_Blogs_Admin))
            {
                ViewBag.AuditNotice = await _blogAppService.GetAuditNotice();
            }
            ViewBag.BlogConfig = blogSite;
            if (name == null)
                return View();
            return View(name);
        }
    }
}