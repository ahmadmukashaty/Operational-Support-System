﻿using Syriatel.OSS.API.Models.Trees.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.API.Models.Trees.AttributesRanTree.NodeCreation
{
    public class AttributeNodeCreation
    {
        public TreeModel Tree { get; set; }

        public AttributeNodeCreation(string DisplayName, string ColumnName, string ColumnType)
        {
            AttributeNodeData rootAttributes = new AttributeNodeData(0, 0, null, ColumnName, ColumnType);
            this.Tree = new TreeModel(DisplayName, rootAttributes, true);
        }
    }
}