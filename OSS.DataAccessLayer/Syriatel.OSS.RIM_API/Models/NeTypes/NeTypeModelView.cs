using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.RIM_API.Models.NeTypes
{
    public class NeTypeModelView
    {
        public string TableName { get; set; }

        public string ColumnName { get; set; }

        public string DisplayName { get; set; }

        public int Order { get; set; }
    }
}