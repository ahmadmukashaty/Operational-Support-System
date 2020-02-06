using Syriatel.RadioOSS.API.Models.Helper.LocationHelperClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syriatel.RadioOSS.API.Models.CategoryTypesCount
{
    public class CountTypePostObject
    {
        public String CategoryName { get; set; }
        public String Model { get; set; }
        public LocationFilterValues LoctionValues { get; set; }
        public DB_Source Level { get; set; }

        public LocationSite_DB_Source dbsource { get; set; }
    }
}