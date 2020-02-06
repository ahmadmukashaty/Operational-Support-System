using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.RIM_API.Models.NeTypes
{
    public class TypeTree
    {
        
        [JsonProperty(Order = 3)]

        public string tableName { get; set; }
        [JsonProperty(Order = 4)]

        public string columnName { get; set; }
    
    }
}