using Abp.AutoMapper;
using CC.Blog.Users.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CC.Blog.Blogs.DTO
{
    [AutoMapFrom(typeof(Article))]
    public class ArticleOut
    {
        /// <summary>
        /// 文章ID
        /// </summary>
        [Required]
        public long Id { get; set; }

        /// <summary>
        /// 用户信息
        /// </summary>
        [Required]
        public ArticleUserDto User { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [Required]
        [MaxLength(30)]
        public string Title { get; set; }

        /// <summary>
        /// 封面图
        /// </summary>
        [Required]
        public string CoverMap { get; set; }

        /// <summary>
        /// 简介
        /// </summary>
        [Required]
        [MaxLength(500)]
        public string Desc { get; set; }

        /// <summary>
        /// 文章类型
        /// </summary>
        [Required]
        [MaxLength(30)]
        public string Type { get; set; }

        /// <summary>
        /// 文章类型Id
        /// </summary>
        [Required]
        public string TypeId { get; set; }

        /// <summary>
        /// 推荐数
        /// </summary>
        [Required]
        public string Recommend { get; set; }

        /// <summary>
        /// 反对数
        /// </summary>
        [Required]
        public string Opposition { get; set; }

        /// <summary>
        /// 阅读量
        /// </summary>
        [Required]
        public int ReadingNum { get; set; }

        /// <summary>
        /// 评论数
        /// </summary>
        [Required]
        public int CommentNum { get; set; }

        /// <summary>
        /// 标签集合
        /// </summary>
        public List<string> ArticleStrTags { get; set; }

        /// <summary>
        /// 文章发布时间
        /// </summary>
        [Required]
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [Required]
        public bool IsEnable { get; set; }
    }
}
