using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.RIM_API.Models.Attributes
{
    public class Attribute
    {
        public int Id { get; set; }
        public string ColumnName { get; set; }

        public string DisplayName { get; set; }

        public string ColumnType { get; set; }

        public int Order { get; set; }
    }
}