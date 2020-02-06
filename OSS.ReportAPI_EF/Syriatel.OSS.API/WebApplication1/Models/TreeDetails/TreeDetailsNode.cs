using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.API.Models.TreeDetails
{
    public class TreeDetailsNode
    {
        public string label { get; set; }
        public object data { get; set; }
        public object icon { get; set; }
        public object expandedIcon { get; set; }
        public object collapsedIcon { get; set; }
        public List<TreeDetailsNode> children { get; set; }
        public bool leaf { get; set; }
        //public bool expanded { get; set; }
        public string type { get; set; }

        //public TreeDetailsNode parent { get; set; }
        //public bool partialSelected { get; set; }
        //public string styleClass { get; set; }
        //public bool draggable { get; set; }
        //public bool droppable { get; set; }
        //public bool selectable { get; set; }

        public TreeDetailsNode(bool leaf, string collapseIcon, string expandedIcon)
        {
            this.leaf = leaf;
            this.collapsedIcon = collapseIcon;
            this.expandedIcon = expandedIcon;
        }

    }
}