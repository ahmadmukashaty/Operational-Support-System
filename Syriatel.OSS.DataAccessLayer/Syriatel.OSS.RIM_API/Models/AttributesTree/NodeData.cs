using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.RIM_API.Models.AttributesTree
{
    public class NodeData
    {
        public int Id { get; set; }

        public bool IsRoot { get; set; }

        public string LevelName { get; set; }

        public string TableName { get; set; }

        public string NodeType { get; set; }

        public string ColumnName { get; set; }

        public string ColumnType { get; set; }

        public NodeData(int Id, bool IsRoot, string LevelName, string TableName, string NodeType, string ColumnName, string ColumnType)
        {
            this.Id = Id;
            this.IsRoot = IsRoot;
            this.LevelName = LevelName;
            this.TableName = TableName;
            this.NodeType = NodeType;
            this.ColumnName = ColumnName;
            this.ColumnType = ColumnType;
        }
    }
}