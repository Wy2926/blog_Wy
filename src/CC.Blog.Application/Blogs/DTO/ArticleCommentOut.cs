using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CC.Blog.Blogs.DTO
{
    
    [AutoMapFrom(typeof(ArticleComment))]
    public class ArticleCommentOut
    {
        /// <summary>
        /// ID
        /// </summary>
        [Required]
        public long Id { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public long? UserId { get; set; }

        /// <summary>
        /// 被回复用户的名称
        /// </summary>
        public string ReplyNickName { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        [Required]
        public string NickName { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// 评论内容
        /// </summary>
        [Required]
        public string Body { get; set; }

        /// <summary>
        /// 网站
        /// </summary>
        [MaxLength(1000)]
        public string Site { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        [Required]
        public string Ip { get; set; }

        /// <summary>
        /// 子评论
        /// </summary>
        [Required]
        public List<ArticleCommentOut> SubComments { get; set; }

        /// <summary>
        /// 评论时间
        /// </summary>
        [Required]
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 评论状态
        /// </summary>
        public ToExamine ToExamine { get; set; }
    }
}
