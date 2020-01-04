using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CC.Blog.Spiders
{
    /// <summary>
    /// 搜索蜘蛛
    /// </summary>
    public class Spider : AggregateRoot<long>
    {
        /// <summary>
        /// 来源（百度/360/谷歌/等等）
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        [Required]
        public string Ip { get; set; }


        /// <summary>
        /// 访问地址
        /// </summary>
        [Required]
        public string Url { get; set; }

        /// <summary>
        /// 头信息
        /// </summary>
        [Required]
        public string UserAgent { get; set; }

        /// <summary>
        /// 返回状态码
        /// </summary>
        [Required]
        public int Code { get; set; }

        /// <summary>
        /// 访问时间
        /// </summary>
        [Required]
        public DateTime CreationTime { get; set; }
    }
}
