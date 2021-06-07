using Syriatel.OSS.DataAccessLayer.Models.LocationSiteClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.RIM_API.Models.LocationSiteClasses
{
    public class LocationSiteData
    {
        public List<string> Region { get; set; }
        public List<string> Area { get; set; }
        public List<string> Zone { get; set; }
        public List<string> SubArea { get; set; }
        public List<Site> Site { get; set; }

        public LocationSiteHierarchy DataHierarchy { get; set; }
       
        
        public LocationSiteData()
        {
            this.Region = new List<string>();
            this.Area = new List<string>();
            this.Zone = new List<string>();
            this.SubArea = new List<string>();
            this.Site = new List<Site>();
        }
    }
}