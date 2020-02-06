using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.API.Models.Site
{
    public class Zone
    {
        public string ZONE { get; set; }
        public List<SubArea> SubAreas { get; set; }
    }
}