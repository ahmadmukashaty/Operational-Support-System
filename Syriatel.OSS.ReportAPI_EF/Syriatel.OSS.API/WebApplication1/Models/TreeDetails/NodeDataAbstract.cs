using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.API.Models.TreeDetails
{
    public class NodeDataAbstract
    {
        public string Name { get; set; }

        public int Id { get; set; }

        public string Type { get; set; }

        public int? Order { get; set; }

        public int? Parent_Id { get; set; }

        public int Category_Id { get; set; }
    }
}