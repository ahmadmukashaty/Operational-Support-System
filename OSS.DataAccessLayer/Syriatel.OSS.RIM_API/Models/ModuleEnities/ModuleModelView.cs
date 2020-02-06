using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.RIM_API.Models.ModuleEnities
{
    public class ModuleModelView
    {
        public int Id { get; set; }

        public string RootName { get; set; }

        public string Name { get; set; }

        public int ModuleBarId { get; set; }

        public bool SearchFlag { get; set; }
    }
}