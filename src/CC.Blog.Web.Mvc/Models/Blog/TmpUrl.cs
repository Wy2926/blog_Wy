using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CC.Blog.Web.Models.Blog
{
    public class TmpUrl
    {
        public List<string> Data { get; set; }

        public int Errno { get; set; }

        public string Msg { get; set; }

        public static TmpUrl SuccessInfo(string msg, List<string> ls)
        {
            TmpUrl tmpUrl = new TmpUrl();
            tmpUrl.Errno = 200;
            tmpUrl.Msg = msg;
            tmpUrl.Data = ls;
            return tmpUrl;
        }
    }
}
