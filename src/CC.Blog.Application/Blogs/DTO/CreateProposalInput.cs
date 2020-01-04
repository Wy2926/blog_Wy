using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CC.Blog.Blogs.DTO
{
    [AutoMapTo(typeof(BlogProposal))]
    public class CreateProposalInput
    {
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
    }
}
