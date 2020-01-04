using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CC.Blog.Blogs
{
    /// <summary>
    /// 建议
    /// </summary>
    public class BlogProposal : AggregateRoot, ICreationAudited, IDeletionAudited
    {
        /// <summary>
        /// 邮箱
        /// </summary>
        [MaxLength(200)]
        public string Email { get; set; }

        /// <summary>
        /// 建议内容
        /// </summary>
        [Required]
        [MaxLength(1000)]
        public string Body { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        public string Ip { get; set; }

        public long? CreatorUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}
