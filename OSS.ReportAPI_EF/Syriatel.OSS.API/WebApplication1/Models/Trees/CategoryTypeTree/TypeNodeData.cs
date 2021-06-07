using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.API.Models.Trees.CategoryRanTypeTree
{
    public class TypeNodeData
    {
        public string TableName { get; set; }

        public string ColumnName { get; set; }

        public string ColumnType { get; set; }

        public TypeNodeData(string TableName, string ColumnName, string ColumnType)
        {
            this.TableName = TableName;
            this.ColumnName = ColumnName;
            this.ColumnType = ColumnType;
        }

        public TypeNodeData()
        {
            this.TableName = null;
            this.ColumnName = null;
            this.ColumnType = null;
        }
    }
}