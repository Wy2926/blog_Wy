using CC.Blog.Spiders;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CC.Blog.Tests.Spiders
{
    public class SpiderAppService_Tests : BlogTestBase
    {
        private readonly ISpiderAppService _spiderAppService;

        public SpiderAppService_Tests()
        {
            _spiderAppService = Resolve<ISpiderAppService>();
        }

        /// <summary>
        /// 提交链接到搜索引擎
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        [Fact]
        public Task<List<(string, string)>> SubmitLink()
        {
            return _spiderAppService.SubmitLink("https://www.33323.xyz/Article/Details_10.html");
        }

        /// <summary>
        /// 提交链接到搜索引擎
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        [Fact]
        public async Task CleaningRecords()
        {
            await _spiderAppService.CleaningRecords(null, null);
        }
    }
}
