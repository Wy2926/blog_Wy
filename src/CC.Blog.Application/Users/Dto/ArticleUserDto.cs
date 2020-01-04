using Abp.AutoMapper;
using CC.Blog.Authorization.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace CC.Blog.Users.Dto
{
    /// <summary>
    /// 文章需要的User信息
    /// </summary>
    [AutoMapFrom(typeof(User))]
    public class ArticleUserDto
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
    }
}
