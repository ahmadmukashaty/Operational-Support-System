using Syriatel.OSS.DataAccessLayer.Models.LocationSiteClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.RIM_API.Models.LocationSiteClasses
{
    public class LocationSiteModelView
    {
        public string Region { get; set; }

        public string Area { get; set; }

        public string Zone { get; set; }

        public string SubArea { get; set; }

        public Site Site { get; set; }
    }
}