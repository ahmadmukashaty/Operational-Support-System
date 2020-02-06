using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syriatel.RadioOSS.API.Models.CategoryTypesCount
{
    public class TypesReturnResult
    {
        public List<String> LocationNames { get; set; }
        public List<TotalTypeCounts> TotalLocationTypes { get; set; }
    }
}