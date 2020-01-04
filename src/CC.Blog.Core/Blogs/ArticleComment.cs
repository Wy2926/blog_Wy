using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using CC.Blog.Authorization.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CC.Blog.Blogs
{
    /// <summary>
    /// 文章评论
    /// </summary>
    public class ArticleComment : AggregateRoot<long>, ICreationAudited, IDeletionAudited
    {
        /// <summary>
        /// 文章ID
        /// </summary>
        [Required]
        public long ArticleId { get; set; }

        /// <summary>
        /// 文章
        /// </summary>
        [ForeignKey("ArticleId")]
        public virtual Article Article { get; set; }

        [ForeignKey("CreatorUserId")]
        public virtual User User { get; set; }

        /// <summary>
        /// 父评论ID
        /// </summary>
        public long? ParArticleCommentId { get; set; }

        /// <summary>
        /// 被回复用户的名称
        /// </summary>
        public string ReplyNickName { get; set; }

        /// <summary>
        /// 楼层
        /// </summary>
        public int? Floor { get; set; }

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
        /// IP地址
        /// </summary>
        [Required]
        public string Ip { get; set; }

        /// <summary>
        /// 子评论
        /// </summary>
        [NotMapped]
        public List<ArticleComment> SubComments { get; set; }

        /// <summary>
        /// 评论状态
        /// </summary>
        [Required]
        [DefaultValue((int)ToExamine.Unaudited)]
        public ToExamine ToExamine { get; set; }

        public long? CreatorUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public bool IsDeleted { get; set; }
    }

    public enum ToExamine
    {
        /// <summary>
        /// 未审核
        /// </summary>
        Unaudited,
        /// <summary>
        /// 审核通过
        /// </summary>
        Adopt,
        /// <summary>
        /// 审核未通过
        /// </summary>
        NotPass
    }
}
