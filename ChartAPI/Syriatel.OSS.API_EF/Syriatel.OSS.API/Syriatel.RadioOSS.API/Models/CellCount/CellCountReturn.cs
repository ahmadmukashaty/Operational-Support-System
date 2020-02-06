using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syriatel.RadioOSS.API.Models.CellCount
{
    public class CellCountReturn
    {
        public List<String> LocationNames { get; set; }
        public List<TotalBandCounts> TotalLocationCells { get; set; }
    }
}