using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.RIM_API.Models.Sheard
{
    public class Tree
    {
        [JsonProperty(Order = 1)]
        public int id { get; set; }

        [JsonProperty(Order = 2)]
        public string value { get; set; }

        [JsonProperty(Order = 3)]
        public List<Tree> children;

        public Tree(int id, string value, List<Tree> children)
        {
            this.id = id;
            this.value = value;
            this.children = children;
        }

        
    }
}