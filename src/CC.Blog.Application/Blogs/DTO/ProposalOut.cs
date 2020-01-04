using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CC.Blog.Blogs.DTO
{
    [AutoMapFrom(typeof(BlogProposal))]
    public class ProposalOut
    {
        /// <summary>
        /// 建议ID
        /// </summary>
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [Required]
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

        /// <summary>
        /// 建议内容
        /// </summary>
        [Required]
        public DateTime CreationTime { get; set; }
    }
}
