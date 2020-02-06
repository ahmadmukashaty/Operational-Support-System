using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.API.NeTreeModeView
{
    public class Tree
    {
        public int NodeId { get; set; }

        public int InstanceId { get; set;}

        public string Name { get; set;}

        public string CategoryName { get; set; }

        public string Type { get; set; }


        public string ChildType { get; set; }

        public List<Tree> children { get; set; }
    }
}