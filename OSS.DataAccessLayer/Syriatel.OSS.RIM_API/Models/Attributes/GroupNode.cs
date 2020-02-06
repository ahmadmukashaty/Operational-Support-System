using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.RIM_API.Models.Attributes
{
    public class GroupNode
    {
        [JsonProperty(Order = 3)]
        public int groupId { get; set; }

        [JsonProperty(Order = 4)]
        public string tableName { get; set; }

        //[JsonProperty(Order = 5)]
        //public List<TreeNode> children { get; set; }
    }
}