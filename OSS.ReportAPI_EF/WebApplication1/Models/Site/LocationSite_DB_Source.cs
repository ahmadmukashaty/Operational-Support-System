using Syriatel.OSS.API.Models.RAN_Site;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.API.Models.Site
{
    public class LocationSite_DB_Source
    {
        public DB_Source RegionSource { get; set; }
        public DB_Source AreaSource { get; set; }
        public DB_Source ZoneSource { get; set; }
        public DB_Source SubAreaSource { get; set; }
        public DB_SiteSource SiteSource { get; set; }

        public LocationSite_DB_Source()
        {
            this.RegionSource = new DB_Source();
            this.AreaSource = new DB_Source();
            this.ZoneSource = new DB_Source();
            this.SubAreaSource = new DB_Source();
            this.SiteSource = new DB_SiteSource();
        }

    }
}