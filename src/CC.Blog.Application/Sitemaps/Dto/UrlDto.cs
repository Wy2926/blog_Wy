using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace CC.Blog.Sitemaps.Dto
{
    [XmlType("url")]
    public class UrlDto
    {
        public string loc { get; set; }
        public string priority { get; set; }
        public string lastmod { get; set; }
        public string changefreq { get; set; }
    }
}
