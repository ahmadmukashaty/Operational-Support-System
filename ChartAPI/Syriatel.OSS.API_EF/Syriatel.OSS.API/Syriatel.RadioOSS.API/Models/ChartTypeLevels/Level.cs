using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syriatel.RadioOSS.API.Models.ChartTypeLevels
{
    public class Level
    {
        public String DISPLAYNAME { get; set; }
        public String TABLENAME { get; set; }
        public String COLUMNNAME { get; set; }
        public String COLUMNTYPE { get; set; } 
        public int ORDER { get; set; }
    }
}