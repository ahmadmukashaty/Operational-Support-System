using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.RIM_API.Models.ResponseTree
{
    public class TreeJason
    {
        public string label { get; set; }
        public object data { get; set; }
        public string icon {get; set;}

        public string expandedIcon { get; set; }
        public string collapsedIcon { get; set; }
        public List<TreeJason> children { get; set; }
        public bool leaf {get; set;}
        public bool expanded {get; set;}
        public string type {get; set;}
        public TreeJason parent {set; get;}
        public bool partialSelected {get; set;}
        public string styleClass {set; get;}
        public bool draggable { set; get; }
        public bool droppable { set; get; }
        public bool selectable { set; get; }
    }
}