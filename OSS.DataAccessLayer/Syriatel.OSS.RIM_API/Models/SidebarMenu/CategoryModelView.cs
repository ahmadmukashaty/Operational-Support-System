using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.RIM_API.Models.SidebarMenu
{
    public class CategoryModelView
    {
        public int Id { get; set; }

        public int Order { get; set; }

        public bool IsOpen { get; set; }

        public string Title { get; set; }

        public string Name { get; set; }
    }
}