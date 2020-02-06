using Syriatel.OSS.RIM_API.Models.Sheard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.RIM_API.Models.SubCategoryTreeEntities
{
    public class SubCategoryTree
    {
        public SubCategoryTreeBasics SbTreeBasic { get; set; }

        public List<Tree> Trees { get; set; }
    }
}