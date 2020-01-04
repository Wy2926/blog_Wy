using CC.Blog.Blogs.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CC.Blog.Web.Models.Blog
{
    public class ArticleListViewModel
    {
        public IReadOnlyList<ArticleOut> Articles { get; set; }
        public IReadOnlyList<ArticleTypeOut> ArticleTypes { get; set; }
    }
}
