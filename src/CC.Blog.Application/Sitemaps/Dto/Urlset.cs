using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace CC.Blog.Sitemaps.Dto
{
    [XmlRoot(Namespace = "http://www.sitemaps.org/schemas/sitemap/0.9")]
    [XmlType("urlset")]
    public class Urlset
    {
        [XmlElement("url")]
        public List<UrlDto> Urls { get; set; } = new List<UrlDto>();
    }
}
