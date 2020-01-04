using CC.Blog.Blogs.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CC.Blog.Web.Models.Blog
{
    public class EditArticleModalViewModel
    {
        public ArticleDetailsOut Article { get; set; }
        public IReadOnlyList<ArticleTypeOut> ArticleTypes { get; set; }
    }
}
