using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syriatel.RadioOSS.API.Models.CategoryTypesCount
{
    public class LocationInfo
    {
        public String TabelName { get; set; }
        public String Value { get; set; }
        public String ColumnName { get; set; }
        public String ColumnType { get; set; }
        public int Order { get; set; }
    }
}