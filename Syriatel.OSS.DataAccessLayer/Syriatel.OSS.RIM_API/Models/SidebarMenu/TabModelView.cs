using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.RIM_API.Models.SidebarMenu
{
    public class TabModelView
    {
        public int Id { get; set; }

        public int Level { get; set; }

        public int Order { get; set; }

        public string Name { get; set; }

        public int? ParentId { get; set; }

        public string Icon { get; set; }

        public string Route { get; set; }

        public bool Target { get; set; }

        public string Badge { get; set; }
    }
}