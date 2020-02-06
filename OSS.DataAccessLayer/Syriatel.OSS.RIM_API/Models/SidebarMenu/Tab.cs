using Syriatel.NPM.WebAPI.Models.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.RIM_API.Models.SidebarMenu
{
    public class Tab
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public string Icon { get; set; }

        public string State { get; set; }

        public bool Target { get; set; }

        public string Badge { get; set; }

        public List<Tab> children { get; set; }

        public Tab(TabModelView tab)
        {
            this.Id = tab.Id;
            this.Name = tab.Name;
            this.Type = Constants.TAB_TYPE_LINK;
            this.Icon = tab.Icon;
            this.State = tab.Route;
            this.Target = tab.Target;
            this.Badge = tab.Badge;
            this.children = null;
        }
    }
}