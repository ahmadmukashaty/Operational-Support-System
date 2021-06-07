using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syriatel.OSS.DataAccessLayer.Models.LocationSiteClasses
{
    public class Area
    {
        public string AREA { get; set; }
        public List<Zone> Zones { get; set; }

       
    }
}
