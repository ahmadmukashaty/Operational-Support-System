using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.API.Models.Site
{
    public class Region
    {
        public string REGION { get; set; }
        public List<Area> Areas { get; set; }
    }
}