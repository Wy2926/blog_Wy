using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CC.Blog.Blogs
{
    /// <summary>
    /// 文章标签
    /// </summary>
    public class ArticleTag : AggregateRoot<long>, ICreationAudited, IDeletionAudited
    {
        /// <summary>
        /// 文章ID
        /// </summary>
        public long ArticleId { get; set; }

        [ForeignKey("ArticleId")]
        public Article Article { get; set; }

        /// <summary>
        /// 标签名称
        /// </summary>
        [Required]
        public string Name { get; set; }

        public long? CreatorUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}
