using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.API.Models.Trees.AttributesRanTree.ModelViews
{
    public class AttributeModelView
    {
        public string ColumnName { get; set; }

        public string DisplayName { get; set; }

        public string ColumnType { get; set; }

        public int? Order { get; set; }
    }
}