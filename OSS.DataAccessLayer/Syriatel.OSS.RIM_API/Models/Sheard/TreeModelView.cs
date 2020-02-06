using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.RIM_API.Models.CategoryEntities
{
    public class TreeModelView
    {
        public int Id { get; set; }

        public int Level { get; set; }

        public int Order { get; set; }

        public string Name { get; set; }

        public int? ParentId { get; set; }
    }
}