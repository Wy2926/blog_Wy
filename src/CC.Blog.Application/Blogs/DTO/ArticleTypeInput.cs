using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CC.Blog.Blogs.DTO
{
    [AutoMapTo(typeof(ArticleType))]
    public class ArticleTypeInput
    {
        /// <summary>
        /// 类型ID
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// 父类型ID
        /// </summary>
        public int? ParArticleTypeId { get; set; }

        /// <summary>
        /// 类型名称
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 是否显示在菜单上
        /// </summary>
        [Required]
        public bool IsMenu { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
        [Required]
        public int Order { get; set; }
    }
}
