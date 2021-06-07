using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syriatel.OSS.DataAccessLayer.Models.LocationSiteClasses
{
    public class Region
    {
        public string REGION {get; set;}
        public List<Area> Areas { get; set; }

    
    }
}
