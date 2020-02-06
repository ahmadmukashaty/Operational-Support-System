using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.RIM_API.Models.Attributes
{
    public class AttributeTree
    {
        [JsonProperty(Order = 1)]
        public int id { get; set; }

        [JsonProperty(Order = 2)]
        public string name { get; set; }

        [JsonProperty(Order = 3)]
        public int groupId { get; set; }

        [JsonProperty(Order = 4)]
        public string groupName { get; set; }

        [JsonProperty(Order = 5)]
        public string tableName { get; set; }

        [JsonProperty(Order = 6)]
        public string columnName { get; set; }

        [JsonProperty(Order = 6)]
        public string columnType { get; set; }

        [JsonProperty(Order = 7)]
        public List<AttributeTree> children;

        public AttributeTree(int id, string name, List<AttributeTree> children)
        {
            this.id = id;
            this.name = name;
            this.groupId = groupId;
            this.groupName = groupName;
            this.tableName = tableName;
            this.columnName = columnName;
            this.columnType = columnType;
            this.children = children;
        }

        public AttributeTree()
        {

        }
    }
}