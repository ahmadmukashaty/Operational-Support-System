using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syriatel.RadioOSS.API.Models.Buysness
{
    public class BusynessReturn
    {
        public List<String> LocationNames { get; set; }
        public List<int> Free { get; set; }
        public List <int> Busy { get; set; }
        public List<int> Capacity { get; set; }
    }
}