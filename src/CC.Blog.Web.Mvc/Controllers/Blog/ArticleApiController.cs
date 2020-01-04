using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Controllers;
using Abp.UI;
using CC.Blog.Blogs;
using CC.Blog.Blogs.DTO;
using CC.Blog.Controllers;
using CC.Blog.Web.Host.Areas.Blog.Data;
using CC.Helper;
using CC.Helper.Expand;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CC.Blog.Web.Host.Areas.Blog.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ArticleApiController : BlogControllerBase
    {

        public readonly IBlogAppService _blogAppService;

        public ArticleApiController(IBlogAppService blogAppService)
        {
            _blogAppService = blogAppService;
        }

        /// <summary>
        /// 评论
        /// </summary>
        /// <param name="comment"></param>
        /// <param name="code"></param>
        [HttpPost]
        public async Task Comment([FromBody] ArticleCommentInput comment)
        {
            //获取验证码
            string _code = HttpContext.Session.GetString(StringSession.ArticleCommentKey);
            if (string.IsNullOrEmpty(_code) || _code != comment.Code.ToLower())
                throw new UserFriendlyException(403, "验证码有误");
            await _blogAppService.CreateArticleCommentAsync(comment);
            HttpContext.Session.Remove(StringSession.ArticleCommentKey);
        }

        /// <summary>
        /// 混合验证码
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public FileContentResult MixVerifyCode()
        {
            string code = StringRandom.NumOrLetter();
            HttpContext.Session.SetString(StringSession.ArticleCommentKey, code.ToLower());
            return File(code.CreateCheckCodeImage(), "image/gif");
        }


        /// <summary>
        /// 提交建议
        /// </summary>
        /// <param name="input"></param>
        [HttpPost]
        public async Task Proposal([FromBody] CreateProposalInput input)
        {
            await _blogAppService.CreateProposalAsync(input);
        }
    }
}
