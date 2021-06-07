using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.RIM_API.Models.CategoryTree
{
    public class CategoryTree
    {
        [JsonProperty(Order = 1)]
        public int id { get; set; }

        [JsonProperty(Order = 2)]
        public string name { get; set; }

        [JsonProperty(Order = 3)]

        public List<CategoryTree> children;
    }
}