using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.API.Models.Site
{
    public class Area
    {
        public string AREA { get; set; }
        public List<Zone> Zones { get; set; }
    }
}