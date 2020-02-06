using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.RIM_API.Models.AttributesTree
{
    public class LevelNodeModelView
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public int? Order { get; set; }

        public string TableName { get; set; }
    }
}