using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.API.Models
{
    public class DynamicJoinData
    {
        public string ClassificationName { get; set; }
        public string JoinType { get; set; }

        public List<string> EfectedTabels { get; set; }

    }
}