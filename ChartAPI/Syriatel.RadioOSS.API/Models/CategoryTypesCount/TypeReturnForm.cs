using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syriatel.RadioOSS.API.Models.CategoryTypesCount
{
    public class TypeReturnForm
    {
        public List<String> LocationNames { get; set; }
        public List<String> Types { get; set; }
        public List<Counts> LocContValues { get; set; }
    }
}