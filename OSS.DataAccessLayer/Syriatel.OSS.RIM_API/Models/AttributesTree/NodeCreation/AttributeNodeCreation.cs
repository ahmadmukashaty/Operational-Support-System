using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.RIM_API.Models.AttributesTree
{
    public class AttributeNodeCreation
    {
        public AttributeTree Tree { get; set; }

        public AttributeNodeCreation(string DisplayName, int attributeId, string ColumnName, string ColumnType)
        {
            NodeData rootAttributes = new NodeData(attributeId, false, null, null, "Attribute", ColumnName, ColumnType);
            this.Tree = new AttributeTree(DisplayName, rootAttributes, true);
        }
    }
}