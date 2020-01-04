using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CC.Blog.Blogs.DTO
{
    [AutoMapTo(typeof(Article))]
    public class ArticleCreateOrUpdate
    {
        /// <summary>
        /// 文章ID
        /// </summary>
        public long? Id { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        /// <summary>
        /// 封面图
        /// </summary>
        [Required]
        [MaxLength(256)]
        public string CoverMap { get; set; }

        /// <summary>
        /// 简介
        /// </summary>
        [Required]
        [MaxLength(500)]
        public string Desc { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [Required]
        [MaxLength(500000)]
        public string Html { get; set; }

        /// <summary>
        /// 文章类型ID
        /// </summary>
        [Required]
        public int ArticleTypeId { get; set; }

        ///// <summary>
        ///// 推荐数
        ///// </summary>
        //public int Recommend { get; set; }

        ///// <summary>
        ///// 反对数
        ///// </summary>
        //public int Opposition { get; set; }

        ///// <summary>
        ///// 阅读量
        ///// </summary>
        //public int ReadingNum { get; set; }

        /// <summary>
        /// 隐藏内容
        /// </summary>
        public string HideBody { get; set; }

        /// <summary>
        /// 标签集合
        /// </summary>
        public ICollection<string> ArticleStrTags { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [Required]
        public bool IsEnable { get; set; }
    }
}
