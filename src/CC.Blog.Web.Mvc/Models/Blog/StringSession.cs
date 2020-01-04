using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CC.Blog.Web.Host.Areas.Blog.Data
{
    public static class StringSession
    {
        /// <summary>
        /// 评论时验证码在Session里的名称
        /// </summary>
        public static readonly string ArticleCommentKey = "ArticleCommentKey";
    }
}
