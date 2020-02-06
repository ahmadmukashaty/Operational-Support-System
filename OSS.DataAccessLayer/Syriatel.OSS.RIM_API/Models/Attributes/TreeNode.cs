using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.RIM_API.Models.Attributes
{
    public class TreeNode
    {
        [JsonProperty(Order = 1)]
        public int id { get; set; }

        [JsonProperty(Order = 2)]
        public string name { get; set; }
    }
}