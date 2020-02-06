using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syriatel.RadioOSS.API.Models.Helper.LocationHelperClass
{
    public class DbCellCountReturn
    {
        public string LOCATION { get; set; }
        public string BAND { get; set; }
        public decimal COUNT { get; set; }
    }
}