using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.API.Models.Site
{
    public class LocationSiteModelView
    {
        public string Region { get; set; }

        public string Area { get; set; }

        public string Zone { get; set; }

        public string SubArea { get; set; }

        public SiteData Site { get; set; }
    }
}