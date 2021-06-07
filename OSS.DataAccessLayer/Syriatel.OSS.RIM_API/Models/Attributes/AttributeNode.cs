using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.RIM_API.Models.Attributes
{
    public class AttributeNode
    {
        [JsonProperty(Order = 3)]
        public int AttributeId { get; set; }

        [JsonProperty(Order = 4)]
        public string columnName { get; set; }

        [JsonProperty(Order = 5)]
        public string columnType { get; set; }
    }
}