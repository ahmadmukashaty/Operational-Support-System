using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.API.Models.Site
{
    public class SubArea
    {
        public string SUBAREA { get; set; }
        public List<SiteData> Sites { get; set; }
    }
}