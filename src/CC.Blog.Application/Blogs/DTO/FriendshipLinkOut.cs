using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CC.Blog.Blogs.DTO
{
    [AutoMapFrom(typeof(FriendshipLink))]
    public class FriendshipLinkOut
    {
        /// <summary>
        /// 站点名称
        /// </summary>
        [Required]
        public int Id { get; set; }

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
        /// 友链下线时间
        /// </summary>
        [Required]
        public DateTime OfflineTime { get; set; }

        /// <summary>
        /// 站点排序
        /// </summary>
        [Required]
        public int Order { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        [Required]
        public string Ip { get; set; }

        /// <summary>
        /// 友链类型
        /// </summary>
        [Required]
        public LinkType LinkType { get; set; }

        /// <summary>
        /// 审核状态
        /// </summary>
        [Required]
        public ToExamine ToExamine { get; set; }
    }
}
