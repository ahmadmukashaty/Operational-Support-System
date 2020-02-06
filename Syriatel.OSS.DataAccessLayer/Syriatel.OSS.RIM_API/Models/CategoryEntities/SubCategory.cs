using Syriatel.OSS.RIM_API.Models.Sheard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.RIM_API.Models.CategoryEntities
{
    public class SubCategory
    {
        public int Order { get; set; }

        public Tree SubCategoryTree { get; set; }
    }
}