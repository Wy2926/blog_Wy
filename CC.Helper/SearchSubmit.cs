using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CC.Helper.Expand;
using Newtonsoft.Json;

namespace CC.Helper
{
    public class SearchSubmit
    {
        /// <summary>
        /// 向百度搜索提交链接
        /// </summary>
        /// <param name="url"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static async Task<ResponseResult> SubmitBaiduAsync(List<string> urls, string key)
        {
            ResponseResult response = new ResponseResult();
            if (string.IsNullOrEmpty(key))
            {
                response.Error = new Exception("百度Key为空");
                return response;
            }
            try
            {
                using (HttpWebResponse httpWebResponse = await CCHttpRequest.CreatePostHttpResponseAsync($"http://data.zz.baidu.com/urls?site=www.33323.xyz&token={key}", string.Join("\r\n", urls)))
                {
                    //获取返回内容
                    string json = await httpWebResponse.GetResponseStream().ReadAllTextAsync();
                    //将JSON字符串转换为dynamic类型
                    dynamic obj = JsonConvert.DeserializeObject<dynamic>(json);
                    if (obj.error != null)
                    {
                        response.Error = new Exception(obj.message);
                        return response;
                    }
                    response.NotSameSiteUrl = obj.not_same_site == null ? new List<string>() : obj.not_same_site.ToObject<List<string>>();
                    response.NotValidUrl = obj.not_valid == null ? new List<string>() : obj.not_valid.ToObject<List<string>>();
                    response.Remain = obj.remain;
                    response.SuccessUrl = urls
                        .Where(p => !response.NotValidUrl.Contains(p) && !response.NotSameSiteUrl.Contains(p))
                        .ToList();
                    return response;
                }
            }
            catch (Exception ex)
            {
                throw ex;
                response.Error = ex;
            }
            return response;
        }
    }

    public class ResponseResult
    {
        /// <summary>
        /// 成功提交的链接
        /// </summary>
        public List<string> SuccessUrl { get; set; }

        /// <summary>
        /// 非法链接链接
        /// </summary>
        public List<string> NotValidUrl { get; set; }

        /// <summary>
        /// 非本站链接
        /// </summary>
        public List<string> NotSameSiteUrl { get; set; }

        /// <summary>
        /// 剩余可推送数量
        /// </summary>
        public int Remain { get; set; }

        /// <summary>
        /// 异常信息
        /// </summary>
        public Exception Error { get; set; }
    }
}
