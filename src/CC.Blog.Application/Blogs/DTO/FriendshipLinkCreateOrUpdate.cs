using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CC.Blog.Blogs.DTO
{
    [AutoMapTo(typeof(FriendshipLink))]
    public class FriendshipLinkCreateOrUpdate
    {

        /// <summary>
        /// 站点名称
        /// </summary>
        public int? Id { get; set; }

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
        /// 友链类型
        /// </summary>
        [Required]
        public LinkType LinkType { get; set; }
    }
}
