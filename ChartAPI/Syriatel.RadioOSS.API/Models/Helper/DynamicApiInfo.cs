using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syriatel.RadioOSS.API.Models.Helper
{
    public class DynamicApiInfo
    {
        public String CategoryName { get; set; }
        public String ModelName { get; set; }
        public String JoinType { get; set; }
        public List<String> EfectedTabels { get; set; }
    }
}