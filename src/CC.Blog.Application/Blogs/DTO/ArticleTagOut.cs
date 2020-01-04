using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CC.Blog.Blogs.DTO
{
    [AutoMapFrom(typeof(ArticleTag))]
    public class ArticleTagOut
    {
        /// <summary>
        /// 标签ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 标签名称
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 标签的数量
        /// </summary>
        [Required]
        public int Count { get; set; }
    }
}
