using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syriatel.RadioOSS.API.Structure
{
    public class Instance
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public int? ParentId { get; set; }

        public string DisName { get; set; }
        public int? SId { get; set; }
        public int? SlId { get; set; }
        public int? PId { get; set; }
    }
}