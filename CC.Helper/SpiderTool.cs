using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Net;

namespace CC.Helper
{
    /// <summary>
    /// 蜘蛛工具类
    /// </summary>
    public class SpiderTool
    {
        private static Dictionary<string, string> uaDic { get; set; } = new Dictionary<string, string>()
        {
            { "baiduspider","百度"},
            { "bingbot","必应"},
            { "360spider","360"},
            { "sogou","搜狗"},
            { "Googlebot","谷歌"},
            { "YisouSpider","神马"},
            { "Bytespider","头条"},
        };
        /// <summary>
        /// 通过UA查询是哪个搜索引擎
        /// </summary>
        /// <param name="ua"></param>
        /// <returns></returns>
        public static string UaSelect(string ua)
        {
            if (ua==null)
                return null;
            string _ua = ua.ToLower();
            var dic = uaDic.Where(p => _ua.Contains(p.Key.ToLower())).FirstOrDefault();
            if (!default(KeyValuePair<string, string>).Equals(dic))
                return dic.Value;
            return null;
        }

        /// <summary>
        /// 通过IP查询是哪个搜索引擎(DNS反查)
        /// </summary>
        /// <param name="ua"></param>
        /// <returns></returns>
        public async static Task<string> IpSelect(string ip)
        {
            string host = (await Dns.GetHostEntryAsync(IPAddress.Parse(ip))).HostName;
            return host;
        }
    }
}
