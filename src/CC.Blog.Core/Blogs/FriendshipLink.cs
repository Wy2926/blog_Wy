using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CC.Blog.Blogs
{
    /// <summary>
    /// 友情链接
    /// </summary>
    public class FriendshipLink : AggregateRoot, ICreationAudited, IDeletionAudited
    {
        /// <summary>
        /// 站点名称
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        /// <summary>
        /// 站点地址
        /// </summary>
        [Required]
        [MaxLength(256)]
        public string Url { get; set; }

        /// <summary>
        /// 网站图标
        /// </summary>
        [MaxLength(256)]
        public string Ioc { get; set; }

        /// <summary>
        /// 站点联系邮箱
        /// </summary>
        [MaxLength(256)]
        public string Email { get; set; }

        /// <summary>
        /// 友链下线时间
        /// </summary>
        public DateTime OfflineTime { get; set; }

        /// <summary>
        /// 站点排序
        /// </summary>
        [Required]
        public int Order { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        /// 友链类型
        /// </summary>
        [Required]
        [DefaultValue((int)LinkType.HomePage)]
        public LinkType LinkType { get; set; }

        /// <summary>
        /// 审核状态
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

    /// <summary>
    /// 友链类型
    /// </summary>
    public enum LinkType
    {
        /// <summary>
        /// 首页
        /// </summary>
        HomePage,
        /// <summary>
        /// 全站
        /// </summary>
        TotalStation
    }
}
