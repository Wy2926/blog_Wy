using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.Web.Models;
using CC.Blog.Blogs;
using CC.Blog.Blogs.DTO;
using CC.Blog.Controllers;
using CC.Blog.Web.Models.Blog;
using CC.Blog.Web.Models.Roles;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CC.Blog.Web.Mvc.Controllers.Blog
{
    [AbpMvcAuthorize]
    public class BlogController : BlogControllerBase
    {
        public readonly IBlogAppService _blogAppService;
        private IHostingEnvironment _host = null;
        public BlogSiteConfig blogSite { get; set; } = Helper.JsonConfig<BlogSiteConfig>.GetSiteConfig() as BlogSiteConfig;

        public BlogController(IBlogAppService blogAppService
            , IHostingEnvironment host)
        {
            _blogAppService = blogAppService;
            _host = host;
        }

        public async Task<ActionResult> Index(int page = 1, int pageSize = 10)
        {
            //文章列表
            var articles = await _blogAppService.GetArticlesByTypeIdAsync(null, page);
            //所有类型
            var types = (await _blogAppService.GetArticleTypesAsync()).Items;
            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalCount = articles.TotalCount;

            return View(new ArticleListViewModel() { Articles = articles.Items, ArticleTypes = types });
        }

        public async Task<ActionResult> Type()
        {
            var types = (await _blogAppService.GetArticleTypesAsync()).Items;
            return View(types);
        }

        public async Task<ActionResult> Comment(CommentSelectDto selectDto)
        {
            var comments = await _blogAppService.GetAdminArticleCommentsAsync(selectDto);
            ViewBag.Page = selectDto.Page;
            ViewBag.PageSize = selectDto.PageSize;
            ViewBag.TotalCount = comments.TotalCount;
            return View(comments);
        }

        public async Task<ActionResult> Proposal(int index = 1)
        {
            var proposals = (await _blogAppService.GetProposalAsync(index)).Items;
            return View(proposals);
        }

        public async Task<ActionResult> Flike()
        {
            var links = (await _blogAppService.GetFriendshipLinksAsync()).Items;
            return View(links);
        }

        public ActionResult Config()
        {
            return View(blogSite);
        }

        public async Task<ActionResult> EditArticleModal(long articleId)
        {
            var article = await _blogAppService.GetArticleDetailAsync(articleId);
            var types = (await _blogAppService.GetArticleTypesAsync()).Items;

            return View("_EditArticleModal", new EditArticleModalViewModel() { Article = article, ArticleTypes = types });
        }

        public async Task<ActionResult> EditArticleTypeModal(int articleTypeId)
        {
            var articleType = await _blogAppService.GetArticleTypeByIdAsync(articleTypeId);
            return View("_EditArticleTypeModal", articleType);
        }

        public async Task<ActionResult> EditFriendshipLinkModal(int linkId)
        {
            var link = await _blogAppService.GetFriendshipLinkByIdAsync(linkId);
            return View("_EditFriendshipLinkModal", link);
        }


        #region 上传图片 OnPostUpload
        [HttpPost]
        [DontWrapResultAttribute]
        public async Task<IActionResult> OnPostUpload()
        {
            var date = Request;
            var files = Request.Form.Files;
            if (files.Count == 0)
            {
                TmpUrl tmp = new TmpUrl()
                {
                    Errno = 404,
                    Msg = "没有图片，上传什么"
                };
                return new JsonResult(tmp);
            }
            long size = files.Sum(f => f.Length);
            string shortTime = $"/Update/{DateTime.Now.ToString("yyyy/MM/dd")}/";
            string filePhysicalPath = $@"{_host.WebRootPath}/{shortTime}";  //文件路径  可以通过注入 IHostingEnvironment 服务对象来取得Web根目录和内容根目录的物理路径
            if (!Directory.Exists(filePhysicalPath)) //判断上传文件夹是否存在，若不存在，则创建
            {
                Directory.CreateDirectory(filePhysicalPath); //创建文件夹
            }
            List<string> urlLs = new List<string>();
            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    var fileName = System.Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);//文件名+文件后缀名
                    using (var stream = new FileStream(filePhysicalPath + fileName, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    urlLs.Add($"{shortTime}{fileName}");
                }
            }
            return new JsonResult(TmpUrl.SuccessInfo("上传成功", urlLs));
        }
        #endregion

        //#region 上传视频 OnPostUploadVideo
        //[HttpPost]
        //public async Task<IActionResult> OnPostUploadVideo([FromServices]IHostingEnvironment environment)
        //{
        //    List<string> fileUrl = new List<string>();
        //    var files = Request.Form.Files;
        //    if (string.IsNullOrWhiteSpace(environment.WebRootPath))
        //    {
        //        environment.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
        //    }

        //    string webRootPath = environment.WebRootPath;
        //    string filePath = Path.Combine(webRootPath + "\\upload\\videos");
        //    if (!Directory.Exists(filePath))
        //    {
        //        Directory.CreateDirectory(filePath);
        //    }

        //    foreach (var formFile in files)
        //    {
        //        if (formFile.Length > 0)
        //        {
        //            var ext = Path.GetExtension(formFile.FileName);
        //            if (!videoFormatArray.Contains(ext.Split('.')[1]))
        //            {
        //                return new JsonResult(TmpUrl.ErrorInfo("视频格式不正确!", null));
        //            }
        //            var fileName = Guid.NewGuid().ToString() + ext;
        //            var path = Path.Combine(webRootPath + "\\upload\\videos", fileName);
        //            using (var stream = new FileStream(path, FileMode.CreateNew))
        //            {
        //                await formFile.CopyToAsync(stream);
        //                //fileUrl.Add($"/upload/videos/{fileName}");
        //                fileUrl.Add("http://localhost:15429/upload/videos/8e11ae8e-8ecc-4b7c-afac-43601530493f.mp4");
        //            }
        //        }
        //    }

        //    return new JsonResult(TmpUrl.SuccessInfo("ok!", fileUrl));
        //}
        //#endregion

        #region 获取图片流  ShowNoticeImg
        public IActionResult ShowNoticeImg(string filePath)
        {
            var contentTypeStr = "image/jpeg";
            string webRootPath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\upload\\images\\{filePath}");
            using (var sw = new FileStream(webRootPath, FileMode.Open))
            {
                var bytes = new byte[sw.Length];
                sw.Read(bytes, 0, bytes.Length);
                sw.Close();
                return new FileContentResult(bytes, contentTypeStr);
            }
        }
        #endregion
    }
}