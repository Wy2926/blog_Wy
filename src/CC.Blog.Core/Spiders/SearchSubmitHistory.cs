using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CC.Blog.Spiders
{
    /// <summary>
    /// 链接提交历史
    /// </summary>
    public class SearchSubmitHistory : AggregateRoot<long>
    {
        /// <summary>
        /// 标题
        /// </summary>
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// 链接
        /// </summary>
        [Required]
        public string Url { get; set; }

        /// <summary>
        /// 名称（百度/360/谷歌/等等）
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 异常信息
        /// </summary>
        public string Error { get; set; }

        /// <summary>
        /// 提交时间
        /// </summary>
        [Required]
        public DateTime CreationTime { get; set; }
    }
}
