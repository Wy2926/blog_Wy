using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CC.Blog.Blogs.DTO
{
    [AutoMapTo(typeof(ArticleComment))]
    public class ArticleCommentInput
    {
        /// <summary>
        /// 文章ID
        /// </summary>
        [Required]
        public long ArticleId { get; set; }

        /// <summary>
        /// 父评论ID
        /// </summary>
        public long? ParArticleCommentId { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        [Required]
        [MaxLength(30)]
        public string NickName { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [MaxLength(200)]
        public string Email { get; set; }

        /// <summary>
        /// 评论内容
        /// </summary>
        [Required]
        [MaxLength(1000)]
        public string Body { get; set; }

        /// <summary>
        /// 网站
        /// </summary>
        [MaxLength(1000)]
        public string Site { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        [Required]
        public string Code { get; set; }
    }
}
