using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.RIM_API.Models.Attributes
{
    public class GroupAttributeModelView
    {
        public int Id { get; set; }

        public string TableName { get; set; }

        public List<Attribute> Attributes { get; set; }

        public string GroupName { get; set; }

        public int? ParentId { get; set; }

        public int Order { get; set; }

    }
}