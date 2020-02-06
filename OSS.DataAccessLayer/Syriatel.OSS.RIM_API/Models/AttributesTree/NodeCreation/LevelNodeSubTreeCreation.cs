using Syriatel.OSS.RIM_API.Models.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.RIM_API.Models.AttributesTree
{
    public class LevelNodeTreeCreation
    {
        public AttributeTree Tree { get; set; }

        public List<AttributeModelView> attributeNodes { get; set; }

        public LevelNodeTreeCreation(string levelNodeName, int tableId, string tableName)
        {
            this.attributeNodes = OracleHelper.GetTableAttributes(tableId);

            init(levelNodeName, tableId, tableName);
        }

        public void init(string levelNodeName, int tableId, string tableName)
        {
            NodeData rootAttributes = new NodeData(tableId, false, levelNodeName, tableName, "LevelNode", null, null);
            this.Tree = new AttributeTree(levelNodeName, rootAttributes, false);
        }

        private void GenerateLevelNodeTree()
        {
            this.attributeNodes.Sort(delegate(AttributeModelView c1, AttributeModelView c2)
            {
                if (c1.Order == null)
                    return 1;
                if (c2.Order == null)
                    return -1;
                return ((int)c1.Order).CompareTo((int)c2.Order);
            });

            foreach (AttributeModelView attributeNode in attributeNodes)
            {
                AttributeNodeCreation attributeNodeTree = new AttributeNodeCreation(attributeNode.DisplayName, attributeNode.Id, attributeNode.ColumnName, attributeNode.ColumnType);
                if (this.Tree.children == null)
                    this.Tree.children = new List<AttributeTree>();
                this.Tree.children.Add(attributeNodeTree.Tree);
            }
        }
    }
}