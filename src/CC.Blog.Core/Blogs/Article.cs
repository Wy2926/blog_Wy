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
    /// 文章
    /// </summary>
    public class Article : AggregateRoot<long>, ICreationAudited, IDeletionAudited
    {
        [ForeignKey("CreatorUserId")]
        public virtual User User { get; set; }

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
        public string CoverMap { get; set; }

        /// <summary>
        /// 简介
        /// </summary>
        [Required]
        [MaxLength(500)]
        public string Desc { get; set; }

        /// <summary>
        /// 文本内容
        /// </summary>
        [Required]
        [MaxLength(500000)]
        public string Body { get; set; }

        /// <summary>
        /// HTML内容
        /// </summary>
        [Required]
        [MaxLength(500000)]
        public string Html { get; set; }

        /// <summary>
        /// 文章类型
        /// </summary>
        [Required]
        [ForeignKey("ArticleTypeId")]
        public virtual ArticleType ArticleType { get; set; }

        /// <summary>
        /// 文章类型
        /// </summary>
        [Required]
        public int ArticleTypeId { get; set; }

        /// <summary>
        /// 推荐数
        /// </summary>
        [Required]
        public int Recommend { get; set; }

        /// <summary>
        /// 反对数
        /// </summary>
        [Required]
        public int Opposition { get; set; }

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
        /// 楼层数
        /// </summary>
        [Required]
        public int FloorNum { get; set; }

        /// <summary>
        /// 隐藏内容
        /// </summary>
        public string HideBody { get; set; }

        /// <summary>
        /// 标签集合
        /// </summary>
        public virtual ICollection<ArticleTag> ArticleTags { get; set; } = new HashSet<ArticleTag>();

        /// <summary>
        /// 是否启用
        /// </summary>
        [Required]
        public bool IsEnable { get; set; }

        /// <summary>
        /// 审核状态
        /// </summary>
        [Required]
        [DefaultValue((int)ToExamine.Unaudited)]
        public ToExamine ToExamine { get; set; }

        /// <summary>
        /// 最后回复时间
        /// </summary>
        public DateTime ReplyTime { get; set; }

        public long? CreatorUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}
