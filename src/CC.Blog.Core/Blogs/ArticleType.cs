using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CC.Blog.Blogs
{
    public class ArticleType : AggregateRoot, ICreationAudited, IDeletionAudited
    {
        /// <summary>
        /// 类型名称
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 父类型
        /// </summary>
        [ForeignKey("ParArticleTypeId")]
        public virtual ArticleType ParArticleType { get; set; }

        public int? ParArticleTypeId { get; set; }


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

        public long? CreatorUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}
